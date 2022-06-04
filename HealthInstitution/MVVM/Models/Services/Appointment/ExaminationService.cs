﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.Models.Services
{
    class ExaminationService
    {
        private readonly ExaminationRepository _examinationRepository;

        public ExaminationService()
        {
            _examinationRepository = Institution.Instance().ExaminationRepository;
        }
        public void AddPrescription(Examination examination, Prescription prescription)
        {
            examination.Prescriptions.Add(prescription);
            //_examinationRepository.Update(examination);
        }

        public List<Examination> GetFutureExaminations(Specialization specialization, Patient patient)
        {
            List<Examination> futureAppointments = new();
            foreach (Examination appointment in _examinationRepository.Examinations)
            {
                if (DateTime.Compare(appointment.Date, DateTime.Now) > 0 &&
                    (appointment.Doctor.Specialization == specialization || appointment.Patient == patient))
                {
                    futureAppointments.Add(appointment);
                }
            }
            futureAppointments = futureAppointments.OrderBy(x => x.Date).ToList();
            return futureAppointments;
        }
    }
}
