using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AssetTracker.View
{
    interface ISavable
    {
        event EventHandler OnSaveComplete;
        event EventHandler OnSaveRefused;
        ICommand ConfirmSave_Clicked { get; }
        ICommand RefuseSave_Clicked { get; }
        void OnNavigatingAway(Page v);
        void CheckForPromptSave(Action methodToCall);
    }
}
