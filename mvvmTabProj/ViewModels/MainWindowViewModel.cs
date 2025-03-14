using System;
using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mvvmTabProj.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly Uri uri = new Uri("ws://217.114.2.102:8088/reviews");
        public ObservableCollection<string> Messages { get; set; } = new();

        public MainWindowViewModel()
        {
            Task.Run(async () => await Connect());
        }

        private async Task Connect()
        {
            using var clientWebSocket = new ClientWebSocket();
            await clientWebSocket.ConnectAsync(uri, CancellationToken.None);
            while (clientWebSocket.State == WebSocketState.Open)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024 * 4]);
                var result = await clientWebSocket.ReceiveAsync(buffer, CancellationToken.None);
                Messages.Add(Encoding.UTF8.GetString(buffer.Array, 0, result.Count));
            }
        }
    }
}
