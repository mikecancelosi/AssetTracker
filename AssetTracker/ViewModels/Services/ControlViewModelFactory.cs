using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModels.Services
{
    public class ControlViewModelFactory : IControlViewModelFactory
    {
        public SearchBoxViewModel BuildSearchboxViewModel(Type type, string baseFilter = "", int startingID = 0)
        {
            //TODO: Verify type is of type DBO
            SearchBoxViewModel vm = new SearchBoxViewModel(type);
            if(startingID != 0)
            {
                vm.SetSelectedItem(startingID);
            }
            if(baseFilter != "")
            {
                vm.SetBaseFilter(baseFilter);
            }
            return vm;
        }
    }
}
