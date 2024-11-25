using IceCoffee.HJ212;
using IceCoffee.HJ212.Models;
using System.Net.Sockets;

namespace Demo
{
    internal class MySession : NetSession
    {
        public MySession(TcpClient client, NetServer server) : base(client, server)
        {
        }

        protected override void OnConnected()
        {
            Console.WriteLine($"会话开始: {RemoteEndPoint}");            
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine($"会话关闭: {RemoteEndPoint}");
        }

        protected override void OnReceived(NetPackage? netPackage, string rawText)
        {
            Console.WriteLine($"接收到: {RemoteEndPoint}: {rawText}");
            if(netPackage != null)
            {
                Send(netPackage.Clone());
            }
        }

        protected override void OnSent(string rawText)
        {
            Console.WriteLine($"发送给: {RemoteEndPoint}: {rawText}");
        }

    }
}
