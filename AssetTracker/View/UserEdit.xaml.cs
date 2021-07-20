﻿using AssetTracker.Model;
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
    /// Interaction logic for UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Page
    {
        public UserEditViewModel VM
        {
            get { return (UserEditViewModel)DataContext; }
            set { DataContext = value; }
        }
        private Coordinator coordinator;

        public UserEdit(Coordinator coord)
        {
            InitializeComponent();
            VM = new UserEditViewModel();
            coordinator = coord;
            Searchbox_Role.SetType(typeof(SecRole));
            Searchbox_Role.SetCurrentSelectedObject(VM.CurrentUser.us_roid);
        }

        public UserEdit(User user, Coordinator coord)
        {
            InitializeComponent();
            VM = new UserEditViewModel(user);
            coordinator = coord;

            Searchbox_Role.SetType(typeof(SecRole));
            Searchbox_Role.SetCurrentSelectedObject(VM.CurrentUser.us_roid);
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            coordinator.OnNavigateSelected += () => OnNavigatingAway();
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
        private void OnNavigatingAway()
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
            if (!VM.OnSave(out violations))
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

        private void FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox nameTextBox = sender as TextBox;
            VM.OnFirstNameChanged(nameTextBox.Text);
        }

        private void LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox nameTextBox = sender as TextBox;
            VM.OnLastNameChanged(nameTextBox.Text);
        }

        private void DisplayName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox nameTextBox = sender as TextBox;
            VM.OnDisplayNameChanged(nameTextBox.Text);
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox emailTextBox = sender as TextBox;
            VM.OnEmailChanged(emailTextBox.Text);
        }

        private void Role_OnSelectionChanged()
        {
            VM.OnRoleChanged(Searchbox_Role.CurrentSelection.ID);
        }

        public void OnActivateAllClicked(object sender, RoutedEventArgs e)
        {
            VM.ActivateAllPermissions();
        }

        public void OnDeactivateAllClicked(object sender, RoutedEventArgs e)
        {
            VM.DeactivateAllPermissions();
        }

        public void NavigateToProjectSettings(object sender, RoutedEventArgs e)
        {
            coordinator.NavigateToProjectSettings();
        }

        public void PermissionAllowance_Clicked(object sender, RoutedEventArgs e)
        {

        }


    }
}
