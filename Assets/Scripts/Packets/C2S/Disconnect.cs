using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Network;

namespace Assets.Scripts.Packets.C2S
{
    class Disconnect : IPacket
    {
        public string Reason;

        public Disconnect() {}

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            Reason = packet.ReadString();
            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            packet.WriteString(Reason);
            return state;
        }
    }
}
