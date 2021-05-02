using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AssetTracker.View.Commands
{
    public class IDReceiverCmd : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> action;
        private Func<object, bool> canExecute;

        public IDReceiverCmd(Action<object> pAction, Func<object, bool> pCanExecute)
        {
            action = pAction;
            canExecute = pCanExecute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}
