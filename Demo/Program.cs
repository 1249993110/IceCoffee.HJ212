using System.Net.Sockets;
using System.Text;

namespace Demo
{
    internal class Program
    {
        private const int port = 12345;
        //private static TcpClient client;

        static void Main(string[] args)
        {
            var server = new MyServer(port);
            server.Start();

            CreateClient();

            Console.ReadKey();
        }

        private static async void CreateClient()
        {
            string server = "127.0.0.1"; // 服务器 IP 地址

            try
            {
                // 1. 创建 TcpClient 并连接到服务器
                using (var tcpClient = new TcpClient())
                {
                    Console.WriteLine($"正在连接到 {server}:{port}...");
                    await tcpClient.ConnectAsync(server, port);
                    Console.WriteLine("连接成功！");

                    string message = "##0285QN=20190925181031464;ST=22;CN=2061;PW=BF470F88957588DE902D1A52;MN=Z13401000010301;Flag=5;CP=&&DataTime=20190924220000;a34006-Avg=2.69700,a34006-Flag=N;a34007-Avg=7.96600,a34007-Flag=N;a34048-Avg=3.30600,a34048-Flag=N;a34047-Avg=7.35700,a34047-Flag=N;a34049-Avg=10.66300,a34049-Flag=N&&C181\r\n";
                    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    tcpClient.Client.Send(buffer);

                    // 2. 获取网络流
                    using (var networkStream = tcpClient.GetStream())
                    {
                        // 3. 创建 StreamReader 以读取数据
                        using (var reader = new StreamReader(networkStream, Encoding.UTF8))
                        {
                            Console.WriteLine("等待接收数据...");

                            while (true)
                            {
                                // 4. 异步读取一行数据
                                string? line = await reader.ReadLineAsync();

                                // 5. 输出接收到的数据
                                if (line != null)
                                {
                                    Console.WriteLine($"接收到的数据: {line}");
                                }
                                else
                                {
                                    Console.WriteLine("连接已关闭或没有数据。");
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生异常: {ex}");
            }
        }
    }
}