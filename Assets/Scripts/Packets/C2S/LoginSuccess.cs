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
    class LoginSuccess : IPacket
    {
        public string UUID;
        public string Username;

        public LoginSuccess() {}

        public LoginSuccess(string uuid, string username)
        {
            UUID = uuid;
            Username = username;
        }

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            Debug.LogWarning("LoginSuccess");

            //try
            //{
            //    Debug.LogWarning("A");
            //    UUID = packet.ReadString();
            //    Debug.LogWarning(UUID);
            //    Username = packet.ReadString();
            //    Debug.LogWarning(Username);
            //}
            //catch (Exception e)
            //{
            //    Debug.LogWarning($"aa: {e.Message}");
            //}

            return NetworkState.Play;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            packet.WriteString(UUID);
            packet.WriteString(Username);
            return NetworkState.Play;
        }
    }
}
