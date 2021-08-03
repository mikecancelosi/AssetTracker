using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.View.Properties;
using AssetTracker.ViewModels;
using AssetTracker.ViewModels.Services;
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

        /// <summary>
        /// Factory for creating sub controls
        /// </summary>
        private readonly IControlViewModelFactory controlFactory;
        public AssetDetail(AssetDetailViewModel vm, IControlViewModelFactory conFactory)
        {
            InitializeComponent();
            VM = vm;
            controlFactory = conFactory;
            InitializeSearchboxes();
        }

        /// <summary>
        /// Viewmodel for assigned user searchbox
        /// </summary>
        private SearchBoxViewModel assignVM;
        /// <summary>
        /// Viewmodel for asset category searchbox
        /// </summary>
        private SearchBoxViewModel categoryVM;
        /// <summary>
        /// Viewmodel for Phase searchbox
        /// </summary>
        private SearchBoxViewModel phaseVM;

        /// <summary>
        /// Initialize searchboxes and listen to when the selections change
        /// </summary>
        private void InitializeSearchboxes()
        {
            assignVM = controlFactory.BuildSearchboxViewModel(typeof(User),
                                                              startingID: VM.myAsset.AssignedToUser?.ID ?? 0);
            categoryVM = controlFactory.BuildSearchboxViewModel(typeof(AssetCategory),
                                                                startingID: VM.myAsset.AssetCategory?.ID ?? 0);
            phaseVM = controlFactory.BuildSearchboxViewModel(typeof(Phase),
                                                             baseFilter: VM.StatusFilter,
                                                             startingID: VM.myAsset.Phase?.ID ?? 0);

            assignVM.OnSelectionChanged += () => { VM.ModifyAssignedUser(assignVM.CurrentlySelectedObject.ID); };
            phaseVM.OnSelectionChanged += () => { VM.ModifyPhase(phaseVM.CurrentlySelectedObject.ID); };
            categoryVM.OnSelectionChanged += () => { OnCategoryChanged(categoryVM.CurrentlySelectedObject.ID); };

            Searchbox_AssignedTo.SetViewmodel(assignVM);
            Searchbox_Phase.SetViewmodel(phaseVM);
            Searchbox_Category.SetViewmodel(categoryVM);
            
        }

        /// <summary>
        /// When a category changes we need to handle the phase selection
        /// </summary>
        /// <param name="newID"></param>
        private void OnCategoryChanged(int newID)
        {
            VM.ModifyCategory(newID);
            phaseVM.ResetDisplay();
            phaseVM.SetBaseFilter(VM.StatusFilter);
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
            int id = (int)((Border)sender).Tag;
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
