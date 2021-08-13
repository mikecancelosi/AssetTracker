using DomainModel;

namespace AssetTracker.ViewModels
{
    public class MainViewModel : ViewModel
    {
        //TODO: Remove singleton pattern and swap for passing around user when needed OR usermanager

        /// <summary>
        /// Logged in user
        /// </summary>
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

        /// <summary>
        /// Login user by setting Currentuser
        /// </summary>
        /// <param name="user"></param>
        public void LoginUser(User user)
        {
            CurrentUser = user;
            NotifyPropertyChanged("CurrentUser");
        }

        /// <summary>
        /// Remove current user value.
        /// </summary>
        public void LogoutUser()
        {
            CurrentUser = null;
            NotifyPropertyChanged("CurrentUser");
        }
    }
}
