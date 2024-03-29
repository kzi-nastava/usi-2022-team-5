﻿using System.Collections.Generic;
using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.MedicineCommands
{
    class ChangeRejectedMedicineCommand : BaseCommand
    {
        private AdminMedicineViewModel _model;
        private IPendingMedicineRepositoryService _pendingMedicines;

        public ChangeRejectedMedicineCommand(AdminMedicineViewModel model)
        {
            _model = model;
            _pendingMedicines = new PendingMedicineRepositoryService();
        }

        public override void Execute(object parameter)
        {
            string name = _model.SelectedName;

            List<Allergen> newIngredients = new List<Allergen>();

            foreach (IngredientListItemViewModel ingredient in _model.Ingredients)
            {
                if (ingredient.Selected) newIngredients.Add(ingredient.Ingredient);
            }

            try
            {
                PendingMedicine m = _pendingMedicines.ChangeMedicine(_model.SelectedMedicine.Medicine, _model.SelectedName, newIngredients);

                m.State = State.ON_HOLD;

                _model.FillMedicineList();
                _model.DialogOpen = false;
            }
            catch (NameNotAvailableException e)
            {
                _model.ShowMessage(e.Message);
            }
            catch (ZeroIngredientsException e)
            {
                _model.ShowMessage(e.Message);
            }
            catch (NoChangeException e)
            {
                _model.ShowMessage(e.Message);
            }
        }
    }
}
