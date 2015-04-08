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

namespace LeanCloud.LeanMeaasge.Demo
{
    public partial class SignUp : PhoneApplicationPage
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private async void btn_stepIn_Click(object sender, RoutedEventArgs e)
        {
            if (txb_password.Text.Trim() == txb_password_verify.Text.Trim())
            {
                AVUser user = new AVUser() { Username = txb_username.Text.Trim(), Password = txb_password.Text.Trim() };
                await user.SignUpAsync();
                await AVUser.LogInAsync(txb_username.Text.Trim(), txb_password.Text.Trim());
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }
    }
}