﻿using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.Repositories
{
    public class DoctorRepository
    {
        private string _fileName;
        private List<Doctor> _doctors;

        public DoctorRepository(string fileName)
        {
            this._fileName = fileName;
            this._doctors = new List<Doctor>();
        }

        public List<Doctor> GetDoctors()
        {
            return this._doctors;
        }

        public void LoadFromFile()
        {
            _doctors = FileService.Deserialize<Doctor>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Doctor>(_fileName, _doctors);
        }
        public Doctor FindByID(int id)
        {
            foreach(Doctor doctor in _doctors)
            {
                if (doctor.ID == id) return doctor;
            }
            return null;
        }
    }
}
