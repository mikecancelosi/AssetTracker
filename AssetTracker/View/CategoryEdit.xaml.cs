using AssetTracker.Model;
using AssetTracker.View.Commands;
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
    /// Interaction logic for CategoryEdit.xaml
    /// </summary>
    public partial class CategoryEdit : Page, ISavable
    {
        public CategoryEditViewModel VM
        {
            get { return (CategoryEditViewModel)DataContext; }
            set { DataContext = value; }
        }
        private Coordinator coordinator;

        
        public CategoryEdit(AssetCategory cat, Coordinator coord)
        {
            InitializeComponent();
            VM = new CategoryEditViewModel(cat);
            coordinator = coord;
        }

        public CategoryEdit(Coordinator coord)
        {
            InitializeComponent();
            VM = new CategoryEditViewModel();
            coordinator = coord;
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            coordinator.OnNavigateSelected += OnNavigatingAway;
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
        public void OnNavigatingAway()
        {
            CheckForPromptSave(() => coordinator.NavigateToQueued());
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

            }
            else
            {
                OnSaveComplete?.Invoke(sender, null);
                OnSaveComplete = delegate { };
            }
        }
        #endregion

        public void OnDeleteClicked(object sender, RoutedEventArgs e)
        {
            VM.OnDelete();
            coordinator.NavigateToProjectSettings();
        }

        public void NavigateToProjectSettings(object sender, RoutedEventArgs e)
        {
            CheckForPromptSave(() => coordinator.NavigateToProjectSettings());
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
