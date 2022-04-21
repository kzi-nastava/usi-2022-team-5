﻿using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
namespace HealthInstitution.MVVM.Models.Repositories
{
    public class PatientRepository
    {
        private string _patientFileName;
        private List<PatientController> _patients;

        public PatientRepository(string patientFileName)
        {
            this._patientFileName = patientFileName;
            this._patients = new List<PatientController>();
        }

        public List<PatientController> GetPatients()
        {
            return this._patients;
        }

        public bool LoadFromFile()
        {
            // TODO: implementirati funkciju za ucitavanje podataka iz fajla
            return false;
        }

        public bool SaveToFile()
        {
            // TODO: implementirati funkciju za cuvanje podataka u fajl
            return false;
        }
    }
}
