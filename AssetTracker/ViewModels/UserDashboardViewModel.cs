using AssetTracker.Enums;
using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                return (from a in context.Alerts
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
                return (from a in context.Alerts
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
                return (from a in context.Alerts
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
        private readonly INavigationCoordinator navCoordinator;
        public UserDashboardViewModel(INavigationCoordinator coord)
        {          
            myUser = MainViewModel.Instance.CurrentUser;
            navCoordinator = coord;
            PrioritizeAlertCommand = new RelayCommand((s) => PrioritizeAlert((int)s), (s) => true);
            ArchiveAlertCommand = new RelayCommand((s) => ArchiveAlert((int)s), (s) => true);
        }

        public void NavigateToAlert(int alertid)
        {
            Alert selectedAlert = UserAlerts.FirstOrDefault(x => x.ID == alertid);
            if (selectedAlert != null)
            {
                context.Entry(selectedAlert).Property(x => x.ar_viewed).CurrentValue = true;
                context.SaveChanges();
                Asset connectedAsset = context.Assets.Find(selectedAlert.ar_asid);
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
                context.Entry(selectedAlert).Property(x => x.ar_priority).CurrentValue = !selectedAlert.ar_priority;
                context.SaveChanges();
                NotifyPropertyChanged("UserAlerts");
            }
        }

        public void ArchiveAlert(int alertid)
        {
            Alert selectedAlert = UserAlerts.FirstOrDefault(x => x.ID == alertid);
            if (selectedAlert != null)
            {
                context.Entry(selectedAlert).Property(x => x.ar_viewed).CurrentValue = !selectedAlert.ar_viewed;
                context.SaveChanges();
                NotifyPropertyChanged("UserAlerts");
            }

        }

        private void MarkAsRead()
        {
            foreach(Alert a in UserAlerts)
            {
                a.ar_viewed = true;
            }
            context.SaveChanges();
            NotifyPropertyChanged("UserAlerts");
        }

    }
}
