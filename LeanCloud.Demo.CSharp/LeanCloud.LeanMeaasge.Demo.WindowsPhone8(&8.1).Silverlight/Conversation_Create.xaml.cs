using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LeanCloud.LeanMeaasge.Demo
{
    public partial class Conversation_Create : PhoneApplicationPage
    {
        public Conversation_Create()
        {
            InitializeComponent();

            BoundBuddies.ItemsSource = App.ViewModel.ContactsListVM;
        }
    }
}