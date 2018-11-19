using System;
using System.Collections.Generic;
using System.Text;
using ExamPreparation03.Vehicles;

namespace ExamPreparation03.Storages
{
    public class Warehouse : Storage
    {
        private const int WarehouseCapacity = 10;
        private const int WarehouseSlot = 10;
        private static Vehicle[] DefaultVehicle =
        {
            new Semi(),
            new Semi(),
            new Semi()
        };

        public Warehouse(string name) 
            : base(name,WarehouseCapacity,WarehouseSlot,DefaultVehicle)
        {
        }
    }
}
