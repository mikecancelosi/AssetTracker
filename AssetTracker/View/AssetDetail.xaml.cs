using AssetTracker.Model;
using AssetTracker.View.Commands;
using AssetTracker.View.Properties;
using AssetTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static AssetTracker.ViewModel.AssetDetailViewModel;

namespace AssetTracker.View
{
    /// <summary>
    /// Interaction logic for AssetDetail.xaml
    /// </summary>
    public partial class AssetDetail : Page
    {
        public AssetDetailViewModel vm;
        public AssetDetail()
        {
            InitializeComponent();
        }
        


        public AssetDetail(Asset model)
        {
            InitializeComponent();
            vm = new AssetDetailViewModel();
            vm.myAsset = model;
            DataContext = vm;

            Searchbox_AssignedTo.SetType(typeof(User));
            if (vm.myAsset.AssignedToUser != null)
            {
                Searchbox_AssignedTo.SetCurrentSelectedObject(vm.myAsset.AssignedToUser.ID);
            }
            Searchbox_AssignedTo.PropertyChanged += (s, e) => { vm.OnAssignedUserChange(Searchbox_AssignedTo.CurrentSelection.ID); };

            Searchbox_Phase.SetType(typeof(Phase));
            if (vm.myAsset.Phase != null)
            {
                Searchbox_Phase.SetCurrentSelectedObject(vm.myAsset.Phase.ID);
            }
            Searchbox_Phase.PropertyChanged += (s, e) => { vm.OnPhaseChanged(Searchbox_Phase.CurrentSelection.ID); };
        }

        private void HierarchyObjectClicked(object sender, RoutedEventArgs e)
        {
            PromptSave();
            // Change viewmodel to new asset

        }

        private void OnClose()
        {
            PromptSave();
        }

        private void OnDeleteClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            List<Violation> violations = new List<Violation>();
            if (vm.OnSave(out violations))
            {

            }
            else
            {

            }
        }

        private void OnChangelogClicked(object sender, RoutedEventArgs e)
        {
            if (Changelog.Visibility == Visibility.Visible)
            {
                Changelog.Visibility = Visibility.Collapsed;
            }
            else
            {
                Changelog.Visibility = Visibility.Visible;
            }
        }

        private void OnChangelogExited(object sender, RoutedEventArgs e)
        {
            Changelog.Visibility = Visibility.Collapsed;
        }
      

        private void PromptSave()
        {

        }     

        #region Discussion

        public ICommand DiscussionReplyClicked => new IDReceiverCmd((arr) => OnDiscussionReplyClicked(arr), (arr) => { return true; });
        public void OnDiscussionReplyClicked(object input)
        {
            string defaultText = "Start a new discussion..";
            object[] values = input as object[];
            int parentID = (int)values[0];
            string content = values[1] as string;
            if (content != defaultText)
            {
                vm.CreateNewDiscussion(parentID, content);
            }

        }
        #endregion
    }
}
