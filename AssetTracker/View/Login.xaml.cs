using Quipu.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Quipu.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private LoginViewModel VM
        {
            get { return (LoginViewModel)DataContext; }
            set { DataContext = value; }
        }

        public Login(LoginViewModel vm)
        {
            InitializeComponent();
            VM = vm;
        }

        private void OnLogin_Clicked(object sender, RoutedEventArgs e)
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
            }
        }
    }
}
