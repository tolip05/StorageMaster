using System;
using System.Collections.Generic;
using System.Text;

namespace ExamPreparation03.Products
{
    public class SolidStateDrive : Product
    {
        private const double SolidStateDriveWeight = 0.2;
        public SolidStateDrive(double price) 
            : base(price, SolidStateDriveWeight)
        {
        }
    }
}
