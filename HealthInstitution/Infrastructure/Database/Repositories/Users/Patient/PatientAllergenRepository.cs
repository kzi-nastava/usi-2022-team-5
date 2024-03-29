﻿using System.Collections.Generic;
using HealthInstitution.Core.Services;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Repositories.References
{
    public class PatientAllergenRepository : BaseRepository, IPatientAllergenRepository
    {
        private List<PatientAllergen> _references;

        public PatientAllergenRepository(string FileName)
        {
            _fileName = FileName;
            _references = new List<PatientAllergen>();
        }
        public List<PatientAllergen> GetReferences()
        {
            return _references;
        }

        public override void LoadFromFile()
        {
            _references = FileService.Deserialize<PatientAllergen>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize<PatientAllergen>(_fileName, _references);
        }

        public List<PatientAllergen> FindByPatientID(int patientId)
        {
            List<PatientAllergen> patientAllergens = new();
            foreach (PatientAllergen reference in _references)
            {
                if (reference.PatientId == patientId)
                    patientAllergens.Add(reference);
            }
            return patientAllergens;
        }

        public bool Add(Allergen allergen, Patient patient)
        {
            if (allergen == null) throw new EmptyFieldException("The allergen is not selected !");

            PatientAllergen patientAllergen = new PatientAllergen(patient.ID, allergen.ID);
            foreach (Allergen existingAllergen in patient.Record.Allergens)
            {
                if (existingAllergen.ID == allergen.ID)
                {
                    throw new ExistingAllergenException("This allergen already exists in medical record !");
                }
            }
            _references.Add(patientAllergen);
            patient.Record.Allergens.Add(allergen);
            return true;
        }
    }
}
