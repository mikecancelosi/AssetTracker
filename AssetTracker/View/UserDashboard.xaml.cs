using AssetTracker.ViewModels;
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
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Page
    {
        public UserDashboardViewModel VM
        {
            get { return (UserDashboardViewModel)DataContext; }
            set { DataContext = value; }
        }
        private Coordinator coordinator;
        public UserDashboard(UserDashboardViewModel vm, Coordinator coord)
        {
            InitializeComponent();
            VM = vm;
            coordinator = coord;
        }
    }
}
