using AssetTracker.Enums;
using AssetTracker.Model;
using AssetTracker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                        select a).ToList();
            }
        }

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

        private readonly INavigationCoordinator navCoordinator;
        public UserDashboardViewModel(INavigationCoordinator coord)
        {          
            myUser = MainViewModel.Instance.CurrentUser;
            navCoordinator = coord;
        }

        public int DiscussionAlertCount=> UserAlerts.Where(x=>x.ar_type == AlertType.DiscussionReply).Count();
        public int AssetAlertCount => UserAlerts.Where(x => x.ar_type == AlertType.AssetAssigned).Count();
        public int ReviewAlertCount => UserAlerts.Where(x => x.ar_type == AlertType.ReviewAssigned).Count();
        public bool EmptyAlerts =>UserAlerts.Count == 0;
    }
}
