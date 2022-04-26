﻿using System.Windows;
using System;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.Stores;
using System.Collections.Generic;

namespace HealthInstitution
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly Institution _institution;
        private readonly NavigationStore _navigation;
        public App()
        {
            // * appSettings kao parametre prima putanje do fajlova gdje su smjesteni podaci za rad Zdravstvene Ustanove 
            AppSettings appSettings = AppSettings.Instance();
            appSettings.AddFilePaths("../../../Data/patients.json", "../../../Data/doctors.json", "../../../Data/secretaries.json",
                                     "../../../Data/admins.json", "../../../Data/appointments.json", "../../../Data/equipment.json",
                                     "../../../Data/operations.json", "../../../Data/rooms.json", "../../../Data/medicine.json",
                                     "../../../Data/daysOff.json");
            _institution = Institution.Instance();
            _institution.LoadAll();

            _navigation = NavigationStore.Instance();


            Patient p1 = new Patient();
            p1.Email = "p";
            p1.Password = "p";
            p1.Record = new MedicalRecord();
            Institution.Instance().GetPatients().Add(p1);

            Secretary s1 = new Secretary();
            s1.Email = "s";
            s1.Password = "s";
            Institution.Instance().GetSecretaries().Add(s1);

            Admin a1 = new Admin();
            a1.Email = "a";
            a1.Password = "a";
            Institution.Instance().GetAdmins().Add(a1);

            Doctor d1 = new Doctor();
            d1.Email = "d";
            d1.Password = "d";

            // test
            System.Diagnostics.Debug.WriteLine("Nesto......");
            List<Examination> appointments = new();
            DateTime date = DateTime.Today;
            date = date.AddHours(12);
            date = date.AddMinutes(30);
            System.Diagnostics.Debug.WriteLine(date);
            System.Diagnostics.Debug.WriteLine(".....");
            Examination appointment = new Examination(1, date, false, false, "", null);  // 12:30 - 12:45 
            appointments.Add(appointment);
            date = date.AddMinutes(40);
            appointment = new Examination(2, date, false, false, "", null);   // 13:10 - 13:25
            appointments.Add(appointment);

            date = date.AddMinutes(45);
            Operation operation = new Operation(3, date, false, false, 60);  // 14:00 - 15:00
            List<Operation> operations = new List<Operation>();
            operations.Add(operation);
            
            d1.SetOperations(operations);
            d1.SetExaminations(appointments);
            DateTime datet = DateTime.Today;
            List<DateTime> available = d1.FindFreeTime(datet, 30);
            foreach(DateTime i in available)
            {
                System.Diagnostics.Debug.WriteLine(i);
            }
            // test
            Institution.Instance().GetDoctors().Add(d1);

        }

        protected override void OnStartup(StartupEventArgs e)
        {

            _navigation.CurrentViewModel = new LoginViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigation)
            };
            //MainWindow.Show();

            System.Diagnostics.Debug.WriteLine("Nesto");
            base.OnStartup(e);
        }
    }
}
