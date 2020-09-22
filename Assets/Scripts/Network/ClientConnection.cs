using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using Assets.Scripts.Packets;
using Assets.Scripts.Packets.Both;
using Assets.Scripts.Packets.C2S;
using Assets.Scripts.Packets.S2C;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Assets.Scripts.Network
{
    class ClientConnection
    {
        public static ClientConnection Instance { get; set; }
        public Session Session { get; set; }
        public TcpClient Connection { get; set; }
        private ManualResetEvent NetworkingReset { get; set; }
        protected internal NetworkStream NetworkStream { get; set; }
        protected internal NetworkManager NetworkManager { get; set; }
        private Thread NetworkWorkerThread { get; set; }
        public ConcurrentQueue<IPacket> PacketQueue { get; set; }

        private static readonly Dictionary<Type, PacketHandler> PacketHandlers = new Dictionary<Type, PacketHandler>();

        public static EventHandler<IPacket> PacketReceived;
        public delegate void PacketHandler(ClientConnection client, IPacket packet);

        public ClientConnection(Session session)
        {
            Session = session;
            PacketQueue = new ConcurrentQueue<IPacket>();
            Instance = this;
            Packets.PacketHandler.Invoke(this);
        }

        public void RegisterPacketHandler(Type packetType, PacketHandler handler)
        {
            if (packetType.GetInterfaces().All(p => p != typeof(IPacket)))
                throw new InvalidOperationException("IPacket");

            PacketHandlers[packetType] = handler;
        }
        public void Connect(string address, int port)
        {
            Connection = new TcpClient();
            Connection.Connect(address, port);
            NetworkStream = Connection.GetStream();
            NetworkManager = new NetworkManager(NetworkStream);
            NetworkingReset = new ManualResetEvent(true);
            NetworkWorkerThread = new Thread(NetworkWorker);
            NetworkWorkerThread.Start();
            var handshake = new Handshaking(NetworkManager.Protocol, address, (ushort)port, NetworkState.Login);
            SendPacket(handshake);
            //var queue = new StatusRequest();
            //SendPacket(queue);
            var login = new LoginStart(Session.SelectedProfile.Name);
            SendPacket(login);

            Debug.Log("Hello");
        }

        public void SendPacket(IPacket packet)
        {
            PacketQueue.Enqueue(packet);
        }

        private void NetworkWorker()
        {
            while (true)
            {
                while (PacketQueue.Count != 0)
                {
                    if (PacketQueue.TryDequeue(out IPacket packet))
                    {
                        NetworkManager.WritePacket(packet, NetworkSide.Serverbound);
                    }
                }

                var readTimeout = DateTime.Now.AddMilliseconds(20);
                while (NetworkStream.DataAvailable && DateTime.Now < readTimeout)
                {
                    try
                    {
                        var packet = NetworkManager.ReadPacket(NetworkSide.Clientbound);

                        var type = packet.GetType();

                        PacketReceived?.Invoke(this, packet);

                        if (PacketHandlers.ContainsKey(type))
                        {
                            PacketHandlers[type](this, packet);
                        }
                        //NetworkManager.HandlePacket(packet);

                        if (packet is Disconnect sender)
                        {
                            Debug.LogWarning(sender.Reason);
                            return;
                        }
                    }
                    catch (Exception e) 
                    {
                         // TODO: OnNetworkException or something
                        Debug.LogError(e);
                    }
                }
                NetworkingReset.Set();
                NetworkingReset.Reset();
                Thread.Sleep(1);
            }
        }
    }
}
