using System;
using System.Collections.Generic;
using System.Text;

namespace ExamPreparation03.Vehicles
{
    public class Van : Vehicle
    {
        private const int VanCapacity = 2;
        public Van() 
            : base(VanCapacity)
        {
        }
    }
}
