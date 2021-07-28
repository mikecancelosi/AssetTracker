using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.View.Properties;
using AssetTracker.ViewModels;
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
using static AssetTracker.ViewModels.AssetDetailViewModel;

namespace AssetTracker.View
{
    /// <summary>
    /// Interaction logic for AssetDetail.xaml
    /// </summary>
    public partial class AssetDetail : Page
    {
        public AssetDetailViewModel VM
        {
            get { return (AssetDetailViewModel)DataContext; }
            set { DataContext = value; }
        }

        public AssetDetail(AssetDetailViewModel vm)
        {
            InitializeComponent();           
            VM = vm;
            #region SearchboxSetup
            Searchbox_AssignedTo.SetType(typeof(User));
            Searchbox_Phase.SetType(typeof(Phase));
            Searchbox_Category.SetType(typeof(AssetCategory));
            AssignSearchboxValues();
            SubscribeToSearchboxes();
            #endregion

            VM.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "myAsset")
                {
                    OnModelChanged();
                }
            };
        }


        private void AssignSearchboxValues()
        {
            if (VM.myAsset.AssignedToUser != null)
            {
                Searchbox_AssignedTo.SetCurrentSelectedObject(VM.myAsset.AssignedToUser.ID);
            }
            if (VM.myAsset.Phase != null)
            {
                Searchbox_Phase.SetCurrentSelectedObject(VM.myAsset.Phase.ID);
            }
            if (VM.myAsset.AssetCategory != null)
            {
                Searchbox_Category.SetCurrentSelectedObject(VM.myAsset.AssetCategory.ca_id);
                Searchbox_Phase.SetFilter(VM.StatusFilter);
            }
        }
        private void SubscribeToSearchboxes()
        {
            Searchbox_AssignedTo.OnSelectionChanged += () => { VM.ModifyAssignedUser(Searchbox_AssignedTo.CurrentSelection.ID); };
            Searchbox_Phase.OnSelectionChanged += () => { VM.ModifyPhase(Searchbox_Phase.CurrentSelection.ID); };
            Searchbox_Category.OnSelectionChanged += () => { OnCategoryChanged(Searchbox_Category.CurrentSelection.ID); };
        }

        private void OnCategoryChanged(int newID)
        {
            VM.ModifyCategory(newID);
            Searchbox_Phase.ResetDisplay();
            Searchbox_Phase.SetFilter(VM.StatusFilter);
        }

        private void OnModelChanged()
        {
            Searchbox_AssignedTo.ClearInvocationList();
            Searchbox_Phase.ClearInvocationList();
            Searchbox_Category.ClearInvocationList();
            AssignSearchboxValues();
            SubscribeToSearchboxes();

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

        public ICommand DiscussionReplyClicked => new IDReceiverCmd((arr) => OnDiscussionReplyClicked(arr), (arr) => { return true; });
        public void OnDiscussionReplyClicked(object input)
        {
            string defaultText = "Start a new discussion..";
            object[] values = input as object[];
            int parentID = (int)values[0];
            string content = values[1] as string;
            if (content != defaultText)
            {
                VM.CreateNewDiscussion(parentID, content);
            }

        }
        #endregion        

        #region Tags
        private void OnMetadataAdd_Clicked(object sender, RoutedEventArgs e)
        {
            TagAdd_Window.Visibility = Visibility.Visible;
        }

        private void OnMetadataDelete_Clicked(object sender, RoutedEventArgs e)
        {
            int id = (int)((Border)sender).Tag;
            VM.DeleteTag(id);
        }

        private void AddTagConfirm_Clicked(object sender, RoutedEventArgs e)
        {
            VM.AddMetadata(TagAdd_Content.Text);
            TagAdd_Content.Text = "";
            TagAdd_Window.Visibility = Visibility.Collapsed;
        }

        private void AddTagCancel_Clicked(object sender, RoutedEventArgs e)
        {
            TagAdd_Content.Text = "";
            TagAdd_Window.Visibility = Visibility.Collapsed;
        }
        #endregion

        private void OnModifyTitleOpen_Clicked(object sender, RoutedEventArgs e)
        {
            ModifyTitlePanel.Visibility = Visibility.Visible;
            EditTitle_Value.Text = VM.myAsset.Name;
            EditDescription_Value.Text = VM.myAsset.as_description;
        }

        private void OnModifyTitleSave_Clicked(object sender, RoutedEventArgs e)
        {
            ModifyTitlePanel.Visibility = Visibility.Collapsed;
            VM.ModifyAssetName(EditTitle_Value.Text);
            VM.ModifyDescription(EditDescription_Value.Text);
        }
        private void OnModifyTitleCancel_Clicked(object sender, RoutedEventArgs e)
        {
            ModifyTitlePanel.Visibility = Visibility.Collapsed;
            EditTitle_Value.Text = "";
            EditDescription_Value.Text = "";
        }
    }
}
