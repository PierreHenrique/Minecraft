using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Network;

namespace Assets.Scripts.Packets.Both
{
    class KeepAlive : IPacket
    {
        public long Id;

        public KeepAlive() {}

        public KeepAlive(long id)
        {
            Id = id;
        }

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            Id = packet.ReadInt64();

            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            packet.WriteInt64(Id);

            return state;
        }
    }
}
