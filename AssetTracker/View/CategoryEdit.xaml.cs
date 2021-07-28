using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View.Commands;
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
    /// Interaction logic for CategoryEdit.xaml
    /// </summary>
    public partial class CategoryEdit : Page
    {
        public CategoryEditViewModel VM
        {
            get { return (CategoryEditViewModel)DataContext; }
            set { DataContext = value; }
        }
        private INavigationCoordinator coordinator;

        public CategoryEdit(CategoryEditViewModel vm, INavigationCoordinator coord)
        {
            InitializeComponent();
            VM = vm;
            coordinator = coord;
            //coordinator.OnNavigateSelected += (v) => OnNavigatingAway(v);
        }

        #region ISavableSetup
        public event EventHandler OnSaveComplete;
        public event EventHandler OnSaveRefused;
        public void CheckForPromptSave(Action methodToCall)
        {
            if (VM.Savable)
            {
                PromptSavePanel.Visibility = Visibility.Visible;
                OnSaveComplete += (s, e) => methodToCall();
                OnSaveRefused += (s, e) => methodToCall();
            }
            else
            {
                methodToCall();
            }
        }
        public void OnNavigatingAway(Page v)
        {
            if (v != this)
            {
                CheckForPromptSave(() => coordinator.NavigateToQueued());
            }
        }

        public ICommand ConfirmSave_Clicked => new IDReceiverCmd((arr) => OnConfirmSave(), (arr) => { return true; });
        public ICommand RefuseSave_Clicked => new IDReceiverCmd((arr) => OnRefuseSave(), (arr) => { return true; });

        private void OnConfirmSave()
        {
            PromptSavePanel.Visibility = Visibility.Collapsed;
            OnSaveClicked(this, null);
        }

        public void OnRefuseSave()
        {
            PromptSavePanel.Visibility = Visibility.Collapsed;
            OnSaveRefused?.Invoke(this, null);
            OnSaveRefused = delegate { };
        }

        public void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            List<Violation> violations = new List<Violation>();
            if (!VM.Save(out violations))
            {
                throw new NotImplementedException();
            }
            else
            {
                OnSaveComplete?.Invoke(sender, null);
                OnSaveComplete = delegate { };
            }
        }
        #endregion

       
        #region Delete Category

        private void DeleteCategory_Clicked(object sender, RoutedEventArgs e)
        {
            DeletePrompt.Visibility = Visibility.Visible;
        }

        private void DeleteConfirm_Clicked(object sender, RoutedEventArgs e)
        {
            VM.DeleteCategory();
            //coordinator.NavigateToProjectSettings();
        }

        private void DeleteCancel_Clicked(object sender, RoutedEventArgs e)
        {
            DeletePrompt.Visibility = Visibility.Collapsed;
        }
        #endregion

        public void NavigateToProjectSettings(object sender, RoutedEventArgs e)
        {
            //CheckForPromptSave(() => coordinator.NavigateToProjectSettings());
        }

        private void OnPhaseUp(object sender, RoutedEventArgs e)
        {
            Button senderBtn = sender as Button;
            Phase senderPhase = senderBtn.DataContext as Phase;
            VM.OnPhaseUpClicked(senderPhase.ph_id);
        }

        private void OnPhaseDown(object sender, RoutedEventArgs e)
        {
            Button senderBtn = sender as Button;
            Phase senderPhase = senderBtn.DataContext as Phase;
            VM.OnPhaseDownClicked(senderPhase.ph_id);
        }
        private void OnPhaseDelete(object sender, RoutedEventArgs e)
        {
            Button senderBtn = sender as Button;
            Phase senderPhase = senderBtn.DataContext as Phase;
            VM.OnPhaseDelete(senderPhase.ph_id);
        }
        private void OnPhaseAdd(object sender, RoutedEventArgs e)
        {
            VM.OnNewPhaseClicked();
        }


        private void PhaseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox senderText = sender as TextBox;
            Phase senderPhase = senderText.DataContext as Phase;
            VM.OnPhaseNameChange(senderPhase.ph_step, senderText.Text);
        }

        private void CategoryName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox senderText = sender as TextBox;
            VM.OnCategoryNameChanged(senderText.Text);
        }

      
    }
}
