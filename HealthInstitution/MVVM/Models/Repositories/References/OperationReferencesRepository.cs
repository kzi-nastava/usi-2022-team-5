﻿using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class OperationReferencesRepository
    {
        private string _fileName;
        private List<OperationReference> _references;

        public OperationReferencesRepository(string patientFileName)
        {
            _fileName = patientFileName;
            _references = new List<OperationReference>();
        }
        public List<OperationReference> GetReferences()
        {
            return _references;
        }

        public void LoadFromFile()
        {
            _references = FileService.Deserialize<OperationReference>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<OperationReference>(_fileName, _references);
        }

        public OperationReference FindByOperationID(int id)
        {
            foreach (OperationReference reference in _references)
            {
                if (reference.GetOperationId() == id) return reference;
            }
            return null;
        }
    }
}
