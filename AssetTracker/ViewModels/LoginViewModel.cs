using AssetTracker.Services;
using DataAccessLayer;
using DomainModel;
using System.Linq;

namespace AssetTracker.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

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

        private GenericRepository<User> userRepo;

        private INavigationCoordinator navCoordinator;
        public LoginViewModel(INavigationCoordinator coord, GenericUnitOfWork uow)
        {
            Email = "";
            Password = "";
            navCoordinator = coord;
            unitOfWork = uow;
            userRepo = unitOfWork.GetRepository<User>();
        }

        public enum LoginCode
        {
            Username,
            Password,
            Success
        }
    }
}
