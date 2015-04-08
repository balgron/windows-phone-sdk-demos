using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AVOSCloud.RealtimeMessageV2;
using LeanCloud.LeanMeaasge.Demo.ViewModels;
using GalaSoft.MvvmLight.Threading;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Devices;
using Windows.Phone.Media.Capture;
using System.IO.IsolatedStorage;
using System.IO;
using Windows.Storage;
using Windows.Devices.Geolocation;
using System.Collections.ObjectModel;

namespace LeanCloud.LeanMeaasge.Demo
{
    public partial class Conversation : PhoneApplicationPage
    {
        PhoneApplicationService myService = PhoneApplicationService.Current;
        private string recordVideoFileName = "record_video_temp.mp4";
        private string recordAudioFileName = "record_audio_temp.aac";
        AVIMConversation conversation;
        ObservableCollection<DemoMessage> messages;
        public Conversation()
        {
            InitializeComponent();
            DataContext = App.ViewModel.SelectedConversationVM;
            conversation = App.ViewModel.SelectedConversationVM.AVConversation;
            messages = App.ViewModel.SelectedConversationVM.Messages;
            conversation.OnMessageReceived += AVConversation_OnMessageRecieved;
            conversation.OnTextMessageReceived += conversation_OnTextMessageReceived;
            conversation.OnImageMessageReceived += conversation_OnImageMessageReceived;
        }

        void conversation_OnTextMessageReceived(object sender, AVIMTextMessage e)
        {
            var text = e.TextContent;
        }

        void conversation_OnImageMessageReceived(object sender, AVIMImageMessage e)
        {
            var title = e.Title;
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string action = string.Empty;
            if (myService.State.ContainsKey("action"))
            {
                action = myService.State["action"].ToString();

                switch (action)
                {
                    case "Send_Video":
                        {
                            SendVideo();
                        }
                        break;

                    case "Send_Audio":
                        {

                            SendAudio();
                        }
                        break;
                }
            }
            base.OnNavigatedTo(e);

        }

        private async void SendAudio()
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            var AudioFile = await local.OpenStreamForReadAsync(recordAudioFileName);

            AVIMAudioMessage audioMessage = new AVIMAudioMessage(recordAudioFileName, AudioFile);

            await conversation.SendAudioMessageAsync(audioMessage);
        }

        private async void SendVideo()
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            var VideoFile = await local.OpenStreamForReadAsync(recordVideoFileName);

            AVIMVideoMessage videoMessage = new AVIMVideoMessage(recordVideoFileName, VideoFile);

            await conversation.SendVideoMessageAsync(videoMessage);
        }
            

        private void AVConversation_OnMessageRecieved(object sender, AVIMMessage e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                messages.Add(new DemoMessage(e) { DisplayTime = DateTime.Now });
            });
        }

        private async void btn_sendMsg_Click(object sender, RoutedEventArgs e)
        {
            await conversation.SendTextMessageAsync(txb_msg.Text).ContinueWith(t =>
            {
                messages.Add(new DemoMessage() { DisplayTime = DateTime.Now, From = "Me", TextContent = txb_msg.Text });
                txb_msg.Text = string.Empty;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void btn_addMembers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Conversation_Create.xaml", UriKind.RelativeOrAbsolute));
        }
        private void btn_sendImg_Click(object sender, RoutedEventArgs e)
        {
            MediaLibrary library = new MediaLibrary();
            var photo = library.Pictures[0];

            AVIMImageMessage imgMessage = new AVIMImageMessage(photo.Name, photo.GetImage());
            imgMessage.Attributes = new Dictionary<string, object>() 
            { 
              {"w","q"}
            };
            imgMessage.Title = "发自我的WP";
            conversation.SendImageMessageAsync(imgMessage);
        }

        private void btn_sendVideo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Media/VideoRecord.xaml", UriKind.Relative));
        }

        private void btn_sendAudio_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Media/AudioRecord.xaml", UriKind.Relative));
        }

        private async void btn_sendLocation_Click(object sender, RoutedEventArgs e)
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));
                AVIMLocationMessage locationMessage = new AVIMLocationMessage(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);
                await conversation.SendLocationMessageAsync(locationMessage);
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {

                    var error = "location  is disabled in phone settings.";
                }
            }
        }
    }
}