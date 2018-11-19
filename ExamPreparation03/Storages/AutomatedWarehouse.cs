using System;
using System.Collections.Generic;
using System.Text;
using ExamPreparation03.Vehicles;

namespace ExamPreparation03.Storages
{
    public class AutomatedWarehouse : Storage
    {
        private const int AutomatedWarehouseCapacity = 1;
        private const int AutomatedWarehouseSlots = 2;

        private static Vehicle[] DefaultVehicle =
        {
            new Truck()
        };
        public AutomatedWarehouse(string name) 
            : base(name, AutomatedWarehouseCapacity, AutomatedWarehouseSlots, DefaultVehicle)
        {
        }
    }
}
