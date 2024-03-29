﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    class DayOffRepositoryService : IDayOffRepositoryService
    {
        private readonly IDayOffRepository _dayOffRepository;

        public DayOffRepositoryService()
        {
            _dayOffRepository = Institution.Instance().DayOffRepository;
        }

        public DayOff FindByID(int id)
        {
            return _dayOffRepository.FindByID(id);
        }

        public int FindNewID()
        {
            return _dayOffRepository.FindNewID();
        }

        public List<DayOff> DoctorDaysOffToDaysOff(List<DoctorDaysOff> doctorDaysOff)
        {
            return _dayOffRepository.DoctorDaysOffToDaysOff(doctorDaysOff);
        }

        public List<DayOff> GetDaysOff()
        {
            return _dayOffRepository.GetDaysOff();
        }
    }
}
