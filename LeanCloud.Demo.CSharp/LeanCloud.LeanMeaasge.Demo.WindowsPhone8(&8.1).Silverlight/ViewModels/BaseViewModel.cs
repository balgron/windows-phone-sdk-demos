using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeanCloud.LeanMeaasge.Demo.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void NotifyPropertyChangedOnUI(string propertyName)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() => 
            {
                NotifyPropertyChanged(propertyName);
            });
        }
    }
}
