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
    class LoginCompression : IPacket
    {
        public int compressionThreshold;

        public LoginCompression() {}

        public LoginCompression(int compressionThreshold)
        {
            this.compressionThreshold = compressionThreshold;
        }

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            compressionThreshold = packet.ReadVarInt();
            Debug.LogWarning($"JDJDDJJDDJJD {compressionThreshold}");
            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            packet.WriteVarInt(compressionThreshold);
            return state;
        }
    }
}
