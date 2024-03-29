﻿using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Core.Services.Equipments;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.EquipmentCommands
{
    class FilterCommand : BaseCommand
    {
        private AdminEquipmentViewModel _model;
        private IFilterEquipmentService _filter;

        public FilterCommand(AdminEquipmentViewModel model)
        {
            _model = model;
            _filter = new FilterEquipmentService();
        }

        private bool CheckPrerequisites()
        {
            bool prerequisitesFulfilled = true;
            int minQuantity = 0, maxQuantity = 0;
            if (!(int.TryParse(_model.FilterMinQuantity, out minQuantity) && int.TryParse(_model.FilterMaxQuantity, out maxQuantity)))
            {
                MessageBox.Show("Minimum and maximum quantity must be whole numbers!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            return prerequisitesFulfilled;
        }

        public override void Execute(object parameter)
        {
            if (CheckPrerequisites())
            {
        
                int minQuantity = int.Parse(_model.FilterMinQuantity), maxQuantity = int.Parse(_model.FilterMaxQuantity);
                try
                {
                    _model.FilteredEquipment = _filter.Filter((RoomType)_model.FilterRoomType, minQuantity,
                        maxQuantity, (EquipmentType)_model.FilterEquipmentType);
                    
                    _model.DialogOpen = false;
                    _model.FilterEquipmentList();
                } catch (EquipmentFilterQuantityException e)
                {
                    _model.ShowMessage(e.Message);
                } catch (Exception e)
                {
                    _model.ShowMessage(e.Message);
                }

            }
        }
    }
}
