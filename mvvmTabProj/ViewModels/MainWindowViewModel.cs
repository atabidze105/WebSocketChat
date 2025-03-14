using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mvvmTabProj.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly ClientWebsocketHandler _clientWebsocketHandler = new();
        public ObservableCollection<string> _Messages { get; set; } = new();

        [ObservableProperty]
        public string _currentMessage = String.Empty;




        public MainWindowViewModel()
        {
            _clientWebsocketHandler.OnRecieveMessage += DisplayMessage;
        }

        private void DisplayMessage( string message)
        {
            _Messages.Add(message);
        }

        public async Task SendMessage(string message)
        {
            _Messages.Add(message);
            await _clientWebsocketHandler.SendMessage(message);
        }
    }
}
