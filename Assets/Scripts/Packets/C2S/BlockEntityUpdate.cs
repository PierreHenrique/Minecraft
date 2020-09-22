using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Network;
using Assets.Util;
using fNbt;

namespace Assets.Scripts.Packets.C2S
{
    class BlockEntityUpdate : IPacket
    {
        public BlockPos Position;
  
        public int BlockEntityType;
  
        public NbtCompound Tag;

        public BlockEntityUpdate() {}

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            Position = packet.readBlockPos();
            BlockEntityType = packet.ReadByte();
            var a = new NbtReader(packet);


            Tag = ;

            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            return state;
        }
    }
}
