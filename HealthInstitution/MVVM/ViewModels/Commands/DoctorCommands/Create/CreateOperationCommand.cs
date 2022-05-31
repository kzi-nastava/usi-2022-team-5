﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class CreateOperationCommand : BaseCommand
    {
        private DoctorOperationViewModel _viewModel;

        public CreateOperationCommand(DoctorOperationViewModel doctorOperationViewModel)
        {
            _viewModel = doctorOperationViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            Doctor doctor = _viewModel.Doctor;
            Patient patient = _viewModel.NewPatient;
            DateTime datetime = _viewModel.MergeTime(_viewModel.NewDate, _viewModel.NewTime);

            try
            {
                bool isCreated = Institution.Instance().CreateAppointment(doctor, patient, datetime, nameof(Operation), _viewModel.Duration);
                
                if (isCreated)
                {
                    _viewModel.ShowMessage("Examination successfully scheduled !");
                }
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }

            _viewModel.FillOperationsList();
        }
    }
}