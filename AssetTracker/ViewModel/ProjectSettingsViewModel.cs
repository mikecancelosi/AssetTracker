using AssetTracker.Extensions;
using AssetTracker.Model;
using AssetTracker.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace AssetTracker.ViewModel
{
    public class ProjectSettingsViewModel : ViewModel
    {

        public ObservableCollection<User> Users
        {
            get
            {
                return new ObservableCollection<User>(context.Users);
            }
        }

        public ObservableCollection<SecRole> Roles
        {
            get
            {
                return new ObservableCollection<SecRole>(context.SecRoles);
            }

        }

        public ObservableCollection<AssetCategory> Categories
        {
            get
            {
                return new ObservableCollection<AssetCategory>(context.AssetCategories);
            }
        }

        public User CopyUser(int id)
        {
            User user = context.Users.Find(id);
            return user.Clone() as User;
        }

        public void DeleteUser(int id)
        {
            User user = context.Users.Find(id);
            user.Delete(context);
            NotifyPropertyChanged("Users");
        }

        public void DeleteCategory(int id)
        {
            AssetCategory cat = context.AssetCategories.Find(id);
            cat.Delete(context);
            NotifyPropertyChanged("Categories");
        }

        public void DeleteRole(int id)
        {
            SecRole role = context.SecRoles.Find(id);
            role.Delete(context);
            NotifyPropertyChanged("Roles");
        }
    }
}
