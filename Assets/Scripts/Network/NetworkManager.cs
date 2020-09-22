using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Packets;
using Assets.Scripts.Packets.Both;
using Assets.Scripts.Packets.C2S;
using Assets.Scripts.Packets.S2C;
using UnityEngine;

namespace Assets.Scripts.Network
{
    class NetworkManager
    {
        public static int Protocol = 736;

        private readonly object _streamLock = new object();
        
        private Stream _baseStream;
        public Stream BaseStream
        {
            get => _baseStream;
            set
            {
                lock (_streamLock)
                {
                    BufferedStream?.Flush();
                    _baseStream = value;
                    BufferedStream = new BufferedStream(value);
                    Packet = new PacketByteBuf(BufferedStream);
                }
            }
        }

        private BufferedStream BufferedStream { get; set; }
        private PacketByteBuf Packet { get; set; }
        public NetworkState NetworkState { get; private set; }
        public static int CompressionThreshold = 256;

        private static readonly Dictionary<NetworkState, Dictionary<NetworkSide, List<Type>>> States = new Dictionary<NetworkState, Dictionary<NetworkSide, List<Type>>>();

        public NetworkManager(Stream stream)
        {
            NetworkState = NetworkState.Handshaking;
            BaseStream = stream;
            Debug.Log(BufferedStream.CanRead);

            States.Add(NetworkState.Handshaking, new Dictionary<NetworkSide, List<Type>>
            {
                { NetworkSide.Clientbound, new List<Type> { typeof(Handshaking) } },
                { NetworkSide.Serverbound, new List<Type> { typeof(Handshaking) } }
            });

            States.Add(NetworkState.Status, new Dictionary<NetworkSide, List<Type>>
            {
                { NetworkSide.Clientbound, new List<Type> { typeof(StatusResponse), typeof(StatusPing) } },
                { NetworkSide.Serverbound, new List<Type> { typeof(StatusRequest), typeof(StatusPing) } }
            });

            States.Add(NetworkState.Login, new Dictionary<NetworkSide, List<Type>>
            {
                { NetworkSide.Clientbound, new List<Type> { typeof(LoginDisconnect), typeof(EncryptionKeyRequest), typeof(LoginSuccess), typeof(LoginCompression) } },
                { NetworkSide.Serverbound, new List<Type> { typeof(LoginStart) } }
            });

            States.Add(NetworkState.Play, new Dictionary<NetworkSide, List<Type>>
            {
                { NetworkSide.Clientbound, new List<Type>
                {
                    typeof(EntitySpawn), typeof(ExperienceOrb), typeof(MobSpawn), typeof(PaintingSpawn), typeof(PlayerSpawn), typeof(EntityAnimation), typeof(Statistics), typeof(PlayerActionResponse), typeof(BlockBreaking),
                    typeof(BlockEntityUpdate), typeof(BlockEvent), typeof(BlockUpdate), typeof(BossBar), typeof(Difficulty), typeof(GameMessage), typeof(ChunkDelta), typeof(CommandSuggestion), typeof(CommandTree), typeof(ConfirmGui), typeof(CloseScreen),
                    typeof(Inventory), typeof(ScreenHandlerProperty), typeof(ScreenHandlerSlot), typeof(Cooldown), typeof(CustomPayload), typeof(PlaySoundId), typeof(Disconnect), typeof(EntityStatus), typeof(Explosion), typeof(UnloadChunk), typeof(GameState),
                    typeof(OpenHorse), typeof(KeepAlive), typeof(ChunkData), typeof(WorldEvent), typeof(Particle), typeof(LightUpdate), typeof(GameJoin), typeof(MapUpdate), typeof(TradeOffers), typeof(EntityMoveRelative), typeof(EntityRotateMoveRelative),
                    typeof(EntityRotate), typeof(Entity), typeof(VehicleMove), typeof(OpenWrittenBook), typeof(OpenScreen), typeof(SignEditor), typeof(CraftFailedResponse), typeof(PlayerAbilities), typeof(CombatEvent), typeof(PlayerList), typeof(LookAt),
                    typeof(PlayerPositionLook), typeof(UnlockRecipes), typeof(EntitiesDestroy), typeof(RemoveEntityStatusEffect), typeof(ResourcePackSend), typeof(PlayerRespawn), typeof(EntitySetHeadYaw), typeof(SelectAdvancementTab), typeof(WorldBorder),
                    typeof(SetCameraEntity), typeof(HeldItemChange), typeof(ChunkDistanceRender), typeof(ChunkLoadDistance), typeof(PlayerSpawnPosition), typeof(ScoreboardDisplay), typeof(EntityTrackerUpdate), typeof(EntityAttach), typeof(EntityVelocityUpdate),
                    typeof(EntityEquipmentUpdate), typeof(ExperienceBarUpdate), typeof(HealthUpdate), typeof(ScoreboardObjectiveUpdate), typeof(EntityPassengersSet), typeof(Team), typeof(ScoreboardPlayerUpdate), typeof(WorldTimeUpdate), typeof(Title),
                    typeof(PlaySoundFromEntity), typeof(PlaySound), typeof(StopSound), typeof(PlayerListHeader), typeof(TagQueryResponse), typeof(ItemPickupAnimation), typeof(EntityPosition), typeof(AdvancementUpdate), typeof(EntityAttributes),
                    typeof(EntityStatusEffect), typeof(SynchronizeRecipes),
                } },
                { NetworkSide.Serverbound, new List<Type> { typeof(TeleportConfirm), typeof(KeepAlive) } }
            });

            //var a = Assembly.GetExecutingAssembly().GetTypes().Where(y => typeof(IPacket).IsAssignableFrom(y));
            //foreach (var b in a)
            //{
            //    _packetHandlers[b] = this;
            //}

            //PacketHandler = (from type in Assembly.GetExecutingAssembly().GetTypes() where typeof(IPacket).IsAssignableFrom(type) select type) as List<IPacket>;
        }

