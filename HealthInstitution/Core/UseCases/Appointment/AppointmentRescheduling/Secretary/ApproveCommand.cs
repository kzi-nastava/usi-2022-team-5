﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class ApproveCommand : BaseCommand
    {
        private readonly Institution _institution;
        private AppointmentRequestsViewModel _viewModel;
        private readonly ExaminationChangeService _service;

        public ApproveCommand(AppointmentRequestsViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _service = new ExaminationChangeService();
        }

        public override void Execute(object parameter)
        {
            string message = _service.ApproveChange(_viewModel.SelectedRequestId);
            if (message != null) _viewModel.ShowMessage(message);
            _viewModel.FillRequestsList();
        }
    }
}
