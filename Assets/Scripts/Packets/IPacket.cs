using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Network;

namespace Assets.Scripts.Packets
{
    interface IPacket
    {
        NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side);
        NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side);
    }
}
