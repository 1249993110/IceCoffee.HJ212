using IceCoffee.FastSocket.Tcp;
using IceCoffee.HJ212;
using IceCoffee.HJ212.Models;
using System.Net;
using System.Text;

namespace Test
{
    internal class Program
    {
        private const int port = 10000;
        private static IPAddress ip = IPAddress.Loopback;
        private static NetServer server;
        private static TcpClient client;

        static void Main(string[] args)
        {
            server = new NetServer(ip, port);
            server.Started += OnNetServer_Started;
            server.ExceptionCaught += OnNetServer_ExceptionCaught;
            server.SessionStarted += OnNetServer_SessionStarted;
            server.SessionClosed += OnNetServer_SessionClosed;
            server.ReceivedData += OnNetServer_ReceivedData;
            server.SendData += OnNetServer_SendData;
            server.Start();

            client = new TcpClient(ip, port);
            client.ConnectAsync();
            client.Connected += OnClient_Connected;
            
            Console.ReadKey();
        }

        private static void OnClient_Connected()
        {
            string message = "##0285QN=20190925181031464;ST=22;CN=2061;PW=BF470F88957588DE902D1A52;MN=Z13401000010301;Flag=5;CP=&&DataTime=20190924220000;a34006-Avg=2.69700,a34006-Flag=N;a34007-Avg=7.96600,a34007-Flag=N;a34048-Avg=3.30600,a34048-Flag=N;a34047-Avg=7.35700,a34047-Flag=N;a34049-Avg=10.66300,a34049-Flag=N&&C181\r\n";
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.SendAsync(data);
        }

        private static void OnNetServer_Started()
        {
           Console.WriteLine($"开始监听: {ip}:{port}");
        }

        private static void OnNetServer_SendData(NetSession session, NetPackage netPackage, string rawText)
        {
           Console.WriteLine($"发送给: {session.RemoteIPEndPoint}: {rawText}");
        }

        private static void OnNetServer_SessionClosed(TcpSession session)
        {
           Console.WriteLine("会话关闭: " + session.RemoteIPEndPoint + ", 当前会话总数: " + server.SessionCount);
        }

        private static void OnNetServer_SessionStarted(TcpSession session)
        {
           Console.WriteLine("会话开始: " + session.RemoteIPEndPoint + ", 当前会话总数: " + server.SessionCount);
        }

        private static void OnNetServer_ReceivedData(TcpSession session, NetPackage netPackage, string rawText)
        {
            Console.WriteLine("收到自: " + session.RemoteIPEndPoint + ": " + rawText);
        }

        private static void OnNetServer_ExceptionCaught(Exception ex)
        {
            Console.WriteLine("Error in NetServer" + ex);
        }
    }
}