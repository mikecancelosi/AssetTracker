﻿using AssetTracker.Model;
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
    /// Interaction logic for AssetList.xaml
    /// </summary>
    public partial class AssetList : Page
    {
        public AssetListViewModel VM
        {
            get { return (AssetListViewModel)DataContext; }
            set { DataContext = value; }
        }
        private Coordinator myCoordinator;
        public AssetList(Coordinator coordinator)
        {
            InitializeComponent();
            VM = new AssetListViewModel();
            myCoordinator = coordinator;
            #region SearchboxSetup
            Value_AssignedTo.SetType(typeof(User));
            Value_Category.SetType(typeof(AssetCategory));
            Value_ParentID.SetType(typeof(Asset));
            SubscribeToSearchboxes();
            #endregion
        }
        private void SubscribeToSearchboxes()
        {
            Value_AssignedTo.OnSelectionChanged += () => { VM.OnParentAssetChanged(Value_AssignedTo.CurrentSelection.ID); };
            Value_Category.OnSelectionChanged += () => { VM.OnCategoryChanged(Value_Category.CurrentSelection.ID); };
            Value_ParentID.OnSelectionChanged += () => { VM.OnParentAssetChanged(Value_ParentID.CurrentSelection.ID); };
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Asset ast = MainGrid.SelectedItem as Asset;
            myCoordinator.NavigateToAssetDetail(ast);
        }

        private void CreateAsset_Clicked(object sender, MouseButtonEventArgs e)
        {
            VM.CreateAsset();
            AssetCreateControl.Visibility = Visibility.Visible;
            //TODO: Make everything else not interactable
        }

      
        private void CreateCancel_Clicked(object sender, MouseButtonEventArgs e)
        {
            AssetCreateControl.Visibility = Visibility.Collapsed;
            VM.ResetNewAsset();
        }

        private void CreateConfirm_Clicked(object sender, MouseButtonEventArgs e)
        {           
            List<Violation> violations;
            if (!VM.SaveAsset(out violations))
            {
                throw new NotImplementedException();
            }
            else
            {
                AssetCreateControl.Visibility = Visibility.Collapsed;
                Value_AssignedTo.ResetDisplay();
                Value_Category.ResetDisplay();
                Value_ParentID.ResetDisplay();
            }
        }

        private void MainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Asset ast = MainGrid.SelectedItem as Asset;
            VM.ChangeSelectedAsset(ast);
        }

        private void DeleteAsset_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (MainGrid.SelectedItem != null)
            {
                AssetDeletePrompt.Visibility = Visibility.Visible;
            }
        }

        private void DeleteConfirm_Clicked(object sender, MouseButtonEventArgs e)
        {
            VM.DeleteCurrentlySelectedAsset();
            AssetDeletePrompt.Visibility = Visibility.Collapsed;

        }

        private void DeleteCancel_Clicked(object sender, MouseButtonEventArgs e)
        {
            AssetDeletePrompt.Visibility = Visibility.Collapsed;
        }

        private void Value_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            VM.OnNameChanged(Value_Name.Text);
        }

        private void Value_Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            VM.OnDescriptionChanged(Value_Description.Text);
        }
    }
}
