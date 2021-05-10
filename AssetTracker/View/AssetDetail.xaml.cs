using AssetTracker.Model;
using AssetTracker.View.Commands;
using AssetTracker.View.Properties;
using AssetTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
using static AssetTracker.ViewModel.AssetDetailViewModel;

namespace AssetTracker.View
{
    /// <summary>
    /// Interaction logic for AssetDetail.xaml
    /// </summary>
    public partial class AssetDetail : Page
    {
        public AssetDetailViewModel vm;
        public AssetDetail()
        {
            InitializeComponent();
        }
        


        public AssetDetail(Asset model)
        {
            InitializeComponent();
            vm = new AssetDetailViewModel();
            vm.myAsset = model;
            DataContext = vm;

            Searchbox_AssignedTo.SetType(typeof(User));
            if (vm.myAsset.AssignedToUser != null)
            {
                Searchbox_AssignedTo.SetCurrentSelectedObject(vm.myAsset.AssignedToUser.ID);
            }
            Searchbox_AssignedTo.PropertyChanged += (s, e) => { vm.OnAssignedUserChange(Searchbox_AssignedTo.CurrentSelection.ID); };

            Searchbox_Phase.SetType(typeof(Phase));
            if (vm.myAsset.Phase != null)
            {
                Searchbox_Phase.SetCurrentSelectedObject(vm.myAsset.Phase.ID);
            }
            Searchbox_Phase.PropertyChanged += (s, e) => { vm.OnPhaseChanged(Searchbox_Phase.CurrentSelection.ID); };

            InitializeTimeline();
        }

        private void HierarchyObjectClicked(object sender, RoutedEventArgs e)
        {
            PromptSave();
            // Change viewmodel to new asset

        }

        private void OnClose()
        {
            PromptSave();
        }

        private void OnDeleteClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            List<Violation> violations = new List<Violation>();
            if (vm.OnSave(out violations))
            {

            }
            else
            {

            }
        }

        private void OnChangelogClicked(object sender, RoutedEventArgs e)
        {
            if (Changelog.Visibility == Visibility.Visible)
            {
                Changelog.Visibility = Visibility.Collapsed;
            }
            else
            {
                Changelog.Visibility = Visibility.Visible;
            }
        }

        private void OnChangelogExited(object sender, RoutedEventArgs e)
        {
            Changelog.Visibility = Visibility.Collapsed;
        }
      

        private void PromptSave()
        {

        }

        #region Timeline
        protected LinkedList<Control> DateControls = new LinkedList<Control>();
        protected int DateOffset = 75;
        protected int GetViewableDateCount => (int)Math.Floor((TimelineCanvas.Width / DateOffset));
        protected int GetDateCount => GetViewableDateCount + 2;
        protected DateTime startDate;
        protected static int PhaseHeight = 50;
        protected int hInitialOff = 10;
        protected int vIntialOffPhase = 50;
        protected int maxVisibleCount = 10;
        protected bool reachedStart = true;
        protected string startDateDisplayString; 


        private void InitializeTimeline()
        {
            //Init dates
            startDate = vm.GetCreationDate();
            ControlTemplate dateTemplate = (ControlTemplate) TryFindResource("DateTemplate");
            CultureInfo culture = new CultureInfo("en-US");
            for (int i = 0; i < GetDateCount; i++)
            {
                Control dateControlInst = new Control();
                dateControlInst.Template = dateTemplate;
                TimeSpan addedDay = new TimeSpan(i, 0, 0, 0);
                DateTime newDay = startDate + addedDay;
                string displayString = newDay.Month + "/" + newDay.Day;
                if(i == 0)
                {
                    startDateDisplayString = displayString;
                }
                TextPropertyExtension.SetTextSource(dateControlInst, displayString);
                TimelineCanvas.Children.Add(dateControlInst);
                Canvas.SetTop(dateControlInst, 10);
                Canvas.SetLeft(dateControlInst, (DateOffset * i) + hOff + hInitialOff);
                DateControls.AddLast(dateControlInst);
            }

            // Phase blocks
            InitPhaseBlocks();
        }

