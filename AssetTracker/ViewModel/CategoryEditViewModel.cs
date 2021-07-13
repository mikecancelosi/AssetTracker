using AssetTracker.Model;
using AssetTracker.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssetTracker.ViewModel
{
    public class CategoryEditViewModel : ViewModel
    {
        public AssetCategory Category;
        public CategoryEditViewModel()
        {
            Category = new AssetCategory()
            {
                ca_id = -1,
                ca_name = "temp"
            };
        }

        public CategoryEditViewModel(AssetCategory cat)
        {
            Category = cat;
        }

        public bool Savable { get; set; }


        /*
        public void OnNewPhaseClicked()
        {
            Phase phaseInst = context.Phases.Create();
            phaseInst.ph_step = CurrentCategoryInst.Phases.Count + 1;
            CurrentCategoryInst.Phases.Add(phaseInst);
            NotifyPropertyChanged("CurrentPhases");
        }

        public void OnPhaseUpClicked(int index)
        {
            if (index > 1)
            {
                List<Phase> copyList = CurrentCategoryInst.Phases.ToList();
                Phase phaseAtIndex = CurrentPhases[index - 1];
                Phase swapPhase = CurrentPhases[index - 2];
                phaseAtIndex.ph_step = index - 1;
                swapPhase.ph_step = index;
                copyList.RemoveAt(index - 1);
                copyList.Insert(index - 2, phaseAtIndex);
                CurrentCategoryInst.Phases = copyList;
                NotifyPropertyChanged("CurrentPhases");
            }
        }
        public void OnPhaseDownClicked(int index)
        {
            if (index < CurrentPhases.Count)
            {
                List<Phase> copyList = CurrentCategoryInst.Phases.ToList();
                Phase phaseAtIndex = CurrentPhases[index - 1];
                Phase swapPhase = CurrentPhases[index];
                phaseAtIndex.ph_step = index + 1;
                swapPhase.ph_step = index;
                copyList.RemoveAt(index - 1);
                copyList.Insert(index, phaseAtIndex);
                CurrentCategoryInst.Phases = copyList;
                NotifyPropertyChanged("CurrentPhases");
            }
        }

        public void OnPhaseDelete(int index)
        {
            CurrentCategoryInst.Phases.Remove(CurrentCategoryInst.Phases.ToList()[index - 1]);
            for (int i = index - 1; i < CurrentCategoryInst.Phases.Count; i++)
            {
                CurrentCategoryInst.Phases.ToList()[i].ph_step = i + 1;
            }
            NotifyPropertyChanged("CurrentPhases");

        }

        public void OnSaveCategory()
        {
            if (CurrentCategoryInst.Phases.Count > 0)
            {
                context.SaveChanges();
                NotifyPropertyChanged("Categories");
                CurrentCategoryInst = new AssetCategory();
            }
        }*/

    }
}
