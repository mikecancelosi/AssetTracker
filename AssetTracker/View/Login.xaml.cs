﻿using AssetTracker.ViewModel;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private Coordinator coordinator;
        private LoginViewModel VM
        {
            get { return (LoginViewModel)DataContext; }
            set { DataContext = value; }
        }
        public Login(Coordinator coord)
        {
            InitializeComponent();
            coordinator = coord;
            VM = new LoginViewModel();
        }

        private void OnLogin_Clicked(object sender, MouseButtonEventArgs e)
        {
            var loginResult = VM.LoginUser();
            switch (loginResult)
            {
                case LoginViewModel.LoginCode.Username:
                    {
                        UsernameError.Visibility = Visibility.Visible;
                        break;
                    }
                case LoginViewModel.LoginCode.Password:
                    {
                        PasswordError.Visibility = Visibility.Visible;
                        break;
                    }
                case LoginViewModel.LoginCode.Success:
                default:
                    {
                        coordinator.NavigateToUserDashboard();
                        break;
                    }
            }
        }
    }
}
