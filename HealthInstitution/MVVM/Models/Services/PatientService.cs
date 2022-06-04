﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Repositories;

namespace HealthInstitution.MVVM.Models.Services
{
    public class PatientService
    {
        private readonly PatientRepository _patientRepository;

        public PatientService()
        {
            _patientRepository = Institution.Instance().PatientRepository;
        }

        public void CreatePatient(string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight)
        {
            _patientRepository.CreatePatient(firstName, lastName, email, password, gender, height, weight);
        }

        public void UpdatePatient(int id, string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight)
        {
            Patient patient = _patientRepository.FindByID(id);
            patient.Update(id, firstName, lastName, email, password, gender, height, weight);
        }

        public void DeletePatient(int id)
        {
            Patient patient = _patientRepository.FindByID(id);
            patient.Delete();
            AppointmentService.DeleteFutureAppointments(patient);
        }

        public void BlockPatient(int id)
        {
            Patient patient = _patientRepository.FindByID(id);
            patient.Block();
        }

        public void UnblockPatient(int id)
        {
            Patient patient = _patientRepository.FindByID(id);
            patient.Unblock();
        }

        public void AddIllness(Patient patient, string illness)
        {
            foreach (Patient i in _patientRepository.Patients)
            {
                if (patient.ID == i.ID) patient.Record.HistoryOfIllnesses.Add(illness);

            }
        }
        public bool IsAllergic(Patient patient, List<Allergen> allergens)
        {
            foreach (Allergen i in patient.Record.Allergens)
            {
                foreach (Allergen allergen in allergens)
                {
                    if (i.ID == allergen.ID) return true;
                }
            }
            return false;
        }
    }
}
