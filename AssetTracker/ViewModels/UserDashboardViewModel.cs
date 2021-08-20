using Quipu.Services;
using Quipu.View.Commands;
using DataAccessLayer;
using DataAccessLayer.Services;
using DomainModel;
using DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Quipu.ViewModels
{
    public class UserDashboardViewModel : ViewModel
    {     
        /// <summary>
        /// User to bind to
        /// </summary>
        public User myUser { get; set; }

        /// <summary>
        /// Alerts for just this user to see
        /// </summary>
        public List<Alert> UserAlerts
        {
            get
            {
                return (from a in alertsRepo.Get()
                        where a.ar_usid == myUser.us_id &&
                        !a.ar_projectwide
                        orderby a.ar_viewed, a.ar_date
                        select a).ToList();
            }
        }

        /// <summary>
        /// These are alerts that are visible to everyone. Typically messages
        /// like 'Remember to keep your assets updated with the current status'
        /// </summary>
        public List<Alert> ProjectWideAlerts
        {
            get
            {
                return (from a in alertsRepo.Get()
                        where a.ar_usid != null &&
                        a.ar_projectwide
                        select a).ToList();
            }
        }

        /// <summary>
        /// These are alerts that give user update tothe current status of the
        /// project. Typically messages like 'We have gotten back onto our target!'
        /// </summary>
        public List<Alert> ProjectUpdateAlerts
        {
            get
            {
                return (from a in alertsRepo.Get()
                        where a.ar_asid == 0 &&
                        !a.ar_projectwide
                        select a).ToList();
            }
        }

        /// <summary>
        /// The amount of unviewed discussion alerts this user has
        /// </summary>
        public int DiscussionAlertCount => UserAlerts.Where(x => x.ar_type == AlertType.DiscussionReply && !x.ar_viewed).Count();
        /// <summary>
        /// The amount of unviewed assigned to asset alerts this user has
        /// </summary>
        public int AssetAlertCount => UserAlerts.Where(x => x.ar_type == AlertType.AssetAssigned && !x.ar_viewed).Count();
        /// <summary>
        /// The amount of unviewed reviews assigned to this user
        /// </summary>
        public int ReviewAlertCount => UserAlerts.Where(x => x.ar_type == AlertType.ReviewAssigned && !x.ar_viewed).Count();
        /// <summary>
        /// Whether or not this user has no alerts.
        /// </summary>
        public bool EmptyAlerts => UserAlerts.Count == 0;

        #region Commands
        public ICommand PrioritizeAlertCommand => new RelayCommand((s) => PrioritizeAlert((int) s), (s) => true);
        public ICommand ArchiveAlertCommand => new RelayCommand((s) => ArchiveAlert((int)s), (s) => true);
        public ICommand MarkAlertsAsRead => new RelayCommand((s) => MarkAsRead(), (s) => true);
        #endregion

        #region Repositories
        private IRepository<Alert> alertsRepo;
        private IRepository<Asset> assetRepo;
        #endregion

        /// <summary>
        /// Coordinator to use to navigate to other pages
        /// </summary>
        private readonly INavigationCoordinator navCoordinator;

        public UserDashboardViewModel(INavigationCoordinator coord, IUnitOfWork uow)
        {          
            myUser = MainViewModel.Instance.CurrentUser;
            navCoordinator = coord;
            unitOfWork = uow;

            alertsRepo = unitOfWork.GetRepository<Alert>();
            assetRepo = unitOfWork.GetRepository<Asset>();
        }

        /// <summary>
        /// Navigate to the page linked to the alert given
        /// </summary>
        /// <param name="alertid">Alert to navigate to</param>
        public void NavigateToAlert(int alertid)
        {
            Alert selectedAlert = UserAlerts.FirstOrDefault(x => x.ID == alertid);
            if (selectedAlert != null)
            {
                selectedAlert.ar_viewed = true;
                alertsRepo.Update(selectedAlert);
                unitOfWork.Commit();
                Asset connectedAsset = assetRepo.GetByID(selectedAlert.ar_asid);
                navCoordinator.NavigateToAssetDetail(connectedAsset);
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Prioritize the alert to highlight its importance
        /// </summary>
        /// <param name="alertid">Alert to prioritize</param>
        public void PrioritizeAlert(int alertid)
        {
            Alert selectedAlert = UserAlerts.FirstOrDefault(x => x.ID == alertid);
            if (selectedAlert != null)
            {
                selectedAlert.ar_priority = !selectedAlert.ar_priority;
                alertsRepo.Update(selectedAlert);
                unitOfWork.Commit();
                NotifyPropertyChanged("UserAlerts");
            }
        }

        /// <summary>
        /// Archive the alert by marking it unread
        /// </summary>
        /// <param name="alertid">Alert to archive</param>
        public void ArchiveAlert(int alertid)
        {
            Alert selectedAlert = UserAlerts.FirstOrDefault(x => x.ID == alertid);
            if (selectedAlert != null)
            {
                selectedAlert.ar_viewed = !selectedAlert.ar_viewed;
                alertsRepo.Update(selectedAlert);
                unitOfWork.Commit();
                NotifyPropertyChanged("UserAlerts");
            }
        }

        /// <summary>
        /// Mark all user alerts as read
        /// </summary>
        private void MarkAsRead()
        {
            foreach(Alert a in UserAlerts)
            {
                a.ar_viewed = true;
                alertsRepo.Update(a);
            }

            unitOfWork.Commit();

            NotifyPropertyChanged("UserAlerts");
        }

    }
}
