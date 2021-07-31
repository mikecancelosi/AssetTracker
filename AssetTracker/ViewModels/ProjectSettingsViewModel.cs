using AssetTracker.Enums;
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

        public ICommand CreateRole { get; set; }
        public ICommand CreateCategory { get; set; }
        public ICommand CreateUser { get; set; }
        public ICommand CreateAlert => new RelayCommand((s) => CreateNewProjectWideAlert(), (s) => true);

        public ICommand OpenNewAlert => new RelayCommand((s) => PromptNewAlert= true, (s) => true);
        public ICommand CloseNewAlert => new RelayCommand((s) => PromptNewAlert = false, (s) => true);

        private bool promptNewAlert;
        public bool PromptNewAlert
        {
            get => promptNewAlert;
            set
            {
                if(value == false)
                {
                    NewPrompt_Header = "";
                    NewPrompt_Contents = "";
                    NotifyPropertyChanged("NewPrompt_Header");
                    NotifyPropertyChanged("NewPrompt_Contents");
                }
                promptNewAlert = value;
                NotifyPropertyChanged("PromptNewAlert");
            }
        }

        public string NewPrompt_Header { get; set; }
        public string NewPrompt_Contents { get; set; }

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

        public enum OperationType
        {
            Copy,
            Edit,
            Delete
        }

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

        private void CreateNewProjectWideAlert()
        {           

            Alert newAlert = context.Alerts.Create();
            newAlert.ar_usid = MainViewModel.Instance.CurrentUser.ID;
            newAlert.ar_projectwide = true;
            newAlert.ar_date = DateTime.Now;
            newAlert.ar_header = NewPrompt_Header;
            newAlert.ar_content = NewPrompt_Contents;
            newAlert.ar_type = AlertType.ProjectWideAlert;
            context.Alerts.Add(newAlert);
            context.SaveChanges();
            PromptNewAlert = false;
            NotifyPropertyChanged("ProjectWideAlerts");
        }

    }
}
