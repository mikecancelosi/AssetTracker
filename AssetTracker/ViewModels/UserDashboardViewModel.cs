using AssetTracker.Services;
using AssetTracker.View.Commands;
using DataAccessLayer;
using DomainModel;
using DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AssetTracker.ViewModels
{
    public class UserDashboardViewModel : ViewModel
    {     
        public User myUser { get; set; }

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

        //TODO: Add 'nonviewed' count in upper right of each count
        public int DiscussionAlertCount => UserAlerts.Where(x => x.ar_type == AlertType.DiscussionReply).Count();
        public int AssetAlertCount => UserAlerts.Where(x => x.ar_type == AlertType.AssetAssigned).Count();
        public int ReviewAlertCount => UserAlerts.Where(x => x.ar_type == AlertType.ReviewAssigned).Count();
        public bool EmptyAlerts => UserAlerts.Count == 0;

        public ICommand PrioritizeAlertCommand { get; set; }
        public ICommand ArchiveAlertCommand { get; set; }
        public ICommand MarkAlertsAsRead => new RelayCommand((s) => MarkAsRead(), (s) => true);

        private GenericRepository<Alert> alertsRepo;
        private GenericRepository<Asset> assetRepo;

        private readonly INavigationCoordinator navCoordinator;
        public UserDashboardViewModel(INavigationCoordinator coord, GenericUnitOfWork uow)
        {          
            myUser = MainViewModel.Instance.CurrentUser;
            navCoordinator = coord;
            unitOfWork = uow;

            alertsRepo = unitOfWork.GetRepository<Alert>();
            assetRepo = unitOfWork.GetRepository<Asset>();


            PrioritizeAlertCommand = new RelayCommand((s) => PrioritizeAlert((int)s), (s) => true);
            ArchiveAlertCommand = new RelayCommand((s) => ArchiveAlert((int)s), (s) => true);
        }

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
