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
    class LoginDisconnect : IPacket
    {
        public string JsonData;

        public LoginDisconnect() {}
        public LoginDisconnect(string jsonData)
        {
            JsonData = jsonData;
        }

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            JsonData = packet.ReadString();

            Debug.LogWarning(JsonData);
            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            packet.WriteString(JsonData);
            return state;
        }
    }
}
