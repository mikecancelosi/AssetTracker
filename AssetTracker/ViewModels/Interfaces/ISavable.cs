using AssetTracker.Services;
using DomainModel;
using System.Collections.Generic;
using System.Windows.Input;

namespace AssetTracker.ViewModels.Interfaces
{
    public interface ISavable
    {
        bool IsSavable { get; }
        INavigationCoordinator navCoordinator { get; set; }
        void Save();
        bool PromptSave { get; set; }
        List<Violation> SaveViolations { get; set; }
        ICommand RefuseSave { get; }
        ICommand SaveCommand { get;}

    }
}
