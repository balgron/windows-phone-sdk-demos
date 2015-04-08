using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AVOSCloud;
using AVOSCloud.RealtimeMessage;
using AVOSCloud.RealtimeMessageV2;
using LeanCloud.LeanMeaasge.Demo.Resources;
using System.Windows.Media.Animation;

namespace LeanCloud.LeanMeaasge.Demo
{
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Storyboard sbd = Resources["leafLeave"] as Storyboard;
            sbd.Begin();

            Storyboard baiyun = Resources["cloudMove"] as Storyboard;
            baiyun.Begin();
            if (AVUser.CurrentUser != null)
            {
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
                NavigationService.RemoveBackEntry();
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ///AVSession.SelfId 是标识 当前AVSession 的 PeerId，是跟Server 交互的唯一可靠的 ID，
            ///请开发者放心，这个Id 在不同应用之间是不会产生混淆的，你可以叫大卫，别的开发者也可以在他的 LeanCloud 的应用叫做大卫，而服务端会做区分。
            ///这里直接用名字作为 PeerId 是为了方便演示，出去了将PeerId 转化为 Display Name 的繁琐，但是实际运用的时候还是建议使用唯一性最好的GUID或者直接使用AVUser.objectId更为妥当。
            try
            {
                var user = await AVUser.LogInAsync(txb_selfId.Text.Trim(), txb_pwd.Password.Trim());

                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
                NavigationService.RemoveBackEntry();
            }
            catch
            {

            }

        }

        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/add.png", UriKind.Relative));
            appBarButton.Click += appBarButton_Click;
            appBarButton.Text = AppResources.ab_register;
            ApplicationBar.Buttons.Add(appBarButton);

            // Create a new menu item with the localized string from AppResources.
            //ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            //ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        void appBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SignUp.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}