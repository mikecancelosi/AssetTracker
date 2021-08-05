using AssetTracker.ViewModels;
using DomainModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AssetTracker.View
{
    /// <summary>
    /// Interaction logic for Searchbox.xaml
    /// </summary>
    public partial class Searchbox : UserControl, INotifyPropertyChanged
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


        /// <summary>
        /// Currently selected item to display
        /// </summary>
        public DatabaseBackedObject CurrentSelection
        {
            get { return (DatabaseBackedObject)GetValue(CurrentSelectionProperty); }
            set { SetValue(CurrentSelectionProperty, value); }
        }
        /// <summary>
        /// source of items that should display in the popup
        /// </summary>
        public List<DatabaseBackedObject> Items
        {
            get { return (List<DatabaseBackedObject>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public List<DatabaseBackedObject> FilteredItems
        {
            get
            {
                return Items?.Where(x => x.ID.ToString().ToLower().Contains(UserFilter?.ToLower() ?? "") ||
                                      x.Name.ToLower().Contains(UserFilter?.ToLower() ?? "")).ToList() ?? new List<DatabaseBackedObject>();
            }
        }

        /// <summary>
        /// Filter set in initialization of the seachbox
        /// </summary>
        public string BaseFilter
        {
            get { return (string)GetValue(BaseFilterProperty); }
            set { SetValue(BaseFilterProperty, value); }
        }
        /// <summary>
        /// Filter set by the user trying to search results through the textbox
        /// </summary>
        public string UserFilter { get; set; }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items",
                                                                                              typeof(List<DatabaseBackedObject>),
                                                                                              typeof(Searchbox),
                                                                                              new PropertyMetadata(null,
                                                                                                                   new PropertyChangedCallback(OnItemsChanged)));

        public static readonly DependencyProperty BaseFilterProperty = DependencyProperty.Register("BaseFilter",
                                                                                                     typeof(string),
                                                                                                     typeof(Searchbox),
                                                                                                     new PropertyMetadata(null,
                                                                                                                          new PropertyChangedCallback(BaseFilterChanged)));

        public static readonly DependencyProperty CurrentSelectionProperty = DependencyProperty.Register("CurrentSelection",
                                                                                                         typeof(DatabaseBackedObject),
                                                                                                         typeof(Searchbox),
                                                                                                         new PropertyMetadata(null,
                                                                                                                              new PropertyChangedCallback(SelectionChanged)));


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Wrap event fire in a method for easy access
        /// </summary>
        /// <param name="info">The property that has changed</param>
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public Searchbox()
        {
            InitializeComponent();
        }

        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Searchbox thisUserControl = (Searchbox)d;
            List<DatabaseBackedObject> dbos = e.NewValue as List<DatabaseBackedObject>;
            thisUserControl.Items = dbos;
            thisUserControl.NotifyPropertyChanged("FilteredItems");
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

        private void SetCurrentSelection(DatabaseBackedObject newValue)
        {
            CurrentSelection = newValue;
            RoutedEventArgs newEventArgs = new RoutedEventArgs(SelectionChangeRoutedEvent);
            RaiseEvent(newEventArgs);
        }

        private void InputText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsLoaded && ((TextBox)sender).IsFocused)
            {
                Results.IsOpen = true;
                UserFilter = ((TextBox)sender).Text;
                NotifyPropertyChanged("FilteredItems");
            }
        }

        private void InputText_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsLoaded)
            {
                Results.IsOpen = true;
                UserFilter = "";
            }
        }

        private void InputText_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsLoaded)
            {
                DatabaseBackedObject dbo = Items.FirstOrDefault(x => x.ID.ToString().ToLower() == UserFilter.ToLower() ||
                                        x.Name.ToLower() == UserFilter.ToLower());

                if (dbo != null)
                {
                    SetCurrentSelection(dbo);
                }

                if (Results.IsOpen)
                {
                    Results.IsOpen = false;
                }

                UserFilter = "";
            }
        }

        private void MainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatabaseBackedObject dbo = MainGrid.SelectedItem as DatabaseBackedObject;
            if (dbo != null)
            {
                Results.IsOpen = false;
                SetCurrentSelection(dbo);
            }
        }
    }
}
