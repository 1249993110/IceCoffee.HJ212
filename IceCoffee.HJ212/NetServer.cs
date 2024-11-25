using IceCoffee.HJ212.Models;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IceCoffee.HJ212
{
    public abstract class NetServer : IDisposable
    {
        private readonly TcpListener _tcpListener;
        private volatile bool _isListening;
        private readonly ConcurrentDictionary<IPEndPoint, NetSession> _sessions = new ConcurrentDictionary<IPEndPoint, NetSession>();
        private readonly Encoding _encoding;

        public TcpListener TcpListener => _tcpListener;
        public bool IsListening => _isListening;
        public IReadOnlyDictionary<IPEndPoint, NetSession> Sessions => _sessions;
        public Encoding Encoding => _encoding;

        public NetServer(int port, Encoding? encoding = null)
        {
            _tcpListener = new TcpListener(IPAddress.Any, port);
            _tcpListener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            _tcpListener.Server.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

            _encoding = encoding ?? Encoding.UTF8;
        }
        public NetServer(IPAddress localaddr, int port, Encoding? encoding = null)
        {
            _tcpListener = new TcpListener(localaddr, port);
            _tcpListener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            _tcpListener.Server.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            _encoding = encoding ?? Encoding.UTF8;
        }

        protected abstract NetSession CreateSession(TcpClient client, NetServer server);
        protected virtual void OnStarted() { }
        protected virtual void OnStopped() { }
        protected virtual void OnConnected(NetSession session) { }
        protected virtual void OnDisconnected(NetSession session) { }
        protected virtual void OnError(Exception exception) { }

        public virtual void Start()
        {
            _tcpListener.Start();
            _isListening = true;
            AcceptClientsAsync();
            OnStarted();
        }

        public virtual void Start(int backlog)
        {
            _tcpListener.Start(backlog);
            _isListening = true;
            AcceptClientsAsync();
            OnStarted();
        }

        public virtual void Stop()
        {
            _isListening = false;
            _tcpListener.Stop();
            DisconnectAll();
            OnStopped();
        }

        public virtual void DisconnectAll()
        {
            // Disconnect all sessions
            foreach (var session in _sessions.Values)
            {
                session.Close();
            }
        }
        
        private async void AcceptClientsAsync()
        {
            while (_isListening)
            {
                try
                {
                    var tcpClient = await _tcpListener.AcceptTcpClientAsync();
                    ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(HandleClientAsync), tcpClient);
                }
                catch (SocketException ex)
                {
                    var error = ex.SocketErrorCode;
                    // Ignore disconnect errors
                    if ((error == SocketError.ConnectionAborted) 
                        || (error == SocketError.ConnectionRefused) 
                        || (error == SocketError.ConnectionReset) 
                        || (error == SocketError.OperationAborted)
                        || (error == SocketError.Shutdown))
                        return;
                    OnError(new Exception("Error in NetServer.AcceptClientsAsync", ex));
                }
                catch (Exception ex)
                {
                    OnError(new Exception("Error in NetServer.AcceptClientsAsync", ex));
                }
            }
        }

        private async void HandleClientAsync(object? state)
        {
            var tcpClient = (TcpClient)state!;
            var socket = tcpClient.Client;
            IPEndPoint remoteEndPoint = (IPEndPoint)socket.RemoteEndPoint!;
            try
            {
                using (tcpClient)
                {
                    var session = CreateSession(tcpClient, this);
                    _sessions.TryAdd(remoteEndPoint, session);
                    session.OnConnected();
                    OnConnected(session);

                    using (var networkStream = tcpClient.GetStream())
                    {
                        using (var reader = new StreamReader(networkStream, _encoding, false, 1024, true))
                        {
                            while (true)
                            {
                                string? rawText = await reader.ReadLineAsync();
                                if (rawText != null)
                                {
                                    rawText += NetPackage.FixedTail;
                                    if (rawText.Length < 64 || rawText.Substring(0, 2) != NetPackage.FixedHead) // '##'
                                    {
                                        session.OnReceived(null, rawText);
                                        throw new Exception("异常TCP连接");
                                    }
                                    else
                                    {
                                        var netPackage = NetPackage.Parse(rawText, session.GetUnpackCache);
                                        session.OnReceived(netPackage, rawText);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // Ignore
            }
            catch (Exception ex)
            {
                OnError(new Exception($"Error in NetServer.HandleClientAsync, IP: {remoteEndPoint}", ex));
            }
            finally
            {
                if (_sessions.TryRemove(remoteEndPoint, out var session))
                {
                    session.OnDisconnected();
                    OnDisconnected(session);
                }
            }
        }

        #region IDisposable Support
        public bool IsDisposed { get; private set; }
        ~NetServer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    Stop();
                }

                IsDisposed = true;
            }
        }
        #endregion
    }
}
