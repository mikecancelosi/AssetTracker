using DomainModel;

namespace AssetTracker.ViewModels
{
    public class MainViewModel : ViewModel
    {
        public User CurrentUser { get; set; }

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

        public void LoginUser(User user)
        {
            CurrentUser = user;
            NotifyPropertyChanged("CurrentUser");
        }

        public void LogoutUser()
        {
            CurrentUser = null;
            NotifyPropertyChanged("CurrentUser");
        }
    }
}
