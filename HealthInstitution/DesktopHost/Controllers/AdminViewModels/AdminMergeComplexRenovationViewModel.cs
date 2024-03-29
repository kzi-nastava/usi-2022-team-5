﻿using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RenovationCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    class AdminMergeComplexRenovationViewModel : BaseViewModel
    {
        private readonly Admin _admin;
        private Institution _institution;

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private List<Room> _rooms;
        public List<Room> Rooms => _rooms;


        private Room _firstSelectedRoom;
        public Room FirstSelectedRoom
        {
            get => _firstSelectedRoom;
            set
            {
                _firstSelectedRoom = value;
                OnPropertyChanged(nameof(FirstSelectedRoom));
            }
        }

        private Room _secondSelectedRoom;
        public Room SecondSelectedRoom {
            get => _secondSelectedRoom;
            set 
            {
                _secondSelectedRoom = value;
                OnPropertyChanged(nameof(SecondSelectedRoom));
            } 
        }

        private List<string> _roomTypes;
        public List<string> RoomTypes => _roomTypes;

        private int _newRoomType;
        public int NewRoomType
        {
            get => _newRoomType;
            set
            {
                _newRoomType = value;
                OnPropertyChanged(nameof(NewRoomType));
            }
        }

        private string _newRoomName;
        public string NewRoomName
        {
            get => _newRoomName;
            set
            {
                _newRoomName = value;
                OnPropertyChanged(nameof(NewRoomName));
            }
        }

        private int _newRoomNumber;
        public int NewRoomNumber
        {
            get => _newRoomNumber;
            set
            {
                _newRoomNumber = value;
                OnPropertyChanged(nameof(NewRoomNumber));
            }
        }

        public ICommand Back { get; set; }
        public ICommand Merge { get; set; }

        public AdminMergeComplexRenovationViewModel()
        {
            _institution = Institution.Instance();
            _admin = (Admin)_institution.CurrentUser;
            _rooms = new List<Room>();
            _roomTypes = new List<string>();

            _startDate = DateTime.Today;
            _endDate = DateTime.Today;

            Back = new BackCommand();
            Merge = new MergeRenovationCommand(this);

            FillRooms();
            FillRoomTypes();
        }
        
        private void FillRoomTypes()
        {

            foreach (RoomType t in Enum.GetValues(typeof(RoomType)))
            {
                _roomTypes.Add(t.ToString());
            }
        }

        private void FillRooms()
        {
            IRoomRepositoryService rooms = new RoomRepositoryService();
            foreach (Room r in rooms.GetCurrentRooms())
            {
                if (r.ID == 0) continue;
                _rooms.Add(r);
            }
        }
    }
}
