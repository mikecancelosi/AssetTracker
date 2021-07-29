using AssetTracker.Model;
using AssetTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginCode LoginUser()
        {
            User user = (from u in context.Users
                         where u.us_email == Email
                         select u).FirstOrDefault();
            if(user != null)
            {
                if (user.us_password != Password)
                {
                    return LoginCode.Password;
                }

                MainViewModel.Instance.LoginUser(user);
                navCoordinator.NavigateToUserDashboard();
                return LoginCode.Success;                
            }
            else
            {
                return LoginCode.Username;
            }
                        
        }

        private NavigationCoordinator navCoordinator;
        public LoginViewModel(NavigationCoordinator coord)
        {
            Email = "";
            Password = "";
            navCoordinator = coord;
        }

        public enum LoginCode
        {
            Username,
            Password,
            Success
        }
    }
}
