using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LeanCloud.LeanMeaasge.Demo.Resources;
using AVOSCloud.RealtimeMessageV2;
using AVOSCloud;
using LeanCloud.LeanMeaasge.Demo.ViewModels;
using LeanCloud.LeanMeaasge.Demo.Security;
using Windows.Storage;
using System.IO;

namespace LeanCloud.LeanMeaasge.Demo
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string recordVideoFileName = "record_video_temp.mp4";
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            App.ViewModel.CurrentClient = new AVIMClient(AVUser.CurrentUser.ObjectId);

            #region 如果开启了聊天的签名，请执行以下代码
            //App.ViewModel.CurrentClient.SignatureFactory = new SampleSignatureFactory();
            #endregion
            appBarList = new List<ApplicationBar>(main_pivot.Items.Count);

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            //SendAudio();
        }
        private async void SendAudio()
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            var AudioFile = await local.OpenStreamForReadAsync(recordVideoFileName);
            AVFile testFile = new AVFile(recordVideoFileName, AudioFile);
            await testFile.SaveAsync();

        }
        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        // Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/Logout.png", UriKind.Relative));
            appBarButton.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(appBarButton);

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }
        List<ApplicationBar> appBarList { get; set; }
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pIndex = ((Pivot)sender).SelectedIndex;

            if (appBarList.Count > pIndex)
            {
                var appBar = appBarList[pIndex];

                if (appBar != null)
                {
                    if (appBar.Buttons.Count > 0 || appBar.MenuItems.Count > 0)
                    {
                        ApplicationBar = appBar;
                        return;
                    }
                    else
                    {
                        ApplicationBar = null;
                    }
                }
            }
            switch (pIndex)
            {
                case 0:
                    {
                        appBarList.Add(new ApplicationBar());
                    }
                    break;
                case 1:
                    {
                        appBarList.Add(new ApplicationBar());
                    }
                    break;
                case 2:
                    {
                        var appBar_AboutMe = new ApplicationBar() { };
                        ApplicationBarIconButton logout_appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.door.leave.png", UriKind.Relative));
                        logout_appBarButton.Text = AppResources.ab_logout;

                        logout_appBarButton.Click += logout_appBarButton_Click;
                        appBar_AboutMe.Buttons.Add(logout_appBarButton);

                        appBarList.Add(appBar_AboutMe);

                        ApplicationBar = appBar_AboutMe;
                    }
                    break;

            }
        }

        void logout_appBarButton_Click(object sender, EventArgs e)
        {
            AVUser.LogOut();
            App.ViewModel.IsDataLoaded = false;
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.RelativeOrAbsolute));
            NavigationService.RemoveBackEntry();
        }

        private async void lls_contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lls_contacts.SelectedItem == null)
                return;

            var contact = e.AddedItems[0] as DemoContact;//选择的联系人

            var conversations = await App.ViewModel.CurrentClient.GetQuery().WhereContains("m", contact.UserId).FindAsync();//查询之前是否有过对话
            AVIMConversation conversation = null;
            if (conversations.Count() > 0)
            {
                conversation = conversations.FirstOrDefault();//如果有就不用创建新的。
            }
            else
            {
                conversation = await App.ViewModel.CurrentClient.CreateConversationAsync(contact.UserId, "xxxx", new Dictionary<string, object>() { { "type", "private" } });//创建新的对话，并且加上自定义的属性标识它为一个 私有的对话（私聊）
            }
            var existVMs = App.ViewModel.ConversationsListVM.Where(t =>
                {
                    if (t.AVConversation == null)
                        return false;
                    return t.AVConversation.ConversationId == conversation.ConversationId;
                }
                );
            ConversationViewModel existVM = null;
            if (existVMs.Count() == 0)
            {
                existVM = new ConversationViewModel() { AVConversation = conversation };
                App.ViewModel.ConversationsListVM.Add(existVM);
            }
            else
            {
                existVM = existVMs.FirstOrDefault();
            }
            App.ViewModel.SelectedConversationVM = existVM;

            lls_contacts.SelectedItem = null;
            NavigationService.Navigate(new Uri("/Conversation.xaml", UriKind.RelativeOrAbsolute));


        }

        private void lls_conversation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lls_conversation.SelectedItem == null)
                return;
            var existVM = e.AddedItems[0] as ConversationViewModel;
            App.ViewModel.SelectedConversationVM = existVM;
            lls_conversation.SelectedItem = null;
            NavigationService.Navigate(new Uri("/Conversation.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}