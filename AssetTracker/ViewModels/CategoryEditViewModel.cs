﻿using AssetTracker.Model;
using AssetTracker.Services;
using AssetTracker.View;
using AssetTracker.View.Commands;
using AssetTracker.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AssetTracker.ViewModels
{
    public class CategoryEditViewModel : ViewModel,ISavable
    {
        public AssetCategory Category { get; set; }
        private List<Phase> phasesToDelete = new List<Phase>();

        public List<Phase> CurrentPhases
        {
            get
            {
                var orderedList = Category.Phases.OrderBy(x => x.ph_step);
                return orderedList.ToList();
            }
        }
        public string HeadingContent
        {
            get
            {
                if (Creating)
                {
                    return "Create Category";
                }
                else if (Cloning)
                {
                    return "Clone Category";
                }
                else
                {
                    return "Modify Category";
                }
            }
        }
        public bool IsSavable => context.ChangeTracker.HasChanges();
        public bool PromptSave { get; set; }
        public ICommand DeleteConfirmed { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelSave { get; set; }
        public List<Violation> SaveViolations { get; set; }


        public bool Creating { get; set; }
        public bool Cloning { get; set; }

        public INavigationCoordinator navCoordinator { get; set; }
        public CategoryEditViewModel(INavigationCoordinator coord)
        {
            navCoordinator = coord;
            Category = context.AssetCategories.Create();
            Creating = true;
            DeleteConfirmed = new RelayCommand((s) => DeleteCategory(), (s) => true);
            SaveCommand = new RelayCommand((s) => Save(), (s) => true);
            CancelSave = new RelayCommand((s) => navCoordinator.NavigateToQueued(), (s) => true);
        }

        public void SetCategory(AssetCategory cat)
        {
            if (cat.ca_id > 0)
            {
                Category = context.AssetCategories.Find(cat.ca_id);
            }
            else
            {
                Category = context.AssetCategories.Add(cat);                
                Cloning = true;
            }
            Creating = false;
            NotifyPropertyChanged("Category");
            NotifyPropertyChanged("HeadingContext");
        }

        public void Save()
        {
            var violations = new List<Violation>();
            foreach (var phase in phasesToDelete)
            {
                phase.Delete(context);
            }
            if (Category.Save(context, out violations))
            {
                context.SaveChanges();
                Creating = false;
                Cloning = false;
                NotifyPropertyChanged("Savable");
                NotifyPropertyChanged("HeadingContent");
                NotifyPropertyChanged("CurrentPhases");
                NotifyPropertyChanged("Category");
            }
            else
            {
                SaveViolations = violations;
                NotifyPropertyChanged("SaveViolations");
                throw new NotImplementedException();
            }
        }

        public void DeleteCategory()
        {
            Category.Delete(context);
            navCoordinator.NavigateToProjectSettings();
        }

        public void OnCategoryNameChanged(string newName)
        {
            context.Entry(Category).Property(x => x.ca_name).CurrentValue = newName;
            NotifyPropertyChanged("Savable");
        }


        public void OnNewPhaseClicked()
        {
            Phase phaseInst = context.Phases.Create();
            phaseInst.ph_step = Category.Phases.Count + 1;
            context.Phases.Add(phaseInst);
            Category.Phases.Add(phaseInst);
            NotifyPropertyChanged("CurrentPhases");
            NotifyPropertyChanged("Savable");
        }

        public void OnPhaseNameChange(int phaseStep, string newValue)
        {
            Phase phase = Category.Phases.FirstOrDefault(x => x.ph_step == phaseStep);
            context.Entry(phase).Property(x => x.ph_name).CurrentValue = newValue;
            phase.ph_name = newValue;
        }

        public void OnPhaseUpClicked(int phaseId)
        {
            Phase phase = Category.Phases.FirstOrDefault(x=>x.ph_id == phaseId);
            int beforeStep = phase.ph_step;
            if (beforeStep > 1)
            {               
                Phase swapPhase = Category.Phases.FirstOrDefault(x => x.ph_caid == phase.ph_caid &&
                                                                      x.ph_step == (beforeStep - 1));
                context.Entry(phase).Property(x => x.ph_step).CurrentValue = beforeStep - 1;
                context.Entry(swapPhase).Property(x => x.ph_step).CurrentValue = beforeStep;

                Category.Phases.FirstOrDefault(x => x.ph_id == phase.ph_id).ph_step = beforeStep - 1;
                Category.Phases.FirstOrDefault(x => x.ph_id == swapPhase.ph_id).ph_step = beforeStep ;

                NotifyPropertyChanged("CurrentPhases");
            }
            
        }
        public void OnPhaseDownClicked(int phaseId)
        {
            Phase phase = Category.Phases.FirstOrDefault(x => x.ph_id == phaseId);
            int beforeStep = phase.ph_step;
            if (beforeStep < Category.Phases.Count)
            {
                Phase swapPhase = Category.Phases.FirstOrDefault(x => x.ph_caid == phase.ph_caid &&
                                                                      x.ph_step == (beforeStep + 1));
                context.Entry(phase).Property(x => x.ph_step).CurrentValue = beforeStep  + 1;
                context.Entry(swapPhase).Property(x => x.ph_step).CurrentValue = beforeStep;

                Category.Phases.FirstOrDefault(x => x.ph_id == phase.ph_id).ph_step = beforeStep + 1;
                Category.Phases.FirstOrDefault(x => x.ph_id == swapPhase.ph_id).ph_step = beforeStep;

                NotifyPropertyChanged("CurrentPhases");
            }
        }

        public void OnPhaseDelete(int phaseId)
        {
            Phase phase = Category.Phases.FirstOrDefault(x => x.ph_id == phaseId);
            int index = phase.ph_step;
            Category.Phases.Remove(phase);
            for (int i = index; i < Category.Phases.Count+1; i++)
            {
                Phase phaseToEdit = Category.Phases.FirstOrDefault(x => x.ph_step == i +1);
                int afterStep = phaseToEdit.ph_step - 1;
                context.Entry(phaseToEdit).Property(x => x.ph_step).CurrentValue = afterStep;
                phaseToEdit.ph_step = afterStep;
            }
            phasesToDelete.Add(phase);
            NotifyPropertyChanged("CurrentPhases");
            NotifyPropertyChanged("Savable");
        }     
    }
}
