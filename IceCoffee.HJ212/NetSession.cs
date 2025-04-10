using System.Net;
using System.Net.Sockets;
using System.Text;
using IceCoffee.HJ212.Models;

namespace IceCoffee.HJ212
{
    public abstract class NetSession
    {
        private readonly TcpClient _client;
        private readonly NetServer _server;
        private readonly DateTime _connectedTime;
        private readonly IPEndPoint _remoteEndPoint;
        private StringBuilder? _unpackCache;

        public TcpClient Client => _client;
        public NetServer Server => _server;
        public DateTime ConnectedTime => _connectedTime;
        public IPEndPoint RemoteEndPoint => _remoteEndPoint;

        public NetSession(TcpClient client, NetServer server)
        {
            _client = client;
            _server = server;
            _connectedTime = DateTime.Now;
            _remoteEndPoint = (IPEndPoint)client.Client.RemoteEndPoint!;
        }

        /// <summary>
        /// 获取分包缓存
        /// </summary>
        /// <returns></returns>
        internal StringBuilder GetUnpackCache()
        {
            return _unpackCache ??= new StringBuilder();
        }

        protected internal virtual void OnConnected() { }
        protected internal virtual void OnDisconnected() { }
        protected internal virtual void OnReceived(NetPackage? netPackage, string rawText)
        {
        }
        protected virtual void OnSent(string rawText) { }

        public virtual void Send(NetPackage netPackage)
        {
            string rawText = netPackage.Serialize();
            Send(rawText);
        }

        public virtual void Send(string rawText)
        {
            byte[] data = _server.Encoding.GetBytes(rawText);
            _client.Client.Send(data);
            OnSent(rawText);
        }

        public virtual void Close()
        {
            _client.Client.Shutdown(SocketShutdown.Both);
            _client.Close();
        }
    }
}
