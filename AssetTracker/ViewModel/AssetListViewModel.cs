using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Linq;

namespace AssetTracker.ViewModel
{
    public class AssetListViewModel
    {
        private List<Asset> assets;
        public List<Asset> Assets
        {
            get
            {
                if (assets == null)
                {
                    assets = new List<Asset>();
                    var temp = (from a in context.Assets
                                select a).ToList();
                    assets = temp;
                }

                return assets;
            }
        }
        private TrackerContext context = new TrackerContext();

        public void AddItem()
        {

        }

        


    }
}
