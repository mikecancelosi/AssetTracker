using AssetTracker.Services;
using AssetTracker.ViewModels;
using System.Windows.Controls;

namespace AssetTracker.View.Services
{
    public interface IViewFactory
    {
        Page BuildView(INavigationCoordinator coord, ViewModel vm);
    }
}
