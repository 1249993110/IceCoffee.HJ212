using IceCoffee.HJ212;
using System.Net;
using System.Net.Sockets;

namespace Demo
{
    internal class MyServer : NetServer
    {
        public MyServer(int port) : base(port)
        {
        }

        protected override NetSession CreateSession(TcpClient client, NetServer server)
        {
            return new MySession(client, server);
        }

        protected override void OnStarted()
        {
            Console.WriteLine($"服务已启动, 端口: {((IPEndPoint)TcpListener.LocalEndpoint).Port}");
        }

        protected override void OnStopped()
        {
            Console.WriteLine("服务已停止");
        }

        protected override void OnError(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
