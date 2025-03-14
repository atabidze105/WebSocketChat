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
        public delegate void ClientWebsocketRecieveMessage(string message);
        public event ClientWebsocketRecieveMessage? OnRecieveMessage;

        private readonly Uri _Uri = new Uri("ws://localhost:5000/chat");
        private ClientWebSocket _WebSocket = new ClientWebSocket();

        public ClientWebsocketHandler()
        {
            _ = InitWebSocket();
        }

        private async Task InitWebSocket()
        {
            _WebSocket = new ClientWebSocket();
            await _WebSocket.ConnectAsync(_Uri, CancellationToken.None);
            while (_WebSocket.State == WebSocketState.Open)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte [1025*4]  );
                var result = await _WebSocket.ReceiveAsync(buffer, CancellationToken.None);
                OnRecieveMessage?.Invoke(Encoding.UTF8.GetString(buffer.Array, 0, result.Count));
            }
        }

        public async Task SendMessage(string message)
        {
            await _WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }
    }
}
