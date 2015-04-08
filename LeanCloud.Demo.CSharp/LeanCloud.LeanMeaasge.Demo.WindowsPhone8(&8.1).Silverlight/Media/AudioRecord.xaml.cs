using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Phone.Media.Capture;
using Windows.Storage.Streams;
using LeanCloud.LeanMeaasge.Demo.Resources;
using System.IO.IsolatedStorage;
using System.IO;

namespace LeanCloud.LeanMeaasge.Demo.Media
{
    public partial class AudioRecord : PhoneApplicationPage
    {
        PhoneApplicationService myService = PhoneApplicationService.Current;
        private string recordAudioFileName = "record_audio_temp.aac";
        IRandomAccessStream _randomAccessStream;
        AudioVideoCaptureDevice _mic;
        public AudioRecord()
        {
            InitializeComponent();

            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.ab_sendVideo);
            appBarMenuItem.Click += appBarMenuItem_Click;
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        private void appBarMenuItem_Click(object sender, EventArgs e)
        {
            myService.State["action"] = "Send_Audio";
            _randomAccessStream.CloneStream();
            NavigationService.GoBack();
        }

        public async Task StartRecordingAsync()
        {
            _mic = await AudioVideoCaptureDevice.OpenForAudioOnlyAsync();
            _mic.AudioEncodingFormat = CameraCaptureAudioFormat.Amr;
            try
            {
                await CreateFileStreamForAudioAsync(recordAudioFileName);
                await _mic.StartRecordingToStreamAsync(_randomAccessStream);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CreateFileStreamForAudioAsync(string fileName)
        {
            try
            {
                //var s = new IsolatedStorageFileStream(recordAudioFileName,
                //                           FileMode.Open, FileAccess.Read,
                //                           IsolatedStorageFile.GetUserStoreForApplication());
                
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                
                StorageFile file = file = await local.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                _randomAccessStream = await file.OpenAsync(FileAccessMode.ReadWrite);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void StopRecord()
        {
            await _mic.StopRecordingAsync();
            _randomAccessStream.CloneStream();
        }

        private async void StartRecording_Click(object sender, EventArgs e)
        {
            await StartRecordingAsync();
        }

        private void StopPlaybackRecording_Click(object sender, EventArgs e)
        {
            StopRecord();
        }
    }
}