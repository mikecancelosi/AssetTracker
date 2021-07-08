using AssetTracker.Extensions;
using AssetTracker.Model;
using AssetTracker.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace AssetTracker.ViewModel
{
    public class ProjectSettingsViewModel : ViewModel
    {
        private List<User> users;
        public List<User> Users
        {
            get
            {
                if (users == null)
                {
                    users = new List<User>();
                    var temp = (from a in context.Users
                                select a).ToList();
                    users = temp;
                }

                return users;
            }
        }

        private List<SecRole> roles;
        public List<SecRole> Roles
        {
            get
            {
                if (roles == null)
                {
                    roles = new List<SecRole>();
                    var temp = (from a in context.SecRoles
                                select a).ToList();
                    roles = temp;
                }

                return roles;
            }
        }

        private List<AssetCategory> categories;
        public List<AssetCategory> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = new List<AssetCategory>();
                    var temp = (from a in context.AssetCategories
                                select a).ToList();
                    categories = temp;
                }

                return categories;
            }
        }

        public AssetCategory CurrentCategoryInst { get; set; }

        public List<Phase> CurrentPhases
        {
            get
            {
                if (CurrentCategoryInst != null)
                {
                    return CurrentCategoryInst.Phases.ToList();
                }
                else
                {
                    return new List<Phase>();
                }
            }
            set
            {
                if (CurrentCategoryInst != null)
                {
                    CurrentCategoryInst.Phases = value;
                }
            }
        }      

        public void OnNewCategoryClicked()
        {
            CurrentCategoryInst = context.AssetCategories.Create();
        }

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
            CurrentCategoryInst.Phases.Remove(CurrentCategoryInst.Phases.ToList()[index -1 ]);
            for (int i = index -1; i < CurrentCategoryInst.Phases.Count; i++)
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
        }

        public void OnExitCategory()
        {
            context.RevertChanges();
            CurrentCategoryInst = new AssetCategory();
        }

        public void OnCategorySelectedForEdit(int id)
        {
            CurrentCategoryInst = context.AssetCategories.Find(id);
            NotifyPropertyChanged("CurrentCategoryInst");
            NotifyPropertyChanged("CurrentPhases");
        }

       
    }
}
