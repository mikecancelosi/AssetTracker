using AssetTracker.Model;
using AssetTracker.ViewModel;
using System;
using System.Collections.Generic;
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
            if(vm.myAsset.Phase != null)
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
            if(vm.OnSave(out violations))
            {

            }
            else
            {

            }
        }

        private void OnChangelogClicked(object sender, RoutedEventArgs e)
        {
            Changelog.Visibility = Visibility.Visible;
        }

        private void OnChangelogExited(object sender, RoutedEventArgs e)
        {
            Changelog.Visibility = Visibility.Collapsed;
        }

        private void DiscussionAddClicked(object sender, RoutedEventArgs e)
        {
            string defaulttext = "Start a new discussion..";
            if (NewDiscussionReply.Text != defaulttext)
            {
                vm.CreateNewDiscussion(NewDiscussionReply.Text);
                NewDiscussionReply.Text = defaulttext;
            }
        }
        

        private void PromptSave()
        {

        }
    }
}
