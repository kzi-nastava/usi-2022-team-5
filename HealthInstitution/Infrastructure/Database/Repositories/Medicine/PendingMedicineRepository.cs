﻿using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class PendingMedicineRepository : BaseRepository, IPendingMedicineRepository
    {
        private List<PendingMedicine> _pendingMedicines;
        public List<PendingMedicine> PendingMedicines { get => _pendingMedicines; }

        public PendingMedicineRepository(string fileName)
        {
            _fileName = fileName;
            _pendingMedicines = new List<PendingMedicine>();
        }

        public override void LoadFromFile()
        {
            _pendingMedicines = FileService.Deserialize<PendingMedicine>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize<PendingMedicine>(_fileName, _pendingMedicines);
        }

        public PendingMedicine FindByID(int id)
        {
            foreach (PendingMedicine medicine in _pendingMedicines)
            {
                if (medicine.ID == id) return medicine;
            }
            return null;
        }

        private bool IsNameAvailable(PendingMedicine medicine, string name)
        {
            foreach (PendingMedicine m in _pendingMedicines)
            {
                if (m.Name.Equals(name) && !m.Equals(medicine)) return false;
            }
            return true;
        }

        private bool CheckID(int id)
        {
            foreach (PendingMedicine m in _pendingMedicines)
            {
                if (m.ID == id) return false;
            }

            foreach (Medicine m in new MedicineRepositoryService().GetMedicines())
            {
                if (m.ID == id) return false;
            }

            return true;
        }

        private int GetID()
        {
            int i = 1;
            while (true)
            {
                if (CheckID(i)) return i;
                i++;
            }
        }

        public PendingMedicine AddNewMedicine(PendingMedicine newMedicine)
        {
            if (!IsNameAvailable(null, newMedicine.Name)) throw new NameNotAvailableException("Name already in use!");
            newMedicine.ID = GetID();
            _pendingMedicines.Add(newMedicine);

            return newMedicine;
        }

        public PendingMedicine ChangeMedicine(PendingMedicine medicine, string newName, List<Allergen> newIngredients)
        {
            if (newName is null || newName.Equals("")) throw new NameNotAvailableException("Name cannot be empty");
            else if (newIngredients.Count == 0) throw new ZeroIngredientsException("Ingredient list cannot be empty");
            else if (!IsNameAvailable(medicine, newName)) throw new NameNotAvailableException("Name already in use!");

            List<Allergen> alreadyIngredient = medicine.Ingredients.Except(newIngredients).ToList();
            List<Allergen> addedIngredients = newIngredients.Except(medicine.Ingredients).ToList();

            if ((!alreadyIngredient.Any() && !addedIngredients.Any()) && medicine.Name == newName)
                throw new NoChangeException("Change must be made!");

            medicine.Name = newName;
            medicine.Ingredients = newIngredients;

            return medicine;
        }

        public List<PendingMedicine> GetPendingMedicines()
        {
            return _pendingMedicines;
        }
    } 
}