        public IPacket ReadPacket(NetworkSide side)
        {
            lock (_streamLock)
            {
                Debug.LogWarning($"[NetworkRead] - Begin <{NetworkState}/{side}>");

                long length = Packet.ReadVarInt();


                int id = Packet.ReadVarInt(out int idLength);
                Debug.LogWarning($"Length: {length}/{idLength} Id:{id}/{States[NetworkState][side].Count}");
                byte[] data = Packet.ReadUInt8Array((int)(length - idLength));

                if (States[NetworkState][side].Count <= id || States[NetworkState][side][id] == null)
                {
                    throw new InvalidOperationException("Invalid packet ID: 0x" + id.ToString("X2"));
                }
                Debug.Log($"xdxd: {id}");
                var ms = new PacketByteBuf(new MemoryStream(data));
                
                Debug.Log($"AKKJ: {id}");

                var packet = (IPacket)Activator.CreateInstance(States[NetworkState][side][id]);
                Debug.Log($"ytytytyt: {States[NetworkState][side][id].Name}");
                NetworkState = packet.ReadPacket(ms, NetworkState, side);

                Debug.Log($"Content: {id} {data} {packet.GetType().Name}");

                Debug.Log($"FKK: {NetworkState}");

                //if (ms.Position < data.Length)
                //    Debug.Log($"Warning: did not completely read packet: {packet.GetType().Name}"); // TODO: Find some other way to warn about this
                return packet;
            }
        }

        public void WritePacket(IPacket packet, NetworkSide side)
        {
            lock (_streamLock)
            {
                var newNetworkMode = packet.WritePacket(Packet, NetworkState, side);
                BufferedStream.WriteImmediately = true;
                int id = -1;
                var type = packet.GetType();

                Debug.Log($"[NetworkWrite] - Begin <{NetworkState}/{side}> {packet.GetType().Name} {States[NetworkState].Count}");

                // Find packet ID for this type

                for (int i = 0; i < States[NetworkState].Count; i++)
                {
                    if (States[NetworkState][side][i] == type)
                    {
                        id = i;
                        break;
                    }
                }

                if (packet is KeepAlive sender)
                {
                    Debug.LogWarning("Trying to fix");

                    id = 16;
                }

                if (id == -1)
                    throw new InvalidOperationException("Attempted to write invalid packet type.");

                Packet.WriteVarInt((int)BufferedStream.PendingWrites + PacketByteBuf.GetVarIntLength(id));
                Packet.WriteVarInt(id);
                BufferedStream.WriteImmediately = false;
                BufferedStream.Flush();
                NetworkState = newNetworkMode;
                Debug.Log($"[NetworkWrite] - End");
            }
        }
    }
}
