using AssetTracker.ViewModels;
using DomainModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        }
        /// <summary>
        /// Allow changelog button to hide/show the changelog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        //TODO: Remove discrepency between usage of 'Tags' and 'Metadata'
        #region Tags
        /// <summary>
        /// Display the tag add window
        /// </summary>
        private void OnMetadataAdd_Clicked(object sender, RoutedEventArgs e)
        {
            TagAdd_Window.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Delete the tag
        /// </summary>
        private void OnMetadataDelete_Clicked(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            VM.DeleteTag(id);
        }

        /// <summary>
        /// Confirm button from add tag window was clicked. Add the tag and reset the window
        /// </summary>
        private void AddTagConfirm_Clicked(object sender, RoutedEventArgs e)
        {
            VM.AddMetadata(TagAdd_Content.Text);
            TagAdd_Content.Text = "";
            TagAdd_Window.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Close the add tag window without adding the tag
        /// </summary>
        private void AddTagCancel_Clicked(object sender, RoutedEventArgs e)
        {
            TagAdd_Content.Text = "";
            TagAdd_Window.Visibility = Visibility.Collapsed;
        }
        #endregion

        /// <summary>
        /// Open the modify title/description window
        /// </summary>
        private void OnModifyTitleOpen_Clicked(object sender, RoutedEventArgs e)
        {
            ModifyTitlePanel.Visibility = Visibility.Visible;
            EditTitle_Value.Text = VM.AssetTitle;
            EditDescription_Value.Text = VM.Description;
        }

        private void OnModifyTitleCancel_Clicked(object sender, RoutedEventArgs e)
        {
            ModifyTitlePanel.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Modify title/description was confirmed.
        /// </summary>
        private void OnModifyTitleSave_Clicked(object sender, RoutedEventArgs e)
        {
            ModifyTitlePanel.Visibility = Visibility.Collapsed;
            VM.AssetTitle = EditTitle_Value.Text;
            VM.Description = EditDescription_Value.Text;
        }

        /// <summary>
        /// Hierarchy object clicked, change our current asset to what was clicked
        /// </summary>
        private void HierarchyObject_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Border selectedObject = sender as Border;
            VM.OnHierarchyAssetSelected((int)selectedObject.Tag );
        }
        
    }
}
