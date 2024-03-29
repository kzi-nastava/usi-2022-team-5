﻿using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IPendingMedicineRepository : IRepository
    {
        public PendingMedicine FindByID(int id);

        public PendingMedicine AddNewMedicine(PendingMedicine newMedicine);

        public PendingMedicine ChangeMedicine(PendingMedicine medicine, string newName, List<Allergen> newIngredients);

        public List<PendingMedicine> GetPendingMedicines();
    }
}