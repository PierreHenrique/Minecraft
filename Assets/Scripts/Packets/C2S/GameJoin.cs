using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Network;

namespace Assets.Scripts.Packets.C2S
{
    class GameJoin : IPacket
    {
        public int PlayerEntityId;
  
        public long Sha256Seed;
  
        public bool Hardcore;
  
        public Gamemode GameMode;
  
        private Gamemode field_25713;
  
        //private Set<RegistryKey<World>> field_25320;
  
        //private RegistryTracker.Modifiable dimensionTracker;
  
        //private RegistryKey<DimensionType> field_25321;
  
        //private RegistryKey<World> dimensionId;
  
        public int MaxPlayers;
  
        public int ChunkLoadDistance;
  
        public bool ReducedDebugInfo;
  
        public bool ShowDeathScreen;
  
        public bool DebugWorld;
  
        public bool FlatWorld;

        public GameJoin() {}

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            return state;
        }
    }
}
