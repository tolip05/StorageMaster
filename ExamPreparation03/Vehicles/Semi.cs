using System;
using System.Collections.Generic;
using System.Text;

namespace ExamPreparation03.Vehicles
{
    public class Semi : Vehicle
    {
        private const int SemiCapacity = 10;
        public Semi() 
            : base(SemiCapacity)
        {
        }
    }
}