        #region DragScrolling
        private Point scrollMousePoint = new Point();
        private double hOff = 1;
        private double TimelineOffsetX = 0;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(TimelineCanvas);
            hOff = TimelineOffsetX;
            TimelineCanvas.CaptureMouse();
        }

        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (TimelineCanvas.IsMouseCaptured)
            {
                ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(TimelineCanvas).X));
            }
        }

        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimelineCanvas.ReleaseMouseCapture();
        }

        private void ScrollToHorizontalOffset(double newX)
        {           
            double deltaX = TimelineOffsetX - newX;
            double canvasWidth = TimelineCanvas.ActualWidth;
            //Move Dates
            int j = DateControls.Count;
            for (int i = 0; i < j; i++)
            {
                Control con = DateControls.ElementAt(i);
                double leftStart = Canvas.GetLeft(con);
                double leftEnd = leftStart + deltaX;
                if (leftEnd >= (canvasWidth + DateOffset))
                {
                    DateControls.Remove(con);
                    DateControls.AddFirst(con);
                    startDate = startDate - new TimeSpan(1, 0, 0, 0);
                    string displayString = startDate.Month + "/" + startDate.Day;
                    TextPropertyExtension.SetTextSource(con, displayString);

                    double nextLeft = Canvas.GetLeft(DateControls.ElementAt(1));
                    Canvas.SetLeft(con, nextLeft - DateOffset);                    
                }
                else if (leftEnd <= (-DateOffset))
                {
                    DateControls.Remove(con);
                    DateControls.AddLast(con);

                    startDate = startDate + new TimeSpan(1, 0, 0, 0);
                    DateTime newDate = startDate + new TimeSpan(GetDateCount -1, 0, 0, 0);
                    string displayString = newDate.Month + "/" + newDate.Day;
                    TextPropertyExtension.SetTextSource(con, displayString);

                    double nextLeft = Canvas.GetLeft(DateControls.ElementAt(DateControls.Count - 2));
                    Canvas.SetLeft(con, nextLeft + DateOffset);
                    i--;
                    j--;
                }
                else
                {
                    Canvas.SetLeft(con, leftEnd);
                }

                if(startDate == vm.GetCreationDate() &&
                   Canvas.GetLeft(con) <= hInitialOff)
                {
                    reachedStart = true;
                }
                
            }


            //Move blocks
            double newPhaseX = Canvas.GetLeft(PhaseChunksParent) + deltaX;
            Canvas.SetLeft(PhaseChunksParent, newPhaseX);

            //Adjust tooltips

            TimelineOffsetX = newX;
        }



        #endregion

        private void InitPhaseBlocks()
        {
            Canvas.SetLeft(PhaseChunksParent, 0);
            int offsetY = vIntialOffPhase;
            // Phase blocks
            foreach (PhaseTimelineObject phaseObject in vm.PhaseTimelineObjects)
            {
                Rectangle phaseRect = new Rectangle();
                phaseRect.Height = PhaseHeight;
                TimeSpan diffInDays;
                diffInDays = phaseObject.EndDate - phaseObject.StartDate;
                phaseRect.Width = diffInDays.Days * DateOffset;
                double startPointX = ((phaseObject.StartDate - startDate).Days * DateOffset) + hOff + hInitialOff;
                PhaseChunksParent.Children.Add(phaseRect);
                Canvas.SetLeft(phaseRect, startPointX);
                Canvas.SetTop(phaseRect, offsetY);
                offsetY += PhaseHeight;
                phaseRect.Fill = new SolidColorBrush(Colors.Red);
            }
        }

        #endregion

        #region Discussion

        public ICommand DiscussionReplyClicked => new IDReceiverCmd((arr) => OnDiscussionReplyClicked(arr), (arr) => { return true; });
        public void OnDiscussionReplyClicked(object input)
        {
            string defaultText = "Start a new discussion..";
            object[] values = input as object[];
            int parentID = (int)values[0];
            string content = values[1] as string;
            if (content != defaultText)
            {
                vm.CreateNewDiscussion(parentID, content);
            }

        }
        #endregion
    }
}
