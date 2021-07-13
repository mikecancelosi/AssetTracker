using AssetTracker.Model;
using AssetTracker.View.Commands;
using AssetTracker.View.Properties;
using AssetTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using static AssetTracker.ViewModel.AssetDetailViewModel;

namespace AssetTracker.View
{
    /// <summary>
    /// Interaction logic for AssetDetail.xaml
    /// </summary>
    public partial class AssetDetail : Page
    {
        public AssetDetailViewModel vm;

        //TODO: Introduce a more robust event system (invoke once)
        private delegate void SaveDelegate();
        private event SaveDelegate OnSaveComplete;
        private event SaveDelegate OnSaveRefused;
        private event SaveDelegate OnSaveCanceled;

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

            #region SearchboxSetup
            Searchbox_AssignedTo.SetType(typeof(User));
            Searchbox_Phase.SetType(typeof(Phase));
            Searchbox_Category.SetType(typeof(AssetCategory));
            AssignSearchboxValues();
            SubscribeToSearchboxes();
            #endregion

            vm.PropertyChanged += (s, e) =>
             {
                 if (e.PropertyName == "myAsset")
                 {
                     OnModelChanged();
                 }
             };
        }       

        private void AssignSearchboxValues()
        {
            if (vm.myAsset.AssignedToUser != null)
            {
                Searchbox_AssignedTo.SetCurrentSelectedObject(vm.myAsset.AssignedToUser.ID);
            }
            if (vm.myAsset.Phase != null)
            {
                Searchbox_Phase.SetCurrentSelectedObject(vm.myAsset.Phase.ID);
            }
            if (vm.myAsset.AssetCategory != null)
            {
                Searchbox_Category.SetCurrentSelectedObject(vm.myAsset.AssetCategory.ca_id);
            }
        }
        private void SubscribeToSearchboxes()
        {
            Searchbox_AssignedTo.OnSelectionChanged += () => { vm.OnAssignedUserChange(Searchbox_AssignedTo.CurrentSelection.ID); };
            Searchbox_Phase.OnSelectionChanged += () => { vm.OnPhaseChanged(Searchbox_Phase.CurrentSelection.ID); };
            Searchbox_Category.OnSelectionChanged += () => { vm.OnCategoryChanged(Searchbox_Category.CurrentSelection.ID); };
        }

        private void OnModelChanged()
        {
            Searchbox_AssignedTo.ClearInvocationList();
            Searchbox_Phase.ClearInvocationList();
            Searchbox_Category.ClearInvocationList();
            AssignSearchboxValues();
            SubscribeToSearchboxes();

        }

        private void OnClose()
        {
            
        }

        private void OnDeleteClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            List<Violation> violations = new List<Violation>();
            if (!vm.OnSave(out violations))
            {

            }
            else
            {
                OnSaveComplete?.Invoke();
                if (OnSaveComplete != null)
                {
                    foreach (Delegate d in OnSaveComplete.GetInvocationList())
                    {
                        OnSaveComplete -= (SaveDelegate)d;
                    }
                }                
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

        #region Discussion

        //TODO: Account for discussion made by users who have since been deleted
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

        private async void OnHierarchyAssetSelected_Clicked(object sender, MouseButtonEventArgs e)
        {
            int senderId = (int)((Border)sender).Tag;
            if (vm.myAsset.ID != senderId)
            {
                if (vm.Savable)
                {
                    PromptSavePanel.Visibility = Visibility.Visible;
                    OnSaveComplete += () => vm.OnHierarchyAssetSelected(senderId);
                    OnSaveRefused += () => vm.OnHierarchyAssetSelected(senderId);
                }
                else
                {
                    vm.OnHierarchyAssetSelected(senderId);
                }
            }
        }

        private void OnMetadataDelete_Clicked(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            vm.OnMetadataDelete_Clicked(id);
        }
        private void OnMetadataAdd_Clicked(object sender, RoutedEventArgs e)
        {
            vm.OnMetadataAdd_Clicked();
        }

        private void OnModifyTitleOpen_Clicked(object sender, MouseButtonEventArgs e)
        {
            ModifyTitlePanel.Visibility = Visibility.Visible;
            EditTitle_Value.Text = vm.myAsset.Name;
            EditDescription_Value.Text = vm.myAsset.as_description;
        }

        private void OnModifyTitleSave_Clicked(object sender, RoutedEventArgs e)
        {
            ModifyTitlePanel.Visibility = Visibility.Collapsed;
            vm.ModifyAssetName(EditTitle_Value.Text);
            vm.ModifyDescription(EditDescription_Value.Text);
        }
        private void OnModifyTitleCancel_Clicked(object sender, RoutedEventArgs e)
        {
            ModifyTitlePanel.Visibility = Visibility.Collapsed;
            EditTitle_Value.Text = "";
            EditDescription_Value.Text = "";
        }

        private void ConfirmSave_Clicked(object sender, RoutedEventArgs e)
        {
            PromptSavePanel.Visibility = Visibility.Collapsed;
            OnSaveClicked(sender, e);
        }

        public void RefuseSave_Clicked(object sender, RoutedEventArgs e)
        {
            PromptSavePanel.Visibility = Visibility.Collapsed;
            OnSaveRefused?.Invoke();
            foreach (Delegate d in OnSaveRefused.GetInvocationList())
            {
                OnSaveRefused -= (SaveDelegate)d;
            }
        }
    }
}
