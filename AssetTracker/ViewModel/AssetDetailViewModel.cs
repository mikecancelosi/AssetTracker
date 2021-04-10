using AssetTracker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.ViewModel
{
    public class AssetDetailViewModel : INotifyPropertyChanged
    {
        
        private Asset myasset;

        public event PropertyChangedEventHandler PropertyChanged;

        public Asset myAsset 
        {
            get
            {
                return myasset;
            }
            set
            {
                myasset = value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("myasset"));
            }
        }


        public AssetDetailViewModel()
        {
            myAsset = new Asset();
        }
    }
}
