using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.ViewModels;
using AssetTracker.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssetTracker.View
{
    /// <summary>
    /// Interaction logic for UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Page
    {
        public UserEditViewModel VM
        {
            get { return (UserEditViewModel)DataContext; }
            set { DataContext = value; }
        }

        private SearchBoxViewModel roleSearchboxVM;

        private readonly IControlViewModelFactory controlFactory;
        public UserEdit(UserEditViewModel vm, IControlViewModelFactory vmFactory)
        {
            InitializeComponent();
            VM = vm;
            controlFactory = vmFactory;

            roleSearchboxVM = controlFactory.BuildSearchboxViewModel(typeof(SecRole), 
                                                                            startingID : vm.CurrentUser.us_roid);
            roleSearchboxVM.OnSelectionChanged += () => Role_OnSelectionChanged();

            Searchbox_Role.SetViewmodel(roleSearchboxVM);
        }

        private void Role_OnSelectionChanged()
        {
            VM.OnRoleChanged(roleSearchboxVM.CurrentlySelectedObject.ID);
            //TODO: Adjust permissions.
        }

        public void OnActivateAllClicked(object sender, RoutedEventArgs e)
        {
            VM.ActivateAllPermissions();
        }

        public void OnDeactivateAllClicked(object sender, RoutedEventArgs e)
        {
            VM.DeactivateAllPermissions();
        }


    }
}
