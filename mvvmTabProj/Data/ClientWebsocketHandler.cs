using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mvvmTabProj.ViewModels
{
    class ClientWebsocketHandler
    {
        private readonly Uri _Uri = new Uri("ws://217.114.2.102:8088/reviews");
        private ClientWebSocket _WebSocket/* = new ClientWebSocket()*/;

        private async void InitWebSocket()
        {
            _WebSocket = new ClientWebSocket();
            await _WebSocket.ConnectAsync(_Uri, CancellationToken.None);
            while (_WebSocket.State == WebSocketState.Open)
            {
                //ArraySegment<byte> buffer = new ArraySegment<byte>(new byte { 102504 });
            }
        }
    }
}
