using Quipu.Services;
using Quipu.ViewModels;
using System.Windows.Controls;

namespace Quipu.View.Services
{
    public interface IViewFactory
    {
        Page BuildView(INavigationCoordinator coord, ViewModel vm);
    }
}
