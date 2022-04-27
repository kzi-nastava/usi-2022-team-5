﻿using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models
{
    public class ExaminationRepository
    {
        private string _fileName;
        private List<Examination> _examinations;

        public ExaminationRepository(string filePath)
        {
            _fileName = filePath;
            _examinations = new List<Examination>();
        }

        public List<Examination> GetExaminations()
        {
            return _examinations;
        }

        public void LoadFromFile()
        {
            _examinations = FileService.Deserialize<Examination>(_fileName);

        }

        public void SaveToFile()
        {
            FileService.Serialize<Examination>(_fileName, _examinations);
        }
        public Examination FindByID(int id)
        {
            foreach (Examination examination in _examinations)
            {
                if (examination.GetId() == id) return examination;
            }
            return null;
        }
    }
}