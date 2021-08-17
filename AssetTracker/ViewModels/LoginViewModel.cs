using AssetTracker.Services;
using DataAccessLayer;
using DataAccessLayer.Services;
using DomainModel;
using System.Linq;

namespace AssetTracker.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        /// <summary>
        /// Email is the equivalent of a username
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Password given by user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Login in the user if a successful match was found, otherwise return error code
        /// </summary>
        /// <returns>Login return code</returns>
        public LoginCode LoginUser()
        {
            User user = (from u in userRepo.Get()
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

        /// <summary>
        /// Repo to check for validation of login information
        /// </summary>
        private IRepository<User> userRepo;

        /// <summary>
        /// Coordinator to navigate away on successful login 
        /// </summary>
        private INavigationCoordinator navCoordinator;
        public LoginViewModel(INavigationCoordinator coord, IUnitOfWork uow)
        {          
            navCoordinator = coord;
            unitOfWork = uow;

            userRepo = unitOfWork.GetRepository<User>();
            Email = "";
            Password = "";
        }

        /// <summary>
        /// Login return codes.
        /// </summary>
        public enum LoginCode
        {
            Username,
            Password,
            Success
        }
    }
}
