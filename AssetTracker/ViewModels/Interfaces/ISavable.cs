using AssetTracker.Model;
using AssetTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModels.Interfaces
{
    public interface ISavable
    {
        bool IsSavable { get; }
        INavigationCoordinator navCoordinator { get; set; }
        void Save();
        bool PromptSave { get; set; }
        List<Violation> SaveViolations { get; set; }

    }
}
