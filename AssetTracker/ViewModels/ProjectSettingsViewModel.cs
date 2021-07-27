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

namespace AssetTracker.ViewModels
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

        public AssetCategory CopyCategory(int id)
        {
            AssetCategory cat = context.AssetCategories.Find(id);
            return cat.Clone() as AssetCategory;
        }
        public void DeleteCategory(int id)
        {
            AssetCategory cat = context.AssetCategories.Find(id);
            cat.Delete(context);
            NotifyPropertyChanged("Categories");
        }

        public SecRole CopyRole(int id)
        {
            SecRole role = context.SecRoles.Find(id);
            return role.Clone() as SecRole;
        }

        public void DeleteRole(int id)
        {
            SecRole role = context.SecRoles.Find(id);
            role.Delete(context);
            NotifyPropertyChanged("Roles");
        }

        public void Reload()
        {
            context = new TrackerContext();
            NotifyPropertyChanged("Users");
            NotifyPropertyChanged("Roles");
            NotifyPropertyChanged("Categories");
        }

        public void SetDeletionObject(DatabaseBackedObject obj)
        {
            SelectedObject = obj;
            NotifyPropertyChanged("SelectedObject");
        }

        public void DeleteSelectedObject()
        {
            if(SelectedObject.GetType().BaseType == typeof(AssetCategory))
            {
                DeleteCategory(SelectedObject.ID);
            } 
            else if (SelectedObject.GetType().BaseType == typeof(SecRole))
            {
                DeleteRole(SelectedObject.ID);
            }
            else if (SelectedObject.GetType().BaseType == typeof(User))
            {
                DeleteUser(SelectedObject.ID);
            }
            else
            {
                return;
            }

            SelectedObject = null;
            NotifyPropertyChanged("SelectedObject");
        }
        
        public void SetSelectedObject(DatabaseBackedObject dbo)
        {
            SelectedObject = dbo;
            NotifyPropertyChanged("SelectedObject");
        }

        public DatabaseBackedObject SelectedObject { get; private set; }
    }
}
