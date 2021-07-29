using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.ViewModels;
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
        public CategoryEditViewModel VM
        {
            get { return (CategoryEditViewModel)DataContext; }
            set { DataContext = value; }
        }
         public CategoryEdit(CategoryEditViewModel vm)
        {
            InitializeComponent();
            VM = vm;
        }       

        public void NavigateToProjectSettings(object sender, RoutedEventArgs e)
        {
            //CheckForPromptSave(() => coordinator.NavigateToProjectSettings());
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
