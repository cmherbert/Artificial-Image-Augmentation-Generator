using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArtificalAugmentationGenerator.Components.LWSVR
{
    /// <summary>
    /// Light Weight TCP Server listener for inter process communication
    /// </summary>
    internal class LWSVR : IDisposable
    {
        UdpClient _client;
        Thread _listenThread;
        bool _shutdown = false;
        int _rport = 0;

        public int Port => ((IPEndPoint)_client.Client.LocalEndPoint).Port;

        public event ClientUpdateHandler ClientUpdate;

        public delegate void ClientUpdateHandler(object sender, int clientID, int clientMessage, int clientValue);
        public LWSVR()
        {
            _client = new UdpClient(new IPEndPoint(IPAddress.Loopback, 0));
            //_listener = new TcpListener(IPAddress.Loopback, 0);
            //_listener.Start();


        }
        public LWSVR(int serverPort)
        {
            _client = new UdpClient(new IPEndPoint(IPAddress.Loopback, 0));
            //_listener = new TcpListener(IPAddress.Loopback, 0);
            //_listener.Start();
            _rport = serverPort;
        }

        public void StartServer()
        {
            ////Start Listener Thread
            _listenThread = new Thread(ClientProcessThread);
            _listenThread.Start();
        }
        public void SendUpdate(int workerID, int value)
        {
            try
            {
                var bytes = new SPacket(workerID, 0, value).Pack();
                _client.SendAsync(bytes, bytes.Length, new IPEndPoint(IPAddress.Loopback, _rport));
            }
            catch { }
        }
        public void SendClosure(int workerID)
        {
            try
            {
                var bytes = new SPacket(workerID, 1, 0).Pack();
                _client.SendAsync(bytes, bytes.Length, new IPEndPoint(IPAddress.Loopback, _rport));
            }
            catch { }
        }
        public void Stop()
        {

            _listenThread.Abort();
            _listenThread = null;
            _client.Dispose();
        }



        private void ClientProcessThread()
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    var data = _client.Receive(ref endpoint);
                    var pack = SPacket.Unpack(data);
                    ClientUpdate?.Invoke(this, pack.WorkerID, pack.MessageID, pack.Value);
                }
                catch
                {

                }
            }
        }

        private void AddClient(TcpClient tcpClient)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (_listenThread != null)
                Stop();
        }

        public class SPacket
        {
            public int WorkerID { get; set; } = 0;
            public int MessageID { get; set; } = 0; //0 - update, 1 -- shutdown
            public int Value { get; set; } = 0;

            public SPacket(int worker, int msg, int value = 0)
            {
                WorkerID = worker;
                MessageID = msg;
                Value = value;
            }

            public byte[] Pack()
            {
                byte[] buffer = new byte[12];
                Buffer.BlockCopy(BitConverter.GetBytes(WorkerID), 0, buffer, 0, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(MessageID), 0, buffer, 4, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Value), 0, buffer, 8, 4);
                return buffer;
            }

            public static SPacket Unpack(byte[] data)
            {
                return new SPacket(BitConverter.ToInt32(data, 0), BitConverter.ToInt32(data, 4), BitConverter.ToInt32(data, 8));
            }
        }
    }
}
