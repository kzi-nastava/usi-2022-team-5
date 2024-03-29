﻿using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public class EquipmentRepositoryService : IEquipmentRepositoryService
    {
        private IEquipmentRepository _repository;

        public EquipmentRepositoryService()
        {
            _repository = Institution.Instance().EquipmentRepository;
        }

        public Equipment FindByID(int id)
        {
            return _repository.FindById(id);
        }

        public List<Equipment> GetEquipment()
        {
            return _repository.GetEquipment();
        }
    }
}