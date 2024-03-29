﻿using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Repositories;
using System;
using System.Collections.Generic;
using HealthInstitution.Core.Reposirory;
using HealthInstitution.Core.Services;
using HealthInstitution.Infrastructure.Database.Repositories;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core
{
    // class through which all system entities can be accessed
    // implemented using Singleton pattern
    public sealed class Institution
    {
        private readonly AppConfiguration _appSettings;

        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly SecretaryRepository _secretaryRepository;
        private readonly AdminRepository _adminRepository;

        private readonly IPrescriptionRepository _prescriptionRepository;

        private readonly IExaminationRepository _examinationRepository;
        private readonly IExaminationRelationsRepository _examinationReferencesRepository;
        private readonly IExaminationChangeRepository _examinationChangeRepository;
        private readonly IOperationRepository _operationRepository;
        private readonly IOperationRelationsRepository _operationReferencesRepository;

        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentArrangementRepository _equipmentArragmentRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IRenovationRepository _renovationRepository;
        private readonly IRoomRenovationRepository _roomRenovationRepository;
        private readonly IEquipmentOrderRepository _equipmentOrderRepository;

        private readonly IMedicineRepository _medicineRepository;
        private readonly IDayOffRepository _dayOffRepository;
        private readonly IReferralRepository _referralRepository;

        private readonly IAllergenRepository _allergenRepository;
        private readonly IPatientAllergenRepository _patientAllergenRepository;
        private readonly IMedicineAllergenRepository _medicineAllergenRepository;
        private readonly IPendingMedicineRepository _pendingMedicineRepository;

        private readonly IDoctorDaysOffRepository _doctorDaysOffRepository;
        private readonly IPrescriptionMedicineRepository _prescriptionMedicineRepository;

        private readonly INotificationRepository _notificationRepository;
        private readonly IReviewRepository _reviewRepository;


        private User _currentUser;
        public User CurrentUser { get => _currentUser; set { _currentUser = value; } }

        private static Institution s_instance = null;

        public static Institution Instance()
        {
            if (s_instance is null)
            {
                s_instance = new Institution();
                ConnectReferences();
            }
            return s_instance;
        }

        private Institution()
        {
            _appSettings = AppConfiguration.Instance();
            _adminRepository = new AdminRepository(_appSettings.AdminsFileName);
            _secretaryRepository = new SecretaryRepository(_appSettings.SecretariesFileName);
            _patientRepository = new PatientRepository(_appSettings.PatientsFileName);
            _doctorRepository = new DoctorRepository(_appSettings.DoctorsFileName);

            _prescriptionRepository = new PrescriptionRepository(_appSettings.PerscriptionsFileName);
            _examinationRepository = new ExaminationRepository(_appSettings.ExaminationsFileName);
            _operationRepository = new OperationRepository(_appSettings.OperationsFileName);
            _examinationReferencesRepository = new ExaminationReferencesRepository(_appSettings.ExaminationReferencesFileName);
            _operationReferencesRepository = new OperationReferencesRepository(_appSettings.OperationsReferencesFileName);

            _roomRepository = new RoomRepository(_appSettings.RoomsFileName, _appSettings.FutureRoomsFileName, _appSettings.DeletedRoomsFileName);
            _equipmentRepository = new EquipmentRepository(_appSettings.EquipmentFileName);
            _equipmentArragmentRepository = new EquipmentArrangementRepository(_appSettings.EquipmentArrangementFileName);
            _renovationRepository = new RenovationRepository(_appSettings.RenovationFileName);
            _roomRenovationRepository = new RoomRenovationRepository(_appSettings.RoomRenovationFileName);
            _equipmentOrderRepository = new EquipmentOrderRepository(_appSettings.EquipmentOrderFileName);

            _dayOffRepository = new DayOffRepository(_appSettings.DaysOffFileName);
            _referralRepository = new ReferralRepository(_appSettings.RefferalsFileName);

            _medicineRepository = new MedicineRepository(_appSettings.MedicinesFileName);
            _allergenRepository = new AllergenRepository(_appSettings.AllergensFileName);
            _patientAllergenRepository = new PatientAllergenRepository(_appSettings.PatientAllergensFileName);
            _medicineAllergenRepository = new MedicineAllergenRepository(_appSettings.MedicineAllergensFileName);
            _pendingMedicineRepository = new PendingMedicineRepository(_appSettings.PendingMedicinesFileName);

            _doctorDaysOffRepository = new DoctorDaysOffRepository(_appSettings.DoctorDaysOffFileName);
            _prescriptionMedicineRepository = new PrescriptionMedicineRepository(_appSettings.PrescriptionMedicineFileName);
            _examinationChangeRepository = new ExaminationChangeRepository(_appSettings.ExaminationChangeFileName);

            _notificationRepository = new NotificationRepository(_appSettings.PatientNotificationsFileName);
            _reviewRepository = new ReviewRepository(_appSettings.ReviewsFileName);

            LoadAll();
        }

        public void LoadAll()
        {
            _adminRepository.LoadFromFile();
            _patientRepository.LoadFromFile();
            _doctorRepository.LoadFromFile();
            _secretaryRepository.LoadFromFile();
            _examinationRepository.LoadFromFile();
            _operationRepository.LoadFromFile();
            _examinationReferencesRepository.LoadFromFile();
            _operationReferencesRepository.LoadFromFile();
            _dayOffRepository.LoadFromFile();
            _roomRepository.LoadFromFile();
            _equipmentRepository.LoadFromFile();
            _equipmentArragmentRepository.LoadFromFile();
            _renovationRepository.LoadFromFile();
            _roomRenovationRepository.LoadFromFile();
            _medicineRepository.LoadFromFile();
            _referralRepository.LoadFromFile();
            _allergenRepository.LoadFromFile();
            _patientAllergenRepository.LoadFromFile();
            _medicineAllergenRepository.LoadFromFile();
            _pendingMedicineRepository.LoadFromFile();
            _doctorDaysOffRepository.LoadFromFile();
            _prescriptionMedicineRepository.LoadFromFile();
            _prescriptionRepository.LoadFromFile();
            _examinationChangeRepository.LoadFromFile();
            _equipmentOrderRepository.LoadFromFile();
            _notificationRepository.LoadFromFile();
            _reviewRepository.LoadFromFile();
        }

        public void SaveAll()
        {
            _adminRepository.SaveToFile();
            _patientRepository.SaveToFile();
            _doctorRepository.SaveToFile();
            _secretaryRepository.SaveToFile();
            _examinationRepository.SaveToFile();
            _operationRepository.SaveToFile();
            _examinationReferencesRepository.SaveToFile();
            _operationReferencesRepository.SaveToFile();
            _dayOffRepository.SaveToFile();
            _roomRepository.SaveToFile();
            _equipmentRepository.SaveToFile();
            _equipmentArragmentRepository.SaveToFile();
            _renovationRepository.SaveToFile();
            _roomRenovationRepository.SaveToFile();
            _referralRepository.SaveToFile();
            _medicineRepository.SaveToFile();
            _allergenRepository.SaveToFile();
            _patientAllergenRepository.SaveToFile();
            _medicineAllergenRepository.SaveToFile();
            _pendingMedicineRepository.SaveToFile();
            _doctorDaysOffRepository.SaveToFile();
            _prescriptionMedicineRepository.SaveToFile();
            _prescriptionRepository.SaveToFile();
            _examinationChangeRepository.SaveToFile();
            _equipmentOrderRepository.SaveToFile();
            _notificationRepository.SaveToFile();
            _reviewRepository.SaveToFile();
        }

        private static void ConnectReferences()
        {
            ReferencesService.ConnectExaminationReferences();
            ReferencesService.ConnectOperationReferences();
            ReferencesService.FillMedicalRecord();
            ReferencesService.ConnectMedicineAllergens();
            ReferencesService.ConnectDoctorDaysOff();
            ReferencesService.ConnectPrescriptionRepository();
            ReferencesService.ConnectExaminationChanges();
            ReferencesService.ArrangeEquipment();
            ReferencesService.ConnectRenovations();
            ReferencesService.ConnectPendingMedicineAllergens();
            ReferencesService.ConnectPatientNotifications();
        }


        public IPatientRepository PatientRepository { get => _patientRepository; }
        public IDoctorRepository DoctorRepository { get => _doctorRepository; }
        public SecretaryRepository SecretaryRepository { get => _secretaryRepository; }
        public AdminRepository AdminRepository { get => _adminRepository; }
        public IPrescriptionRepository PrescriptionRepository { get => _prescriptionRepository; }
        
        public IExaminationRepository ExaminationRepository { get => _examinationRepository; }
        public IExaminationChangeRepository ExaminationChangeRepository { get => _examinationChangeRepository; }
        public IExaminationRelationsRepository ExaminationReferencesRepository { get => _examinationReferencesRepository; }
        public IOperationRepository OperationRepository { get => _operationRepository; }
        public IOperationRelationsRepository OperationReferencesRepository { get => _operationReferencesRepository; }
        
        public IEquipmentRepository EquipmentRepository { get => _equipmentRepository; }
        public IRoomRepository RoomRepository { get => _roomRepository; }
        public IRenovationRepository RenovationRepository { get => _renovationRepository; }
        public IRoomRenovationRepository RoomRenovationRepository { get => _roomRenovationRepository; }
        public IMedicineRepository MedicineRepository { get => _medicineRepository; }
        public IDayOffRepository DayOffRepository { get => _dayOffRepository; }
        public IReferralRepository ReferralRepository { get => _referralRepository; }
        public IAllergenRepository AllergenRepository { get => _allergenRepository; }
        public IPatientAllergenRepository PatientAllergenRepository { get => _patientAllergenRepository; }
        public IMedicineAllergenRepository MedicineAllergenRepository { get => _medicineAllergenRepository; }
        public IPendingMedicineRepository PendingMedicineRepository { get => _pendingMedicineRepository; }
        public IDoctorDaysOffRepository DoctorDaysOffRepository { get => _doctorDaysOffRepository; }
        public IPrescriptionMedicineRepository PrescriptionMedicineRepository { get => _prescriptionMedicineRepository; }
        public IEquipmentArrangementRepository EquipmentArragmentRepository { get => _equipmentArragmentRepository; }
        public IEquipmentOrderRepository EquipmentOrderRepository { get => _equipmentOrderRepository; }
        public INotificationRepository NotificationRepository { get => _notificationRepository; }
        public IReviewRepository ReviewRepository { get => _reviewRepository; }
    }
}
