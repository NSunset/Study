using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fleck;

namespace WebSocketServer
{
    public class SocketHelp
    {
        private Fleck.WebSocketServer WebSocketServer ;

        private List<Fleck.IWebSocketConnection> webSockets = new List<IWebSocketConnection>();

        public SocketHelp(string location)
        {
            WebSocketServer = new Fleck.WebSocketServer(location);
        }

        public void WebSocketInit()
        {
            WebSocketServer.Start(socketConnection =>
            {
                socketConnection.OnClose = () =>
                {
                    Console.WriteLine("连接关闭");
                    webSockets.Remove(socketConnection);
                };
                socketConnection.OnOpen = () =>
                {
                    Console.WriteLine("开启连接");
                    webSockets.Add(socketConnection);
                };
                socketConnection.OnError = ex =>
                {
                    Console.WriteLine("报错了,服务端关闭连接");
                    socketConnection.Close();
                };
                socketConnection.OnMessage = clientMsg =>
                {
                    Console.WriteLine($"接收客户端的信息{clientMsg}");
                    socketConnection.Send($"返回给客户端信息:{clientMsg}");
                };
            });
        }
    }
}
