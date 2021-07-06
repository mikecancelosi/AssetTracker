using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View;
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

namespace AssetTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel vm;

        public MainWindow()
        {
            vm = MainViewModel.Instance;
            DataContext = vm;
            InitializeComponent();                 
            NavigateToDashboard(null,null);           
        }

        public void NavigateToAssetList(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new AssetList());
        }

        public void NavigateToProjectStatus(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProjectStatus());
        }

        private void NavigateToProjectConfig(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProjectSettings());
        }

        private void NavigateToDashboard(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new UserDashboard());
        }

        #region DatabaseSetup
        private void FillDatabase()
        {
            List<DatabaseBackedObject> objectsToAdd = DefaultRepositoryData.BuildTestData();
            TrackerContext context = new TrackerContext();
            objectsToAdd.ForEach(x => context.Set(x.GetType()).Add(x));
            context.SaveChanges();
        }

        private void UpdateDatabase()
        {
            List<DatabaseBackedObject> objectsToAdd = DefaultRepositoryData.BuildTestData();
            TrackerContext context = new TrackerContext();
            objectsToAdd.ForEach(x => context.Entry(x).State = System.Data.Entity.EntityState.Modified);
            context.SaveChanges();
        }
        #endregion

    }
}
