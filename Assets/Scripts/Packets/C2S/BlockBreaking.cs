using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Network;
using Assets.Util;
using UnityEngine;

namespace Assets.Scripts.Packets.C2S
{
    class BlockBreaking : IPacket
    {
        public int EntityId;
  
        public BlockPos Position;
  
        public int Progress;

        public BlockBreaking() {}

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            EntityId = packet.ReadVarInt();
            Position = packet.readBlockPos();
            Progress = packet.ReadByte();

            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            return state;
        }
    }
}
