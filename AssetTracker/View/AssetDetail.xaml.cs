using AssetTracker.Model;
using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for AssetDetail.xaml
    /// </summary>
    public partial class AssetDetail : UserControl
    {
        private Asset Model;

        public AssetDetail()
        {
            InitializeComponent();
        }

        public AssetDetail(Asset model)
        {
            InitializeComponent();
            Model = model;
            Load();
        }

        private void Load()
        {
            // Set place holder name, id

            // Load Hierarchy

            // Load Images
            
            // Load Discussion
            
            // Load State 

            // Load object history

        }
    }
}
