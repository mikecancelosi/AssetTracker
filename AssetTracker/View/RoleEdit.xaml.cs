using AssetTracker.Model;
using AssetTracker.ViewModel;
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
    /// Interaction logic for RoleEdit.xaml
    /// </summary>
    public partial class RoleEdit : Page
    {
        public RoleEditViewModel VM;

        public RoleEdit(SecRole role)
        {
            InitializeComponent();
            VM = new RoleEditViewModel(role);
            DataContext = VM;
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            List<Violation> violations = new List<Violation>();
            if (!VM.OnSave(out violations))
            {

            }
            else
            {
                
            }
        }

        private void OnDeleteClicked(object sender, RoutedEventArgs e)
        {
            
        }

        private void Permissions_Loaded(object sender, RoutedEventArgs e)
        {
            int count = 1;
            foreach (var obj in PermissionsList.Items)
            {

               
                var container = PermissionsList.ItemContainerGenerator.ContainerFromItem(obj);

                var presenter = VisualTreeHelper.GetChild(container, 0);
                var CurrentGrid = VisualTreeHelper.GetChild(presenter, 0) as StackPanel;
                var listview = VisualTreeHelper.GetChild(CurrentGrid, 1) as ListView;
                CurrentGrid.Height = count * 100;
                listview.Height = count * 50;
                count++;
            }

        }

        private void PermissionsList_LayoutUpdated(object sender, EventArgs e)
        {
            Permissions_Loaded(sender, null);
        }
    }
}
