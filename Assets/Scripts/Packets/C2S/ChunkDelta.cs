using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Network;
using Assets.Util.Math;

namespace Assets.Scripts.Packets.C2S
{
    class ChunkDelta : IPacket
    {
        public ChunkPos ChunkPos;
        public ChunkDeltaRecord Records;

        public ChunkDelta() {}

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {


            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            return state;
        }
    }

    class ChunkDeltaRecord
    {
        public int Pos;
        public BlockState State;
    }
}
