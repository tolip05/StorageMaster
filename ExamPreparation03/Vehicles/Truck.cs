using System;
using System.Collections.Generic;
using System.Text;

namespace ExamPreparation03.Vehicles
{
    public class Truck : Vehicle
    {
        private const int TruckCapacity = 5;
        public Truck() 
            : base(TruckCapacity)
        {
        }
    }
}
