﻿using System;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;
using HealthInstitution.Stores;
using HealthInstitution.Core.Repository;
using System.Windows;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.AppointmentCommands
{
    public class ScheduleCommand : BaseCommand
    {
        private IDoctorRepositoryService _doctorService;
        private readonly Institution _institution;
        private EmergencyAppointmentViewModel _viewModel;
        private EmergencyAppointmentService _service;

        private readonly NavigationStore _navigationStore;

        public ScheduleCommand(EmergencyAppointmentViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _navigationStore = NavigationStore.Instance();
            _service = new EmergencyAppointmentService();
            _doctorService = new DoctorRepositoryService();
        }

        public override void Execute(object parameter)
        {
            if (_viewModel.SelectedAppointment is null)
            {
                MessageBox.Show("No compatible appointments to reschedule. Recommendation: create a non-emergency appointment !");
                _navigationStore.CurrentViewModel = new AppointmentsViewModel();
                return;
            }

            // reschedule selected appoinment
            Appointment appointmentToPostpone = _viewModel.SelectedAppointment.Appointment;
            DateTime oldDate = appointmentToPostpone.Date;
            DateTime newDate = _viewModel.AppointmentsNewDate[appointmentToPostpone];
            appointmentToPostpone.Date = newDate;

            // create new emergency appointment
            Specialization specialization = _viewModel.SelectedSpecialization;
            Patient patient = _viewModel.SelectedPatient;
            int duration = _viewModel.SelectedDuration;
            Doctor doctor = appointmentToPostpone.Doctor.Specialization == specialization ?
                appointmentToPostpone.Doctor : _doctorService.FindDoctorBySpecialization(specialization);

            Examination appointment = new(doctor, patient, oldDate);
            new SecretaryScheduleAppointmentService().ScheduleAppointment(appointment, duration, false);

            MessageBox.Show("Emergency appointment has been successfully created !");

            _service.SendNotifications(appointmentToPostpone, appointment);
            _navigationStore.CurrentViewModel = new AppointmentsViewModel();
        }
    }
}