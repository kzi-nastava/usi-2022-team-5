﻿using HealthInstitution.MVVM.ViewModels.Commands;
using HealthInstitution.MVVM.ViewModels.Commands.AdminCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    public class AdminNavigationViewModel
    {
        public ICommand Room { get; }
        public ICommand Equipment { get; }

        public ICommand LogOut { get; }

        public AdminNavigationViewModel()
        {
            Room = new AdminRoomCommand();
            Equipment = new AdminEquipmentCommand();

            LogOut = new LogOutCommand();
        }
    }
}