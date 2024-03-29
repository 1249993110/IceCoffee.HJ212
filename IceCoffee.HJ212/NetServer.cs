﻿using System;
using System.Net;
using System.Text;
using System.Timers;
using IceCoffee.HJ212.Models;
using IceCoffee.FastSocket;
using IceCoffee.FastSocket.Tcp;

namespace IceCoffee.HJ212
{
    public class NetServer : TcpServer
    {
        /// <summary>
        /// 收到数据事件
        /// </summary>
        public event Action<NetSession, NetPackage, string> ReceivedData;

        /// <summary>
        /// 发送数据事件
        /// </summary>
        public event Action<NetSession, NetPackage, string> SendData;

        public NetServer(IPAddress address, int port, TcpServerOptions options = null)
            : base(address, port, options ?? new TcpServerOptions() { KeepAlive = true })
        {
        }
        public NetServer(string address, int port, TcpServerOptions options = null)
            : base(address, port, options ?? new TcpServerOptions() { KeepAlive = true })
        {
        }
        public NetServer(IPEndPoint endPoint, TcpServerOptions options = null)
            : base(endPoint, options ?? new TcpServerOptions() { KeepAlive = true })
        {
        }

        protected override TcpSession CreateSession()
        {
            return new NetSession(this);
        }

        /// <summary>
        /// 引发收到数据事件
        /// </summary>
        internal void RaiseReceivedData(NetSession netSession, NetPackage netPackage, string rawText)
        {
            ReceivedData?.Invoke(netSession, netPackage, rawText);
        }

        /// <summary>
        /// 引发发送数据事件
        /// </summary>
        /// <param name="netSession"></param>
        /// <param name="netPackage"></param>
        /// <param name="rawText"></param>
        internal void RaiseSendData(NetSession netSession, NetPackage netPackage, string rawText)
        {
            SendData?.Invoke(netSession, netPackage, rawText);
        }
    }
}
