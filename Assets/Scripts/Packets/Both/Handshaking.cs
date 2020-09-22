using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Network;
using UnityEngine;

namespace Assets.Scripts.Packets.Both
{
    class Handshaking : IPacket
    {
        public int ProtocolVersion;
        public string ServerHostname;
        public ushort ServerPort;
        public NetworkState NextState;

        public Handshaking(int protocolVersion, string hostname, ushort port, NetworkState nextState)
        {
            ProtocolVersion = protocolVersion;
            ServerHostname = hostname;
            ServerPort = port;
            NextState = nextState;
        }

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {

            try
            {
                ProtocolVersion = packet.ReadVarInt();
                ServerHostname = packet.ReadString();
                ServerPort = packet.ReadUInt16();
                NextState = (NetworkState)packet.ReadVarInt();
            }
            catch (Exception e)
            {
                Debug.LogError($"[ReadPacket] - {e.Message}");
            }

            return NextState;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            try
            {
                packet.WriteVarInt(ProtocolVersion);
                packet.WriteString(ServerHostname);
                packet.WriteUInt16(ServerPort);
                packet.WriteVarInt((int)NextState);
            }
            catch (Exception e)
            {
                Debug.LogError($"[WritePacket] - {e.Message}");
            }

            return NextState;
        }
    }
}
