using System;
using System.Collections.Generic;
using System.Text;
using ExamPreparation03.Vehicles;

namespace ExamPreparation03.Storages
{
    public class DistributionCenter : Storage
    {
        private const int DistributionCenterCapacity = 2;
        private const int DistributionCenterSlot = 5;
        private static Vehicle[] DefaultVehicle =
        {
            new Van(),
            new Van(),
            new Van()
        };
        public DistributionCenter(string name) 
            : base(name,DistributionCenterCapacity,DistributionCenterSlot,DefaultVehicle)
        {
        }
    }
}
