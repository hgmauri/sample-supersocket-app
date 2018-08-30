using System;
using System.ServiceProcess;
using Socket.Domain.Infra;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketEngine;

namespace Socket.ConsoleApp
{
    public class Program : ServiceBase
    {
        private static MyAppServer appServer;

        private static void Main(string[] args)
        {
            appServer = new MyAppServer();

            var root = new RootConfig();
            var server = new ServerConfig();
            var factory = new SocketServerFactory();

            server.Name = "SuperWebSocket";
            server.ServerTypeName = "WebSocketService";
            server.Ip = "Any";
            server.Port = 3333;
            server.MaxRequestLength = 1024*1000*1000;
            server.ReceiveBufferSize = 1048576;
            server.MaxConnectionNumber = 100;
            server.SendingQueueSize = 3000;
            server.TextEncoding = "UTF-8";

            appServer.NewRequestReceived += appServer_NewRequestReceived;

            appServer.Setup(root, server, factory);

            appServer.Start();

            Console.ReadKey();
        }

        private static void appServer_NewRequestReceived(AppSession session, StringRequestInfo requestInfo)
        {
            //Checks are sent back message
            var result = session.TrySend("message received");

            session.Close(CloseReason.ClientClosing);
        }
    }
}