using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Json;
using Assets.Scripts.Network;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.Packets.C2S
{
    class StatusResponse : IPacket
    {
        public ServerStatus Status;

        public StatusResponse()
        {
        }

        public StatusResponse(ServerStatus status)
        {
            Status = status;
        }

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            var s = packet.ReadString();
            Debug.LogWarning(s);

            Status = ServerStatus.FromJson(s);
            return NetworkState.Login;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            packet.WriteString(JsonConvert.SerializeObject(Status));
            return state;
        }
    }
}
