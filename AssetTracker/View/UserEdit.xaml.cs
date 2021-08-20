using Quipu.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Quipu.View
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
        public UserEdit(UserEditViewModel vm)
        {
            InitializeComponent();
            VM = vm;           
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
