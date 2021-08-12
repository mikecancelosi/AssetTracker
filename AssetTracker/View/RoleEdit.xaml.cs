using AssetTracker.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AssetTracker.View
{
    /// <summary>
    /// Interaction logic for RoleEdit.xaml
    /// </summary>
    public partial class RoleEdit : Page
    {
        public RoleEditViewModel VM
        {
            get { return (RoleEditViewModel)DataContext; }
            set { DataContext = value; }
        }

        public RoleEdit(RoleEditViewModel vm)
        {
            InitializeComponent();
            VM = vm;
        }

        public void PermissionChecked(object sender, EventArgs e)
        {

        }

        public void PermissionUnchecked(object sender,EventArgs e)
        {

        }
    }
}
