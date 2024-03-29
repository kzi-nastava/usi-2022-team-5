﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class BlockedPatientListViewModel : BaseViewModel
    {
        private IPatientRepositoryService _patientService;
        public SecretaryNavigationViewModel Navigation { get; }

        private readonly ObservableCollection<BlockedPatientItemViewModel> _patients;
        public IEnumerable<BlockedPatientItemViewModel> Patients => _patients;
        private BlockedPatientItemViewModel _selectedPatient;

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

        public ICommand Unblock { get; set; }

        public int SelectedPatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }

        private readonly ObservableCollection<AllergenItemViewModel> _allergens;
        public IEnumerable<AllergenItemViewModel> Allergens => _allergens;


        private readonly ObservableCollection<IllnessItemViewModel> _illnesses;
        public IEnumerable<IllnessItemViewModel> Illnesses => _illnesses;

        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; };
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                _selectedPatient = _patients.ElementAt(_selection);
                SelectedPatientId = Convert.ToInt32(_selectedPatient.Id);
                OnPropertyChanged(nameof(SelectedPatientId));
                FirstName = _selectedPatient.Name;
                OnPropertyChanged(nameof(FirstName));
                LastName = _selectedPatient.Surname;
                OnPropertyChanged(nameof(LastName));
                Email = _patientService.FindByID(Convert.ToInt32(_selectedPatient.Id)).Email;
                OnPropertyChanged(nameof(Email));
                Height = _patientService.FindByID(Convert.ToInt32(_selectedPatient.Id)).Record.Height.ToString();
                OnPropertyChanged(nameof(Height));
                Weight = _patientService.FindByID(Convert.ToInt32(_selectedPatient.Id)).Record.Weight.ToString();
                OnPropertyChanged(nameof(Weight));

                FillAllergenList();
                FillIllnessList();
            }
        }

        public BlockedPatientListViewModel()
        {
            _patientService = new PatientRepositoryService();
            _patients = new ObservableCollection<BlockedPatientItemViewModel>();
            _allergens = new ObservableCollection<AllergenItemViewModel>();
            _illnesses = new ObservableCollection<IllnessItemViewModel>();
            Navigation = new SecretaryNavigationViewModel();
            EnableChanges = false;
            Unblock = new UnblockCommand(this);
            FillPatientList();
            FillAllergenList();
            FillIllnessList();
        }

        public void FillPatientList()
        {
            _patients.Clear();

            List<Patient> blockedPatients = _patientService.GetPatients();
            foreach (Patient patient in blockedPatients)
            {
                if (patient.Blocked && !patient.Deleted)
                {
                    _patients.Add(new BlockedPatientItemViewModel(patient));
                }
            }
            if (_patients.Count != 0)
            {
                Selection = 0;
                OnPropertyChanged(nameof(Selection));
            }
        }

        public void FillAllergenList()
        {
            _allergens.Clear();
            int id = 1;
            if (_selectedPatient != null) id = Convert.ToInt32(_selectedPatient.Id);
            Patient patient = _patientService.FindByID(id);
            foreach (Allergen allergen in patient.Record.Allergens)
            {
                _allergens.Add(new AllergenItemViewModel(allergen));
            }
        }

        public void FillIllnessList()
        {
            _illnesses.Clear();
            int id = 1;
            if (_selectedPatient != null) id = Convert.ToInt32(_selectedPatient.Id);
            List<string> allIllnesses = _patientService.FindByID(id).GetHistoryOfIllness();
            foreach (string illness in allIllnesses)
            {
                _illnesses.Add(new IllnessItemViewModel(illness));
            }
        }


    }
}
