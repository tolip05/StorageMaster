using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamPreparation03.Core
{
   public class Engine
    {
        private Manager manager;
        private bool isRunning;

        public Engine()
        {
            this.manager = new Manager();
            this.isRunning = false;
        }

        public void Run()
        {
            this.isRunning = true;

            while (isRunning)
            {
                string line = Console.ReadLine();
                string[] tokens = line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string command = tokens[0];
                string output = "";
                try
                {
                    
                    switch (command)
                    {
                        case "AddProduct":
                            string type = tokens[1];
                            double price = double.Parse(tokens[2]);
                            output = this.manager.AddProduct(type, price);
                            break;
                        case "RegisterStorage":
                            output = this.manager.RegisterStorage(tokens[1], tokens[2]);
                            break;
                        case "SelectVehicle":
                            output = this.manager.SelectVehicle(tokens[1], int.Parse(tokens[2]));
                            break;
                        case "LoadVehicle":
                            output = this.manager.LoadVehicle(tokens.Skip(1));
                            break;
                        case "SendVehicleTo":
                            output = this.manager.SendVehicleTo(tokens[1], int.Parse(tokens[2]), tokens[3]);
                            break;
                        case "UnloadVehicle":
                            output = this.manager.UnloadVehicle(tokens[1], int.Parse(tokens[2]));
                            break;
                        case "GetStorageStatus":
                            output = this.manager.GetStorageStatus(tokens[1]);
                            break;
                        case "END":
                            output = manager.GetSumarry();
                            isRunning = false;
                            break;
                        default:
                            break;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    output = $"Error: {ex.Message}";
                    
                }

                Console.WriteLine(output);
            }
        }
    }
}
