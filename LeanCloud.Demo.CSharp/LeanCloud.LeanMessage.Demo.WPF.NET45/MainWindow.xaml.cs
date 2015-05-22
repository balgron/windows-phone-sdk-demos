using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.WebSockets;
using System.Threading;

namespace LeanCloud.LeanMessage.Demo.WPF.NET45
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClientWebSocket wsc = new ClientWebSocket();
        private string puppet = "wss://puppet.avoscloud.com:5799/";
        private ArraySegment<byte> buffer;
        private string loginJson = "{\"i\":-65535,\"ua\":\"win/1.0.0.5\",\"cmd\":\"session\",\"op\":\"open\",\"peerId\":\"A\",\"appId\":\"uay57kigwe0b6f5n0e1d4z4xhydsml3dor24bzwvzr57wdap\"}";
        public MainWindow()
        {
            InitializeComponent();
            this.buffer = new ArraySegment<byte>(new byte[1024]);
        }

        private async void btn_connect_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri(puppet);
            await wsc.ConnectAsync(uri, CancellationToken.None);
            byte[] messageBytes = Encoding.UTF8.GetBytes(loginJson);
            await wsc.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);

            byte[] incomingData = new byte[1024];
            WebSocketReceiveResult result = await wsc.ReceiveAsync(new ArraySegment<byte>(incomingData), CancellationToken.None);
            string receivedText = Encoding.UTF8.GetString(incomingData, 0, result.Count);
            Console.WriteLine(receivedText);



            //Task.Factory.StartNew(async () =>
            //  {
                  
            //  }, TaskCreationOptions.LongRunning);
            //Task receiving = Task.Factory.StartNew(ReceiveData,
            //    result, TaskCreationOptions.LongRunning);
        }
        private async void ReceiveData(object state)
        {
            string fullMessage = string.Empty;
            bool completed = false;
            do
            {
                var result = (WebSocketReceiveResult)state;
                var message = Encoding.UTF8.GetString(this.buffer.Array, 0, result.Count);

                fullMessage += message;
                completed = result.EndOfMessage;
            }
            while (!completed);

            //return fullMessage;
        }


    }
}
