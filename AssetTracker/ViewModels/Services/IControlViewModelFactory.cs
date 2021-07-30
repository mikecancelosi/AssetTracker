using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModels.Services
{
    public interface IControlViewModelFactory
    {
        SearchBoxViewModel BuildSearchboxViewModel(Type type, string baseFilter = "", int startingID = 0);
    }
}
