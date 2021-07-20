using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AssetTracker.View
{
    interface ISavable
    {       
        event EventHandler OnSaveComplete;
        event EventHandler OnSaveRefused;       
        void Page_Loaded(object sender, RoutedEventArgs e); //Need to subscribe to the onnavigateaway after navigation has already happened
        ICommand ConfirmSave_Clicked { get;  }
        ICommand RefuseSave_Clicked { get; }
        void OnNavigatingAway();
        void CheckForPromptSave(Action methodToCall);
    }
}
