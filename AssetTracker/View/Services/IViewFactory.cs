using AssetTracker.Services;
using AssetTracker.ViewModels;
using AssetTracker.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AssetTracker.View.Services
{
    public interface IViewFactory
    {
        Page BuildView(INavigationCoordinator coord, ViewModel vm);
    }
}
