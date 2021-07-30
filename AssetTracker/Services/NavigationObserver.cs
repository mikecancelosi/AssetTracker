using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View;
using AssetTracker.View.Services;
using AssetTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AssetTracker.Services
{
    public class NavigationObserver
    {
        private NavigationService nav;
        private readonly INavigationCoordinator coordinator;
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

        private void NavStore_VMChanged(ViewModel vm)
        {
            Page page = viewFactory.BuildView(coordinator, vm);
            nav.Navigate(page);
        }               
    }
}
