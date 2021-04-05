using AssetTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModel
{
    public class ProjectSettingsViewModel
    {
        private TrackerContext context = new TrackerContext();

        private List<User> users;
        public List<User> Users
        {
            get
            {
                if (users == null)
                {
                    users = new List<User>();
                    var temp = (from a in context.Users
                                select a).ToList();
                    users = temp;
                }

                return users;
            }
        }

        private List<SecRole> roles;
        public List<SecRole> Roles
        {
            get
            {
                if (roles == null)
                {
                    roles = new List<SecRole>();
                    var temp = (from a in context.SecRoles
                                select a).ToList();
                    roles = temp;
                }

                return roles;
            }
        }

        private List<Phase> phases;
        public List<Phase> Phases
        {
            get
            {
                if (phases == null)
                {
                    phases = new List<Phase>();
                    var temp = (from a in context.Phases
                                select a).ToList();
                    phases = temp;
                }

                return phases;
            }
        }

        private List<AssetCategory> categories;
        public List<AssetCategory> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = new List<AssetCategory>();
                    var temp = (from a in context.AssetCategories
                                select a).ToList();
                    categories = temp;
                }

                return categories;
            }
        }

        public ProjectSettingsViewModel()
        {

        }
    }
}
