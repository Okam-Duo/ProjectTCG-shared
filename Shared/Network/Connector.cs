using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Network
{
    public class Connector
    {
        private Func<Session> _sessionFactory;
        private Action<bool> _onConnected;

        //bool : 연결 성공 여부
        public Connector(Action<bool> onConnected = null)
        {
            _onConnected = onConnected;
        }

        public void Connect(IPEndPoint endPoint, Func<Session> sessionFactory)
        {
            Socket socket = new(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _sessionFactory += sessionFactory;

            SocketAsyncEventArgs args = new();
            args.Completed += OnConnectCompleted;
            args.RemoteEndPoint = endPoint;
            args.UserToken = socket;

            RegisterConnect(args);
        }

        void RegisterConnect(SocketAsyncEventArgs args)
        {
            Socket socket = args.UserToken as Socket;
            if (socket == null)
            {
                return;
            }

            bool pending = socket.ConnectAsync(args);
            if (pending == false)
            {
                OnConnectCompleted(null, args);
            }
        }

        void OnConnectCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                Session session = _sessionFactory.Invoke();
                session.Start(args.ConnectSocket);
                session.OnConnected(args.RemoteEndPoint);

                _onConnected?.Invoke(true);
            }
            else
            {
                Console.WriteLine($"OnConnectCompleted Fail: {args.SocketError}");
                _onConnected?.Invoke(false);
            }
        }
    }
}
