﻿using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IEquipmentArrangementRepository : IRepository
    {
        public EquipmentArrangement FindCurrentArrangement(Room r, Equipment e);

        public EquipmentArrangement FindFirstBefore(Room r, Equipment e, DateTime date);

        public List<EquipmentArrangement> FindAllBefore(Room r, Equipment e, DateTime date);

        public EquipmentArrangement FindFirstAfter(Room r, Equipment e, DateTime date);

        public List<EquipmentArrangement> FindAllAfter(Room r, Equipment e, DateTime date);

        public List<EquipmentArrangement> GetArrangements();
        public List<EquipmentArrangement> GetCurrentArrangements();
    }
}