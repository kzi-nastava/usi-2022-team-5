﻿using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class UnblockCommand : BaseCommand
    {
        private readonly Institution _institution;
        private BlockedPatientListViewModel _viewModel;
        private readonly PatientManagementService _service;

        public UnblockCommand(BlockedPatientListViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _service = new PatientManagementService();
        }

        public override void Execute(object parameter)
        {
            _service.UnblockPatient(_viewModel.SelectedPatientId);
            _viewModel.ShowMessage("The patient has ben successfully unblocked.");
            _viewModel.FillPatientList();
        }
    }
}
