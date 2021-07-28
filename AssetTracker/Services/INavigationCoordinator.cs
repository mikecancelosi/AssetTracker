using AssetTracker.Model;
using AssetTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.Services
{

    public interface INavigationCoordinator
    {
        event Action<ViewModel> UserNavigationAttempt;
        event Action<ViewModel> VMChanged;

        ViewModel CurrentVM { get; set; }

        void RequestNavigationTo(ViewModel vm);
        bool WaitingToNavigate { get; }
        void NavigateToQueued();



        void NavigateToLogin();
        void NavigateToCreateUser();
        void NavigateToUserEdit(User userToEdit);
        void NavigateToAssetDetail(Asset asset);
        void NavigateToUserDashboard();
        void NavigateToCreateRole();
        void NavigateToRoleEdit(SecRole roleToEdit);
        void NavigateToProjectSettings();
        void NavigateToAssetList();
        void NavigateToCreateCategory();
        void NavigatetoCategoryEdit(AssetCategory cat);
    }
}
