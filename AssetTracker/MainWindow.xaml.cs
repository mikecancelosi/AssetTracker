using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View;
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

namespace AssetTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {       
        public MainWindow()
        {
            InitializeComponent();
            NavigateToAssetList();
        }

        public void NavigateToAssetList()
        {
            ContentFrame.Navigate(new AssetList());
        }

        private void NavigateToSettings(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProjectSettings());
        }
    }
}
