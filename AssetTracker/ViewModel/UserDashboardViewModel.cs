using AssetTracker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssetTracker.ViewModel
{
    public class UserDashboardViewModel : ViewModel
    {     
        private User myuser;
        public User myUser
        {
            get
            {
                return myuser;
            }
            set
            {
                myuser = context.Users.Find(value.ID);
            }
        }

        public List<Alert> UserAlerts
        {
            get
            {
                return (from a in context.Alerts
                        where a.ar_usid == myuser.us_id &&
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

        public UserDashboardViewModel()
        {
            context = new TrackerContext();
            myUser = MainViewModel.Instance.CurrentUser;
        }

        public int DiscussionAlertCount
        {
            get
            {
                return 3;
            }
        }

        public int AssetAlertCount
        {
            get
            {
                return 3;
            }
        }

        public int ReviewAlertCount
        {
            get
            {
                return 3;
            }
        }

        public bool EmptyAlerts
        {
            get
            {
                return UserAlerts.Count == 0;
            }
        }

    }
}
