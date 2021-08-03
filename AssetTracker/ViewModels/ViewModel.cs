using AssetTracker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModels
{   
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// Wrap event fire in a method for easy access
        /// </summary>
        /// <param name="info">The property that has changed</param>
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        /// <summary>
        /// Context reference
        /// </summary>
        protected TrackerContext context = new TrackerContext();

    }
}
