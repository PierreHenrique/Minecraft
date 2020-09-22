using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Network;
using UnityEngine;

namespace Assets.Scripts.Packets.C2S
{
    class EntitySpawn : IPacket
    {
        public int Id;
  
        //public UUID uuid;
  
        public Vector3 Position;
        public Vector3 Velocity;
        public Vector2 Look;

        //private EntityType<?> entityTypeId;
  
        public int EntityData;

        public EntitySpawn() {}

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            Id = packet.ReadVarInt();
            //this.uuid = arg.readUuid();
            //this.entityTypeId = (EntityType)Registry.ENTITY_TYPE.get(arg.readVarInt());
            var x = packet.ReadDouble();
            var y = packet.ReadDouble();
            var z = packet.ReadDouble();

            Position = new Vector3(Convert.ToSingle(x), Convert.ToSingle(y), Convert.ToSingle(z));

            var pitch = packet.ReadByte();
            var yaw = packet.ReadByte();

            Look = new Vector2(pitch, yaw);

            EntityData = packet.ReadVarInt();

            var velocityX = packet.ReadInt16();
            var velocityY = packet.ReadInt16();
            var velocityZ = packet.ReadInt16();

            Velocity = new Vector3(velocityX, velocityY, velocityZ);

            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            return state;
        }
    }
}
