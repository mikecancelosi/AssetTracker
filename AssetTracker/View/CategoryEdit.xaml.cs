using AssetTracker.Model;
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
    public partial class CategoryEdit : Page
    {
        public CategoryEditViewModel VM;      

        public CategoryEdit(AssetCategory cat)
        {
            InitializeComponent();
            VM = new CategoryEditViewModel(cat);       
        }

        public override void EndInit()
        {
            base.EndInit();
            DataContext = VM;
        }


        public void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void OnDeleteClicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
