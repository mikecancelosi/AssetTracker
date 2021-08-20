using Quipu.ViewModels;
using DomainModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quipu.View
{
    /// <summary>
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Page
    {
        public UserDashboardViewModel VM
        {
            get { return (UserDashboardViewModel)DataContext; }
            set { DataContext = value; }
        }
        public UserDashboard(UserDashboardViewModel vm)
        {
            InitializeComponent();
            VM = vm;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {            
            Alert alert = UserAlertList.SelectedItem as Alert;
            if (alert != null)
            {
                VM.NavigateToAlert(alert.ID);
            }
        }
    }
}
