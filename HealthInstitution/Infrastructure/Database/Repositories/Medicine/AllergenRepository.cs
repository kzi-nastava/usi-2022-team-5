﻿using System.Collections.Generic;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class AllergenRepository : BaseRepository, IAllergenRepository
    {
        private List<Allergen> _allergens;
        public List<Allergen> Allergens { get => _allergens; }

        public AllergenRepository(string fileName)
        {
            _fileName = fileName;
            _allergens = new List<Allergen>();
        }

        public override void LoadFromFile()
        {
            _allergens = FileService.Deserialize<Allergen>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize<Allergen>(_fileName, _allergens);
        }
        public Allergen FindByID(int id)
        {
            foreach (Allergen allergen in _allergens)
            {
                if (allergen.ID == id) return allergen;
            }
            return null;
        }

        public List<Allergen> PatientAllergenToAllergen (List<PatientAllergen> patientAllergens)
        {
            List<Allergen> allergens = new();
            foreach (PatientAllergen reference in patientAllergens)
            {
                allergens.Add(FindByID(reference.IngredientId));
            }
            return allergens;
        }

        public List<Allergen> MedicineAllergenToAllergen(List<MedicineAllergen> medicineAllergens)
        {
            List<Allergen> allergens = new();
            foreach (MedicineAllergen reference in medicineAllergens)
            {
                allergens.Add(FindByID(reference.IngredientId));
            }
            return allergens;
        }

        private bool CheckID(int id)
        {
            foreach (Allergen m in _allergens)
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

        public void ChangeName(Allergen allergen, string name)
        {
            allergen.Name = name;
        }

        public Allergen AddNewAllergen(Allergen allergen)
        {
            allergen.ID = GetID();
            _allergens.Add(allergen);
            return allergen;
        }

        public void DeleteAllergen(Allergen allergen)
        {
            _allergens.Remove(allergen);
        }

        public List<Allergen> GetAllergens()
        {
            return _allergens;
        }
    }
}
