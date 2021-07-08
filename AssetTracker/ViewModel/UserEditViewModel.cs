using AssetTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModel
{
    public class UserEditViewModel : ViewModel
    {
        public User CurrentUser { get; set; }

        private List<SecPermission4> permissionGroups;
        public List<SecPermission4> PermissionGroups
        { 
            get
            {
                if (permissionGroups == null)
                {
                    permissionGroups = (from s in context.SecPermission4                                      
                                        select s).ToList();
                }
                return permissionGroups;
            }
            set
            {

            }
        }

    }
}
