using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AssetTracker.View.Controls
{
    /// <summary>
    /// Interaction logic for Searchbox.xaml
    /// </summary>
    public partial class Searchbox : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Event called when the user has selected a new item
        /// </summary>
        public static readonly RoutedEvent SelectionChangeRoutedEvent = EventManager.RegisterRoutedEvent("SelectionChange",
                                                                                              RoutingStrategy.Bubble,
                                                                                              typeof(RoutedEventHandler),
                                                                                              typeof(Searchbox));
        
        /// <summary>
        /// Handler for when the user has selected a new item
        /// </summary>
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

        /// <summary>
        /// Items filtered down by the userfilter
        /// </summary>
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

        /// <summary>
        /// DP for the items list
        /// </summary>
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items",
                                                                                              typeof(List<DatabaseBackedObject>),
                                                                                              typeof(Searchbox),
                                                                                              new PropertyMetadata(null,
                                                                                                                   new PropertyChangedCallback(OnItemsChanged)));

        /// <summary>
        /// DP for the base filter
        /// </summary>
        public static readonly DependencyProperty BaseFilterProperty = DependencyProperty.Register("BaseFilter",
                                                                                                     typeof(string),
                                                                                                     typeof(Searchbox),
                                                                                                     new PropertyMetadata(null,
                                                                                                                          new PropertyChangedCallback(BaseFilterChanged)));

       /// <summary>
       ///  DP for the currently selected item
       /// </summary>
        public static readonly DependencyProperty CurrentSelectionProperty = DependencyProperty.Register("CurrentSelection",
                                                                                                         typeof(DatabaseBackedObject),
                                                                                                         typeof(Searchbox),
                                                                                                         new PropertyMetadata(null,
                                                                                                                              new PropertyChangedCallback(SelectionChanged)));

        /// <summary>
        /// Introduction of property changed to update filtered item
        /// </summary>
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

        /// <summary>
        /// When items have changed we need to update the filtered items
        /// </summary>
        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Searchbox thisUserControl = (Searchbox)d;
            List<DatabaseBackedObject> dbos = e.NewValue as List<DatabaseBackedObject>;
            thisUserControl.Items = dbos;
            thisUserControl.NotifyPropertyChanged("FilteredItems");
        }

        /// <summary>
        /// When the base filter has changed we need to refetch items
        /// </summary>
        private static void BaseFilterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Searchbox thisUserControl = (Searchbox)d;
            string newValue = e.NewValue as string;
            thisUserControl.BaseFilter = newValue;
        }

        /// <summary>
        /// When the selection changes we need to alert whatever is listening to this searchbox that the selection has changed
        /// </summary>
        private static void SelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Searchbox thisUserControl = (Searchbox)d;
            DatabaseBackedObject newSelection = e.NewValue as DatabaseBackedObject;
            thisUserControl.SetCurrentSelection(newSelection);
        }

        /// <summary>
        /// Set the current selection and raise the event to any listeners
        /// </summary>
        /// <param name="newValue"></param>
        private void SetCurrentSelection(DatabaseBackedObject newValue)
        {
            CurrentSelection = newValue;
            RoutedEventArgs newEventArgs = new RoutedEventArgs(SelectionChangeRoutedEvent);
            RaiseEvent(newEventArgs);
        }

        /// <summary>
        /// When the input text has changes, that means the userfilter has changed and therefore the filtered item list should change
        /// </summary>
        private void InputText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsLoaded && ((TextBox)sender).IsFocused)
            {
                Results.IsOpen = true;
                UserFilter = ((TextBox)sender).Text;
                NotifyPropertyChanged("FilteredItems");
            }
        }

        /// <summary>
        /// When the textbox has focusx, we need to show the results list
        /// </summary>
        private void InputText_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsLoaded)
            {
                Results.IsOpen = true;
                UserFilter = "";
            }
        }

        /// <summary>
        /// When the textbox loses focus, we need to hide the results list, possibly select the user inputted option
        /// </summary>        
        private void InputText_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (IsLoaded)
            {
                string filter = UserFilter?.ToLower() ?? "";
                DatabaseBackedObject dbo = Items?.FirstOrDefault(x => x.ID.ToString().ToLower() == filter ||
                                        x.Name.ToLower() == filter) ?? null;

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

        /// <summary>
        /// If an item in the results panel was selected, we need to set that object to our current selection
        /// </summary>
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
