using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using IceCoffee.HJ212.Models;
using IceCoffee.Common.Extensions;
using IceCoffee.FastSocket.Tcp;

namespace IceCoffee.HJ212
{
    public class NetSession : TcpSession
    {
        private StringBuilder _unpackCache;

        public NetSession(TcpServer server) : base(server)
        {
        }

        protected override void OnClosed()
        {
            base.OnClosed();
            _unpackCache?.Clear();
        }

        /// <summary>
        /// 获取分包缓存
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetUnpackCache()
        {
            return _unpackCache ??= new StringBuilder();
        }

        protected override void OnReceived()
        {
            if (ReadBuffer.IndexOf(35) != 0L)// '#'
            {
                throw new Exception("异常TCP连接 IP: " + RemoteIPEndPoint);
            }

            string rawText = null;
            while (ReadBuffer.CanReadLine)
            {
                try
                {
                    byte[] data = ReadBuffer.ReadLine();
                    rawText = Encoding.UTF8.GetString(data);
                    NetPackage netPackage = NetPackage.Parse(rawText, GetUnpackCache);
                    ((NetServer)Server).RaiseReceivedData(this, netPackage, rawText);
                }
                catch (Exception ex)
                {
                    throw new Exception("数据报：" + rawText, ex);
                }
            }
        }

        /// <summary>
        /// 应答
        /// </summary>
        public void Response(NetPackage netPackage)
        {
            string rawText = netPackage.Serialize();
            byte[] data = Encoding.UTF8.GetBytes(rawText);

            this.SendAsync(data);
            ((NetServer)Server).RaiseSendData(this, netPackage, rawText);
        }
    }

}
