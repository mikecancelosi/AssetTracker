using AssetTracker.DAL;
using AssetTracker.Model;
using AssetTracker.Services;
using Mehdime.Entity;
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
        private GenericRepository<DatabaseBackedObject> assetRepo;
        private IDbContextScopeFactory scopeFactory;

        private bool LoadingData = false;

        public MainWindow()
        {
            InitializeComponent();

            if(LoadingData)
            {
                scopeFactory = new DbContextScopeFactory();
                assetRepo = new GenericRepository<DatabaseBackedObject>(scopeFactory);
                FillWithData();
            }
        }

        private void FillWithData()
        {
            using(IDbContextScope scope = scopeFactory.Create())
            {
                List<DatabaseBackedObject> objectsToCreate = DefaultRepositoryData.BuildTestData();
                objectsToCreate.ForEach(x => assetRepo.Insert(x,x.GetType()));
                scope.SaveChanges();
            }
        }
    }
}
