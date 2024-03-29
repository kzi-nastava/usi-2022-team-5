﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.AppointmentCommands;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class AppointmentsViewModel : BaseViewModel
    {
        private IPatientRepositoryService _patientService;
        private IReferralRepositoryService _referralService;
        private readonly SecretaryReferralService _service;
        public SecretaryNavigationViewModel Navigation { get; }

        private readonly ObservableCollection<ReferralItemViewModel> _referrals;
        public IEnumerable<ReferralItemViewModel> Referrals => _referrals;

        private readonly ObservableCollection<Specialization> _specializations;
        public IEnumerable<Specialization> Specializations => _specializations;

        private readonly ObservableCollection<Patient> _patients;
        public IEnumerable<Patient> Patients => _patients;

        private ReferralItemViewModel _selectedReferral;
        public Patient SelectedPatient { get; set; }
        public Specialization SelectedSpecialization { get; set; }
        public string SelectedDuration { get; set; }

        private bool _enableChanges;
        private int _selection;

        public bool EnableChanges
        {
            get => _enableChanges;
            set
            {
                _enableChanges = value;
                OnPropertyChanged(nameof(EnableChanges));
            }
        }

        private bool _dialogOpen;
        public bool DialogOpen
        {
            get => _dialogOpen;
            set
            {
                _dialogOpen = value;
                OnPropertyChanged(nameof(DialogOpen));
            }
        }

        public DateTime NewAppointmentDate { get; set; }
        public DateTime NewAppointmentTime { get; set; }
        public int SelectedReferralId { get; set; }

        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; }
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                _selectedReferral = _referrals.ElementAt(_selection);

                SelectedReferralId = Convert.ToInt32(_selectedReferral.Id);
                OnPropertyChanged(nameof(SelectedReferralId));
            }
        }

        private string _searchPhrase;
        public string SearchPhrase
        {
            get => _searchPhrase;
            set
            {
                _searchPhrase = value;
                OnPropertyChanged(SearchPhrase);
            }
        }

        public ICommand Search { get; set; }
        public ICommand Reset { get; set; }
        public ICommand CreateReferralAppointment { get; set; }
        public ICommand CreateEmergencyAppointment { get; set; }

        public AppointmentsViewModel()
        {
            _patientService = new PatientRepositoryService();
            _referralService = new ReferralRepositoryService();
            _service = new SecretaryReferralService();
            EnableChanges = false;
            Navigation = new SecretaryNavigationViewModel();

            _referrals = new ObservableCollection<ReferralItemViewModel>();
            _specializations = new ObservableCollection<Specialization>();
            _patients = new ObservableCollection<Patient>();
            NewAppointmentDate = DateTime.Now;
            NewAppointmentTime = DateTime.Now;

            Search = new SearchCommand(this);
            Reset = new ResetCommand(this);
            CreateReferralAppointment = new CreateReferralAppointmentCommand(this);
            CreateEmergencyAppointment = new CreateEmergencyAppointmentCommand(this);

            _service.RemoveReferralsOfDeletedPatients();
            FillReferralsList();
            FillPatientsBox();
            FillSpecializations();
        }

        public void FillPatientsBox()
        {
            _patients.Clear();
            foreach (Patient patient in _patientService.GetPatients())
            {
                if(!patient.Deleted) _patients.Add(patient);
            }
            OnPropertyChanged(nameof(Patients));
        }

        public void FillSpecializations()
        {
            _specializations.Clear();
            foreach (Specialization specialization in Enum.GetValues(typeof(Specialization)))
            {
                _specializations.Add(specialization);
            }
        }

        public void FillReferralsList(string phrase = null)
        {
            _referrals.Clear();
            List<Referral> referrals = _referralService.GetReferrals();
            if (phrase != null) referrals = _service.SearchMatchingReferrals(phrase);
            foreach (Referral referral in referrals)
            {
                _referrals.Add(new ReferralItemViewModel(referral));
            }

            if (_referrals.Count != 0)
            {
                Selection = 0;
                OnPropertyChanged(nameof(Selection));
            }
        }
    }
}
