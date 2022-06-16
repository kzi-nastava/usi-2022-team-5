﻿using System;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Services
{
    class DoctorRescheduleAppointmentService
    {
        private ExaminationRepository _examinationRepository;
        private IRoomRepositoryService _roomRepository;
        private ExaminationReferencesRepository _examinationReferencesRepository;
        private ExaminationChangeRepository _examinationChangeRepository;
        private OperationReferencesRepository _operationReferencesRepository;


        public DoctorRescheduleAppointmentService()
        {
            _examinationRepository = Institution.Instance().ExaminationRepository;
            _roomRepository = new RoomRepositoryService();
            _examinationReferencesRepository = Institution.Instance().ExaminationReferencesRepository;
            _examinationChangeRepository = Institution.Instance().ExaminationChangeRepository;
            _operationReferencesRepository = Institution.Instance().OperationReferencesRepository;
        }

        public bool RescheduleExamination(Appointment appointment, DateTime dateTime, bool validation = true)
        {
            new ValidationService().ValidateAppointmentData(appointment, dateTime, validation);

            FindAvailableRoomService service = new FindAvailableRoomService();
            service.FindAvailableRoom(appointment, dateTime);
            bool resolved = true;
            if (resolved)
            {
                appointment.Date = dateTime;
            }

            if (appointment is Examination)
            {

                _examinationReferencesRepository.Remove((Examination)appointment);
                _examinationReferencesRepository.Add((Examination)appointment);
                _examinationChangeRepository.Add((Examination)appointment, dateTime, resolved, AppointmentStatus.EDITED);

            }

            else if (appointment is Operation)
            {
                _operationReferencesRepository.Remove((Operation)appointment);
                _operationReferencesRepository.Add((Operation)appointment);
            }

            return resolved;

        }
    }
}
