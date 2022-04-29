﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Examination : Appointment
    {
        private string _anamnesis;
        private Perscription _perscription;
        private AppointmentReview _review;

        public Examination(int id, DateTime date, bool isEmergency, bool done,
                            string anamnesis, AppointmentReview review)
                           : base(id, date, isEmergency, done)
        {
            _anamnesis = anamnesis;
            _perscription = null;
            _review = review;
        }

        public string GetAnamnesis() => _anamnesis;
        public void SetAnamnesis(string anamnesis) => _anamnesis = anamnesis;
        public Perscription GetPerscription() => _perscription;
        public void SetPerscription(Perscription perscription) => _perscription = perscription;
        public AppointmentReview GetReview() => _review;
        public void SetReview(AppointmentReview review) => _review = review;

        public void Update(Examination examination)
        {
            SetDateTime(examination.GetDateTime());
            SetEmergency(examination.GetEmergency());
            SetDone(examination.GetDone());
            SetAnamnesis(examination.GetAnamnesis());
            SetPerscription(examination.GetPerscription());
            SetRoom(examination.GetRoom());
        }

        public void Delete(Examination examination)
        {

        }
    }
}