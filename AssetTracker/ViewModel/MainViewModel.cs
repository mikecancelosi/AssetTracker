using AssetTracker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssetTracker.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private TrackerContext context = new TrackerContext();

        public User CurrentUser;

        private static MainViewModel instance;
        public static MainViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainViewModel();
                }

                return instance;
            }
        }

        public MainViewModel()
        {
            if(instance == null)
            {
                instance = this;
            }
            CurrentUser = context.Users.Find(1);
            NotifyPropertyChanged("CurrentUser");
            
        }
    }
}
