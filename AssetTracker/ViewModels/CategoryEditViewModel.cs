using Quipu.Services;
using Quipu.View.Commands;
using Quipu.ViewModels.Interfaces;
using Quipu.ViewModels.Services;
using DataAccessLayer;
using DataAccessLayer.Services;
using DataAccessLayer.Strategies;
using DomainModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Quipu.ViewModels
{
    public class CategoryEditViewModel : ViewModel,ISavable
    {
        /// <summary>
        /// Category to modify
        /// </summary>
        public AssetCategory Category { get; private set; }      

        /// <summary>
        /// Phases of this category
        /// </summary>
        public List<Phase> CurrentPhases
        {
            get
            {
                var orderedList = Category.Phases.OrderBy(x => x.ph_step);
                return orderedList.ToList();
            }
        }
        /// <summary>
        /// Heading to display based on if we are creating,cloning, or modifiying the category
        /// </summary>
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
        /// <summary>
        /// Name of the category
        /// </summary>
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
        #region Saving
        /// <summary>
        /// Whether or not the category has changes made that are not yet committed to the db
        /// </summary>
        public bool IsSavable => unitOfWork.HasChanges;
        /// <summary>
        /// Backing field for PromptSave
        /// </summary>
        private bool promptSave;
        /// <summary>
        /// Should the user be prompted to save
        /// </summary>
        public bool PromptSave
        {
            get => promptSave;
            set
            {
                promptSave = value;
                NotifyPropertyChanged("PromptSave");
            }
        }        
        /// <summary>
        /// Command to execute the save functionality
        /// </summary>
        public ICommand SaveCommand => new RelayCommand((s) => Save(), (s) => true);
        /// <summary>
        /// The user has been prompted to save before navigating away. The user rejected to do so. Continue onwards.
        /// </summary>
        public ICommand RefuseSave => new RelayCommand((s) => navCoordinator.NavigateToQueued(), (s) => true);
        /// <summary>
        /// Any violations found when attempting to save the category
        /// </summary>
        public List<Violation> SaveViolations { get; set; }
        /// <summary>
        /// Validator to use when saving the category
        /// </summary>
        private IModelValidator<AssetCategory> categoryValidator;
        #endregion

        /// <summary>
        /// Is the category new?
        /// </summary>
        public bool Creating { get; set; }
        /// <summary>
        /// Is this category a copy of another?
        /// </summary>
        public bool Cloning { get; set; }

        #region Repositories
        private IRepository<AssetCategory> categoryRepo;
        private IRepository<Phase> phaseRepo;
        #endregion

        /// <summary>
        /// NavCoordinator used to navigate to other pages.
        /// </summary>
        public INavigationCoordinator navCoordinator { get; set; }

        #region Deleting
        /// <summary>
        /// The user has been prompted to confirmed deletion. The user has confirmed. Proceed to delete.
        /// </summary>
        public ICommand DeleteConfirmed => new RelayCommand((s) => DeleteCategory(), (s) => true);
        /// <summary>
        /// Strategy to use when deleting the category
        /// </summary>
        private IDeleteStrategy<AssetCategory> catDeleteStrategy;
        #endregion

        #region Violations
        public Violation CategoryNameViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "ca_name") ?? null;
        public Violation PhasesViolation => SaveViolations?.FirstOrDefault(x => x.PropertyName == "Phases" || 
                                                                                x.PropertyName =="ph_name") ?? null;
        #endregion

        

        public CategoryEditViewModel(INavigationCoordinator coord,
                                     IUnitOfWork uow,
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

        /// <summary>
        /// Set the category value 
        /// </summary>
        /// <param name="cat">Category to modify</param>
        public void SetCategory(AssetCategory cat)
        {
            
            if (cat.ca_id > 0) // This category already exists, we are modifying.
            {
                Category = categoryRepo.GetByID(cat.ca_id);
                Cloning = false;
            }
            else // We are cloning
            {
                Category = cat;
                categoryRepo.Insert(Category);                
                Cloning = true;
            }
            Creating = false;
            NotifyPropertyChanged("Category");
            NotifyPropertyChanged("HeadingContext");
        }

        /// <summary>
        /// Save the category and commit to db
        /// </summary>
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

        /// <summary>
        /// Delete the category from the db.
        /// </summary>
        public void DeleteCategory()
        {
            catDeleteStrategy.Delete(unitOfWork, Category);
            unitOfWork.Commit();
            navCoordinator.NavigateToProjectSettings();
        }

        //TODO: This can be separated into its own control.
        #region Phases
        /// <summary>
        /// Create a new phase for the category
        /// </summary>
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

        /// <summary>
        /// Phase name was changed. change it in the local db
        /// </summary>
        /// <param name="phaseStep">Step of phase that was changed</param>
        /// <param name="newValue">New phase name value</param>
        public void OnPhaseNameChange(int phaseStep, string newValue)
        {
            Phase phase = Category.Phases.FirstOrDefault(x => x.ph_step == phaseStep);
            phase.ph_name = newValue;
            phaseRepo.Update(phase);
        }

        /// <summary>
        /// Move the given phase up a step
        /// </summary>
        /// <param name="phaseId"></param>
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

        /// <summary>
        /// Move the given phase down a step
        /// </summary>
        /// <param name="phaseId"></param>
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

        /// <summary>
        /// Delete the phase with the given step
        /// </summary>
        /// <param name="phaseStep">Step deleted</param>
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
        #endregion
    }
}
