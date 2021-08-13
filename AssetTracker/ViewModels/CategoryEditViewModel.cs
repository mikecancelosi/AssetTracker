using AssetTracker.Services;
using AssetTracker.View.Commands;
using AssetTracker.ViewModels.Interfaces;
using AssetTracker.ViewModels.Services;
using DataAccessLayer;
using DataAccessLayer.Strategies;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AssetTracker.ViewModels
{
    public class CategoryEditViewModel : ViewModel,ISavable
    {
        public AssetCategory Category { get; private set; }      

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
        public string CategoryName
        {
            get => Category.ca_name;
            set
            {
                Category.ca_name = value;
                categoryRepo.Update(Category);
                NotifyPropertyChanged("IsSavable");
            }
        }

        public bool IsSavable => unitOfWork.HasChanges;
        private bool promptSave;
        public bool PromptSave
        {
            get => promptSave;
            set
            {
                promptSave = value;
                NotifyPropertyChanged("PromptSave");
            }
        }
        public ICommand DeleteConfirmed =>  new RelayCommand((s) => DeleteCategory(), (s) => true);
        public ICommand SaveCommand => new RelayCommand((s) => Save(), (s) => true);
        public ICommand RefuseSave => new RelayCommand((s) => navCoordinator.NavigateToQueued(), (s) => true);

        public List<Violation> SaveViolations { get; set; }

        public bool Creating { get; set; }
        public bool Cloning { get; set; }

        private GenericRepository<AssetCategory> categoryRepo;
        private GenericRepository<Phase> phaseRepo;
        
        public INavigationCoordinator navCoordinator { get; set; }
        private IDeleteStrategy<AssetCategory> catDeleteStrategy;

        public Violation CategoryNameViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "ca_name") ?? null;
        public Violation PhasesViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "Phases" || 
                                                                                x.PropertyName =="ph_name") ?? null;

        private IModelValidator<AssetCategory> categoryValidator;

        public CategoryEditViewModel(INavigationCoordinator coord,
                                     GenericUnitOfWork uow,
                                     IDeleteStrategy<AssetCategory> catDeleteStrat,
                                     IModelValidator<AssetCategory> categoryValidatorService)
        {
            navCoordinator = coord;
            catDeleteStrategy = catDeleteStrat;
            unitOfWork = uow;
            categoryValidator = categoryValidatorService;

            navCoordinator.UserNavigationAttempt += (s) => PromptSave = true;           
            categoryRepo = unitOfWork.GetRepository<AssetCategory>();
            phaseRepo = unitOfWork.GetRepository<Phase>();
            Category = new AssetCategory();
            Creating = true;
        }

        public void SetCategory(AssetCategory cat)
        {
            if (cat.ca_id > 0)
            {
                Category = categoryRepo.GetByID(cat.ca_id);
            }
            else
            {
                Category = cat;
                categoryRepo.Insert(Category);                
                Cloning = true;
            }
            Creating = false;
            NotifyPropertyChanged("Category");
            NotifyPropertyChanged("HeadingContext");
        }

        public void Save()
        {
            categoryRepo.Update(Category);
            

            if(categoryValidator.IsValid(unitOfWork,Category,out List<Violation> violations))
            {
                unitOfWork.Commit();

                if (navCoordinator.WaitingToNavigate)
                {
                    navCoordinator.NavigateToQueued();
                }
                else
                {
                    Creating = false;
                    Cloning = false;
                    SaveViolations = null;
                    NotifyPropertyChanged("IsSavable");
                    NotifyPropertyChanged("HeadingContent");
                    NotifyPropertyChanged("CurrentPhases");
                    NotifyPropertyChanged("Category");
                    NotifyPropertyChanged("CategoryNameViolation");
                    NotifyPropertyChanged("PhasesViolation");
                }
            }
            else
            {
                SaveViolations = violations;
                NotifyPropertyChanged("SaveViolations");
                NotifyPropertyChanged("CategoryNameViolation");
                NotifyPropertyChanged("PhasesViolation");
            }
        }

        public void DeleteCategory()
        {
            catDeleteStrategy.Delete(unitOfWork, Category);
            unitOfWork.Commit();
            navCoordinator.NavigateToProjectSettings();
        }

        public void OnNewPhaseClicked()
        {
            Phase phaseInst = new Phase();
            phaseInst.ph_step = Category.Phases.Count + 1;
            phaseRepo.Insert(phaseInst);
            Category.Phases.Add(phaseInst);
            categoryRepo.Update(Category);
            NotifyPropertyChanged("CurrentPhases");
            NotifyPropertyChanged("IsSavable");
        }

        public void OnPhaseNameChange(int phaseStep, string newValue)
        {
            Phase phase = Category.Phases.FirstOrDefault(x => x.ph_step == phaseStep);
            phase.ph_name = newValue;
            phaseRepo.Update(phase);
        }

        public void OnPhaseUpClicked(int phaseId)
        {
            Phase phase = Category.Phases.FirstOrDefault(x=>x.ph_id == phaseId);
            int beforeStep = phase.ph_step;
            if (beforeStep > 1)
            {               
                Phase swapPhase = Category.Phases.FirstOrDefault(x => x.ph_caid == phase.ph_caid &&
                                                                      x.ph_step == (beforeStep - 1));
                phase.ph_step = beforeStep - 1;
                swapPhase.ph_step = beforeStep;              

                Category.Phases.FirstOrDefault(x => x.ph_id == phase.ph_id).ph_step = beforeStep - 1;
                Category.Phases.FirstOrDefault(x => x.ph_id == swapPhase.ph_id).ph_step = beforeStep ;

                phaseRepo.Update(phase);
                phaseRepo.Update(swapPhase);
                categoryRepo.Update(Category);

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

                phase.ph_step = beforeStep + 1;
                swapPhase.ph_step = beforeStep;

                Category.Phases.FirstOrDefault(x => x.ph_id == phase.ph_id).ph_step = beforeStep + 1;
                Category.Phases.FirstOrDefault(x => x.ph_id == swapPhase.ph_id).ph_step = beforeStep;

                phaseRepo.Update(phase);
                phaseRepo.Update(swapPhase);
                categoryRepo.Update(Category);

                NotifyPropertyChanged("CurrentPhases");
            }
        }

        public void OnPhaseDelete(int phaseStep)
        {
            Phase phase = Category.Phases.FirstOrDefault(x => x.ph_step == phaseStep);
            int index = phase.ph_step;
            Category.Phases.Remove(phase);
            phaseRepo.Delete(phase);

            for (int i = index ; i < Category.Phases.Count+1; i++)
            {
                Phase phaseToEdit = Category.Phases.FirstOrDefault(x => x.ph_step == i +1);
                int afterStep = phaseToEdit.ph_step - 1;
                phaseToEdit.ph_step = afterStep;
                phaseRepo.Update(phaseToEdit);
            }

            NotifyPropertyChanged("CurrentPhases");
            NotifyPropertyChanged("IsSavable");
        }     
    }
}
