﻿using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.Services;
using System.Collections.Generic;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    internal class SearchCommand : BaseCommand
    {
        private readonly BaseViewModel _viewModel;
        private IAnamnesisSearch _anamnesisSearch;
        private IDoctorSearch _doctorSearch;

        public SearchCommand(BaseViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (_viewModel is PatientRecordViewModel recordViewModel)
            {
                _anamnesisSearch = new AnamnesisSearchService(recordViewModel.Patient);
                List<Appointment> appointments = _anamnesisSearch.SearchByAnamnesis(recordViewModel.SearchKeyWord);
                recordViewModel.FillAppointmentsList(appointments);
            }
            if (_viewModel is PatientSearchViewModel searchViewModel)
            {

                _doctorSearch = new DoctorsSearchService();
                Doctor search = new Doctor(searchViewModel.FirstNameKeyWord, searchViewModel.LastNameKeyWord, (Specialization)searchViewModel.SelectedSpecialization);
                List<Doctor> doctors = _doctorSearch.SearchForDoctor(search);
                searchViewModel.FillAllDoctorsList(doctors);
            }
        }
    }
}
