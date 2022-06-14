﻿using HealthInstitution.Core;
using HealthInstitution.Core.Services.PatientServices;
using HealthInstitution.MVVM.ViewModels.Commands.PatientCommands;
using HealthInstitution.MVVM.Views.PatientViews;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    public class PatientRecordViewModel : BaseViewModel
    {
        private readonly Institution _institution;
        protected Patient _patient;
        public PatientNavigationViewModel Navigation { get; }
        public Patient Patient => _patient;


        private ObservableCollection<AppointmentListItemViewModel> _appointments;
        public IEnumerable<AppointmentListItemViewModel> Appointments
        {
            get { return _appointments; }
            set { _appointments = new ObservableCollection<AppointmentListItemViewModel>(value); }
        }

        private string _searchKeyWord;
        public string SearchKeyWord
        {
            get { return _searchKeyWord; }
            set { _searchKeyWord = value; OnPropertyChanged(nameof(SearchKeyWord)); }
        }
        public ICommand Search { get; set; }
        public ICommand Reset { get; set; }

        private int _service;
        private int _suggestion;
        private string _comment;

        public int Service
        {
            get { return _service; }
            set { _service = value; OnPropertyChanged(nameof(Service)); }
        }
        public int Suggestion
        {
            get { return _suggestion; }
            set { _suggestion = value; OnPropertyChanged(nameof(Suggestion)); }
        }
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; OnPropertyChanged(nameof(Comment)); }
        }

        private AppointmentListItemViewModel _selectedAppointment;

        public bool CanReview { get; set; }

        public AppointmentListItemViewModel SelectedAppointment
        {
            get { return _selectedAppointment; }
            set { _selectedAppointment = value; 
                if ((Appointment) _selectedAppointment is Examination examination && examination.Review != null)
                CanReview = true;
                OnPropertyChanged(nameof(SelectedAppointment)); 
                OnPropertyChanged(nameof(CanReview)); }
        }


        public PatientRecordViewModel()
        {
            Navigation = new PatientNavigationViewModel();

            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            _appointments = new ObservableCollection<AppointmentListItemViewModel>();
            PatientAppointmentsService service = new PatientAppointmentsService(_patient);
            FillAppointmentsList(service.GetPastAppointments());
            InitializeSearchParameters();
            CanReview = false;
        }

        private void InitializeSearchParameters()
        {
            _searchKeyWord = "";
            Search = new SearchCommand(this);
            Reset = new ResetCommand(this);
        }

        public void FillAppointmentsList(List<Appointment> appointments)
        {
            _appointments.Clear();
            foreach (Appointment appointment in appointments)
            {
                _appointments.Add(new AppointmentListItemViewModel(appointment));
            }
            OnPropertyChanged(nameof(Appointments));
        }

    }
}
