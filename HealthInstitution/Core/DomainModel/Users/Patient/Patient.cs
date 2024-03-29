﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace HealthInstitution.Core
{
    public class Patient : User
    {
        private bool _blocked;
        private BlockadeType _blockadeType;
        private bool _deleted;
        private MedicalRecord _record;
        private List<Examination> _examinations;
        private List<Operation> _operations;
        private List<ExaminationChange> _examinationChanges;
        private List<Notification> _notifications;
        private int _notificationsPreference;

        [JsonIgnore]
        public List<Notification> Notifications
        {
            get
            {
                if (_notifications == null)
                {
                    _notifications = new List<Notification>();
                }

                return _notifications;
            }
            set => _notifications = value;
        }
        [JsonProperty("Blocked")]
        public bool Blocked { get => _blocked; set { _blocked = value; } }
        [JsonProperty("BlockadeType")]
        public BlockadeType BlockadeType { get => _blockadeType; set { _blockadeType = value; } }
        [JsonProperty("Deleted")]
        public bool Deleted { get => _deleted; set { _deleted = value; } }
        [JsonProperty("Record")]
        public MedicalRecord Record { get => _record; set { _record = value; } }
        [JsonProperty("NotificationsPreference")]
        public int NotificationsPreference { get => _notificationsPreference; set { _notificationsPreference = value; } }
        [JsonIgnore]
        public List<Examination> Examinations
        {
            get
            {
                if (_examinations == null)
                {
                    _examinations = new List<Examination>();
                }

                return _examinations;
            }
            set => _examinations = value;
        }
        [JsonIgnore]
        public List<Operation> Operations
        {
            get
            {
                if (_operations == null)
                {
                    _operations = new List<Operation>();
                }

                return _operations;
            }
            set => _operations = value;
        }
        [JsonIgnore]
        public List<ExaminationChange> ExaminationChanges
        {
            get
            {
                if (_examinationChanges == null)
                {
                    _examinationChanges = new List<ExaminationChange>();
                }

                return _examinationChanges;
            }
            set => _examinationChanges = value;
        }
        public Patient()
        {
        }

        public Patient(int id, string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight)
            : base(id, firstName, lastName, email, password, gender)
        {
            _blocked = false;
            _blockadeType = 0;
            _deleted = false;
            _record = new MedicalRecord(height, weight, new List<Allergen>(), new List<string>());
        }

        public void Delete()
        {
            _deleted = true;
        }

        public void Unblock()
        {
            _blocked = false;
            _blockadeType = BlockadeType.NONE;
        }

        public void Block(BlockadeType blockadeType=BlockadeType.SECRETARY)
        {
            _blocked = true;
            _blockadeType = blockadeType;
        }
        public void Update(int id, string name, string lastName, string email, string password, Gender gender, double height, double weight)
        {
            ID = id;
            FirstName = name;
            LastName = lastName;
            Email = email;
            Password = password;
            Gender = gender;
            Record.Height = height;
            Record.Weight = weight;
        }


        public List<string> GetHistoryOfIllness()
        {
            List<string> historyOfIllness = new List<string>();
            if (_examinations == null) return new List<string>();
            foreach (string illness in Record.HistoryOfIllnesses)
            {
                historyOfIllness.Add(illness);
            }
            return historyOfIllness;
        }


    }
}