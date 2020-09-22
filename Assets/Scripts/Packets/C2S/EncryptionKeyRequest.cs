using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Network;

namespace Assets.Scripts.Packets.C2S
{
    class EncryptionKeyRequest : IPacket
    {
        public string ServerId;
        public byte[] PublicKey;
        public byte[] VerificationToken;

        public EncryptionKeyRequest(string serverId, byte[] publicKey, byte[] verificationToken)
        {
            ServerId = serverId;
            PublicKey = publicKey;
            VerificationToken = verificationToken;
        }

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            ServerId = packet.ReadString();
            var pkLength = packet.ReadInt16();
            PublicKey = packet.ReadUInt8Array(pkLength);
            var vtLength = packet.ReadInt16();
            VerificationToken = packet.ReadUInt8Array(vtLength);
            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            packet.WriteString(ServerId);
            packet.WriteInt16((short)PublicKey.Length);
            packet.WriteUInt8Array(PublicKey);
            packet.WriteInt16((short)VerificationToken.Length);
            packet.WriteUInt8Array(VerificationToken);
            return state;
        }
    }
}
