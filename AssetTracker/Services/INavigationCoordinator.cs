using AssetTracker.ViewModels;
using DomainModel;
using System;

namespace AssetTracker.Services
{
    /// <summary>
    /// Navigation coordinator handles storing viewmodels if 
    /// navigation is requested but user needs to save before exiting current screen
    /// 
    /// Currently, this is exposed to our viewmodels in order
    /// to allow them to send a message to navigate away, ideally this will
    /// be removed/split from this class in order to have a singular responsbility
    /// 
    /// TODO: Split storing queue from calling navigatetoView
    /// </summary>
    public interface INavigationCoordinator
    {
        /// <summary>
        /// A user may want to navigate away through the main view
        /// or through navigation bar, but we want to prompt the user
        /// to save before exiting. This action fires at the intial request
        /// </summary>
        event Action<ViewModel> UserNavigationAttempt;
        /// <summary>
        /// This action fires when the current viewmodel actually changes, whether
        /// it was from the queue or initial user request to navigate away.
        /// </summary>
        event Action<ViewModel> VMChanged;

        /// <summary>
        /// Current viewmodel being displayed.
        /// </summary>
        ViewModel CurrentVM { get; set; }

        /// <summary>
        /// Request navigation should check if the current page is ready
        /// to be navigated away from, and either fire the event
        /// or navigate away
        /// </summary>
        /// <param name="vm">VM to navigate to</param>
        void RequestNavigationTo(ViewModel vm);
        /// <summary>
        /// Returns whether or not there's already an vm waiting
        /// to be navigate to.
        /// </summary>
        bool WaitingToNavigate { get; }
        /// <summary>
        /// When the view is ready to be navigated away from, 
        /// it should fire this event to proceed
        /// </summary>
        void NavigateToQueued();


        #region Navigate To Specified Screens
        void NavigateToLogin();
        void NavigateToCreateUser();
        void NavigateToUserEdit(User userToEdit);
        void NavigateToAssetDetail(Asset asset);
        void NavigateToCreateAsset();
        void NavigateToUserDashboard();
        void NavigateToCreateRole();
        void NavigateToRoleEdit(SecRole roleToEdit);
        void NavigateToProjectSettings();
        void NavigateToAssetList();
        void NavigateToCreateCategory();
        void NavigatetoCategoryEdit(AssetCategory cat);
        #endregion
    }
}
