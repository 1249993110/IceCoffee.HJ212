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

            string line = null;
            while (ReadBuffer.CanReadLine)
            {
                try
                {
                    byte[] data = ReadBuffer.ReadLine();
                    line = Encoding.UTF8.GetString(data);
                    NetPackage netPackage = NetPackage.Parse(line, GetUnpackCache);
                    ((NetServer)Server).RaiseReceivedData(this, line, netPackage);

                    if (netPackage.DataSegment.PackageFlag != null && netPackage.DataSegment.PackageFlag.A == 1)
                    {
                        Response(netPackage);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("数据报：" + line, ex);
                }
            }
        }

        /// <summary>
        /// 应答
        /// </summary>
        private void Response(NetPackage netPackage)
        {
            try
            {
                netPackage.DataSegment.ST = DataSegment.ResponseST;
                netPackage.DataSegment.CN = CommandNumber.DataResponse;
                netPackage.DataSegment.PackageFlag.A = 0;
                netPackage.DataSegment.PackageFlag.D = 0;
                netPackage.DataSegment.CpCommand.ExeRtn = ResponseCode.ExecSucceeded;

                string line = netPackage.Serialize();
                byte[] data = Encoding.UTF8.GetBytes(line);

                SendAsync(data);

                ((NetServer)Server).RaiseSendData(this, line);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in NetSession", ex);
            }
        }
    }

}
