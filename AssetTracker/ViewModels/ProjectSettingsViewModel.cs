using AssetTracker.Extensions;
using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View;
using AssetTracker.View.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace AssetTracker.ViewModels
{
    public class ProjectSettingsViewModel : ViewModel
    {
        public ObservableCollection<User> Users => new ObservableCollection<User>(context.Users);
        public ObservableCollection<SecRole> Roles => new ObservableCollection<SecRole>(context.SecRoles);
        public ObservableCollection<AssetCategory> Categories => new ObservableCollection<AssetCategory>(context.AssetCategories);

        public ICommand CreateRole { get; set; }
        public ICommand CreateCategory { get; set; }
        public ICommand CreateUser { get; set; }

        private bool promptDelete;
        public bool PromptDelete 
        {
            get => promptDelete;
            set
            {
                promptDelete = value;
                NotifyPropertyChanged("PromptDelete");
            }
        }
        public ICommand DeleteConfirmed { get; set; }
        public ICommand DeleteCanceled { get; set; }
        public DatabaseBackedObject DeletionObject { get; private set; }

        private readonly INavigationCoordinator navCoordinator;
        public ProjectSettingsViewModel(INavigationCoordinator coord)
        {
            navCoordinator = coord;
            CreateRole = new RelayCommand((s) => navCoordinator.NavigateToCreateRole(), (s) => true);
            CreateCategory = new RelayCommand((s) => navCoordinator.NavigateToCreateCategory(), (s) => true);
            CreateUser = new RelayCommand((s) => navCoordinator.NavigateToCreateUser(), (s) => true);
            DeleteConfirmed = new RelayCommand((s) => DeleteSelectedObject(), (s) => true);
            DeleteCanceled = new RelayCommand((s) => { PromptDelete = false; }, (s) => true);
        }

        public enum OperationType
        {
            Copy,
            Edit,
            Delete
        }

        public void CompleteDBOOperation(DatabaseBackedObject dbo, OperationType operation)
        {
            //TODO : Fix this monstrosity
            string typeName = dbo.GetType().BaseType.Name;
            switch (operation)
            {

                case OperationType.Copy:
                    DatabaseBackedObject clone = dbo.Clone();
                    if (typeName == "SecRole")
                    {
                        navCoordinator.NavigateToRoleEdit((SecRole)clone);
                    }
                    else if (typeName == "AssetCategory")
                    {
                        navCoordinator.NavigatetoCategoryEdit((AssetCategory)clone);
                    }
                    else if (typeName == "User")
                    {
                        navCoordinator.NavigateToUserEdit((User)clone);
                    }
                    break;
                case OperationType.Edit:
                    if (typeName == "SecRole")
                    {
                        navCoordinator.NavigateToRoleEdit((SecRole)dbo);
                    }
                    else if (typeName == "AssetCategory")
                    {
                        navCoordinator.NavigatetoCategoryEdit((AssetCategory)dbo);
                    }
                    else if (typeName == "User")
                    {
                        navCoordinator.NavigateToUserEdit((User)dbo);
                    }
                    break;
                case OperationType.Delete:
                    DeletionObject = dbo;
                    PromptDelete = true;
                    NotifyPropertyChanged("DeletionObject");
                    break;
            }
        }


        public void Reload()
        {
            context = new TrackerContext();
            NotifyPropertyChanged("Users");
            NotifyPropertyChanged("Roles");
            NotifyPropertyChanged("Categories");
        }

        private void DeleteSelectedObject()
        {
            DeletionObject.Delete(context);
            DeletionObject = null;
            PromptDelete = false;
            NotifyPropertyChanged("Users");
            NotifyPropertyChanged("Roles");
            NotifyPropertyChanged("Categories");
        }


    }
}
