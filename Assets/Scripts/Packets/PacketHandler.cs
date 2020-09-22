using Assets.Scripts.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Packets.Both;
using Assets.Scripts.Packets.C2S;
using Assets.Scripts.Packets.S2C;
using UnityEngine;

namespace Assets.Scripts.Packets
{
    class PacketHandler
    {
        public static void Invoke(ClientConnection client)
        {
            client.RegisterPacketHandler(typeof(StatusResponse), (connection, packet) =>
            {
                Debug.Log("Handler");
                var login = new LoginStart(connection.Session.SelectedProfile.Name);
                connection.SendPacket(login);
            });

            client.RegisterPacketHandler(typeof(KeepAlive), (connection, packet) =>
            {
                if (packet is KeepAlive sender)
                {
                    Debug.Log("Bip bop");

                    connection.SendPacket(sender);
                }
            });
        }
    }
}
