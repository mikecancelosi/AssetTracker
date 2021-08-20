using Quipu.ViewModels;
using DomainModel;
using System.Windows;
using System.Windows.Controls;

namespace Quipu.View
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
            VM.OnPhaseDelete(senderPhase.ph_step);
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
    }
}
