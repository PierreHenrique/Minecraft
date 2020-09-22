using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Network;
using Assets.Scripts.Packets;
using UnityEngine;

namespace Assets.Scripts.Packets.S2C
{
    class LoginStart : IPacket
    {
        public string Username;

        public LoginStart(string username)
        {
            Username = username;
        }

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            Username = packet.ReadString();

            Debug.Log($"AHHAHAAAJAJA {Username}");
            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            packet.WriteString(Username);
            return state;
        }
    }
}
