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
    class PlayerSpawn : IPacket
    {
        public int Id;
        public Vector3 Position;
        public Vector2 Look;

        public PlayerSpawn() {}

        public NetworkState ReadPacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            Id = packet.ReadVarInt();
            //this.uuid = packet.readUuid();
            var x = packet.ReadDouble();
            var y = packet.ReadDouble();
            var z = packet.ReadDouble();

            Position = new Vector3(Convert.ToSingle(x), Convert.ToSingle(y), Convert.ToSingle(z));

            var yaw = packet.ReadByte();
            var pitch = packet.ReadByte();

            Look = new Vector2(yaw, pitch);

            return state;
        }

        public NetworkState WritePacket(PacketByteBuf packet, NetworkState state, NetworkSide side)
        {
            packet.WriteVarInt(Id);
            //packet.Write(this.uuid);
            packet.WriteDouble(Position.x);
            packet.WriteDouble(Position.y);
            packet.WriteDouble(Position.z);
            packet.WriteByte(Convert.ToByte(Look.x));
            packet.WriteByte(Convert.ToByte(Look.y));

            return state;
        }
    }
}
