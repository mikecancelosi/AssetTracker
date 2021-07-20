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
    /// Interaction logic for RoleEdit.xaml
    /// </summary>
    public partial class RoleEdit : Page, ISavable
    {
        public RoleEditViewModel VM
        {
            get { return (RoleEditViewModel)DataContext; }
            set { DataContext = value; }
        }
        private Coordinator coordinator;       

        public RoleEdit(Coordinator coord)
        {           
            VM = new RoleEditViewModel();
            coordinator = coord;
        }

        public RoleEdit(SecRole role, Coordinator coord)
        {
            InitializeComponent();
            VM = new RoleEditViewModel(role);
            coordinator = coord;
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


        private void OnDeleteClicked(object sender, RoutedEventArgs e)
        {
            VM.OnDelete();
            coordinator.NavigateToProjectSettings();
        }

        public void NavigateToProjectSettings(object sender, RoutedEventArgs e)
        {
            coordinator.NavigateToProjectSettings();
        }

        public void OnActivateAllClicked(object sender, RoutedEventArgs e)
        {
            VM.ActivateAllPermissions();
        }

        public void OnDeactivateAllClicked(object sender, RoutedEventArgs e)
        {
            VM.DeactivateAllPermissions();
        }

        private void RoleName_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox nameTextBox = sender as TextBox;
            VM.OnRoleNameChanged(nameTextBox.Text);
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            coordinator.OnNavigateSelected += () => OnNavigatingAway();
        }
    }
}
