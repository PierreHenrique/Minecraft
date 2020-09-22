using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Network;

namespace Assets.Scripts.Packets.S2C
{
    class TeleportConfirm : IPacket
    {
        public int TeleportId;

        public TeleportConfirm() {}

        public TeleportConfirm(int l)
        {
            TeleportId = l;
        }

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            TeleportId = packet.ReadVarInt();

            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            packet.WriteVarInt(TeleportId);

            return state;
        }
    }
}
