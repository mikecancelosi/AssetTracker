using Quipu.View.Services;
using Quipu.ViewModels;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Quipu.Services
{
    /// <summary>
    /// Observes Navigation coordinator for when its current viewmodel actually changes
    /// When that viewmodel changes, this class creates the view and navigates away.
    /// </summary>
    public class NavigationObserver
    {
        /// <summary>
        /// Stored reference to the main content frame's navigation service
        /// </summary>
        private NavigationService nav;
        /// <summary>
        /// Coordinator to listen to for viewmodel changes
        /// </summary>
        private readonly INavigationCoordinator coordinator;
        /// <summary>
        /// Factory to use to create views
        /// </summary>
        private readonly IViewFactory viewFactory;
        public NavigationObserver(NavigationService navigationService, 
                                  INavigationCoordinator coord, 
                                  IViewFactory factory)
        {
            nav = navigationService;
            viewFactory = factory;
            coordinator = coord;
            coordinator.VMChanged += NavStore_VMChanged;
        }

        /// <summary>
        /// When the coordinator vm changes, we want to navigate to a new view.
        /// </summary>
        /// <param name="vm"></param>
        private void NavStore_VMChanged(ViewModel vm)
        {
            Page page = viewFactory.BuildView(coordinator, vm);
            nav.Navigate(page);
        }               
    }
}
