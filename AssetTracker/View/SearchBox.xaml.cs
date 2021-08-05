using AssetTracker.ViewModels;
using DomainModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AssetTracker.View
{
    /// <summary>
    /// Interaction logic for Searchbox.xaml
    /// </summary>
    public partial class Searchbox : UserControl
    {
        public static readonly RoutedEvent SelectionChangeRoutedEvent = EventManager.RegisterRoutedEvent("SelectionChange",
                                                                                              RoutingStrategy.Bubble,
                                                                                              typeof(RoutedEventHandler),
                                                                                              typeof(Searchbox));
        public event RoutedEventHandler SelectionChange
        {
            add { this.AddHandler(SelectionChangeRoutedEvent, value); }
            remove { this.RemoveHandler(SelectionChangeRoutedEvent, value); }            
        }


        public DatabaseBackedObject CurrentSelection { get; set; }
        public ObservableCollection<DatabaseBackedObject> Items { get; set; }
        /// <summary>
        /// Filter set in initialization of the seachbox
        /// </summary>
        public string BaseFilter { get; set; }
        /// <summary>
        /// Filter set by the user trying to search results through the textbox
        /// </summary>
        public string UserFilter { get; set; }

        public static readonly DependencyProperty ItemsProperty =
         DependencyProperty.Register("Items", typeof(IList), typeof(Searchbox),
         new PropertyMetadata(null, new PropertyChangedCallback(OnItemsChanged)));

        public static readonly DependencyProperty UserFilterProperty =
       DependencyProperty.Register("UserFilter", typeof(string), typeof(Searchbox),
       new PropertyMetadata(null, new PropertyChangedCallback(UserFilterChanged)));

        public static readonly DependencyProperty BaseFilterProperty =
       DependencyProperty.Register("BaseFilter", typeof(string), typeof(Searchbox),
       new PropertyMetadata(null, new PropertyChangedCallback(BaseFilterChanged)));

        public static readonly DependencyProperty CurrentSelectionProperty =
     DependencyProperty.Register("CurrentSelection", typeof(DatabaseBackedObject), typeof(Searchbox),
     new PropertyMetadata(null, new PropertyChangedCallback(SelectionChanged)));


        public Searchbox()
        {
            InitializeComponent();
        }

        private void InputText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsLoaded)
            {
                Results.IsOpen = true;
            }
        }

        private void InputText_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsLoaded)
            {
                Results.IsOpen = true;
            }
        }

        private void InputText_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsLoaded)
            {
                if (Results.IsOpen)
                {
                    Results.IsOpen = false;
                }
            }
        }

        private void MainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatabaseBackedObject dbo = MainGrid.SelectedItem as DatabaseBackedObject;
            if (dbo != null)
            {

                Results.IsOpen = false;
            }
            else
            {

            }
        }

        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Searchbox thisUserControl = (Searchbox)d;
            (e.NewValue as INotifyCollectionChanged).CollectionChanged += thisUserControl.Searchbox_CollectionChanged;
        }

        private static void UserFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Searchbox thisUserControl = (Searchbox)d;
            string newValue = e.NewValue as string;
            thisUserControl.UserFilter = newValue;
        }

        private static void BaseFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Searchbox thisUserControl = (Searchbox)d;
            string newValue = e.NewValue as string;
            thisUserControl.BaseFilter = newValue;
        }


        private static void SelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Searchbox thisUserControl = (Searchbox)d;
            DatabaseBackedObject newSelection = e.NewValue as DatabaseBackedObject;
            thisUserControl.SetCurrentSelection(newSelection);            
        }

        private void Searchbox_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SetCurrentSelection(DatabaseBackedObject newValue)
        {
            CurrentSelection = newValue;
            RoutedEventArgs newEventArgs = new RoutedEventArgs(SelectionChangeRoutedEvent);
            RaiseEvent(newEventArgs);
        }

    }
}
