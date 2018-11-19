using ExamPreparation03.Factories;
using ExamPreparation03.Products;
using ExamPreparation03.Storages;
using ExamPreparation03.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamPreparation03.Core
{
   public class Manager
    {
        private Dictionary<string, Stack<Product>> products;
        private Dictionary<string, Storage> storages;
        private ProductFactory ProductFactory;
        private StorageFactory storageFactory;
        private Vehicle currentVehicle;

        public Manager()
        {
            this.products = new Dictionary<string, Stack<Product>>();
            this.storages = new Dictionary<string, Storage>();
            this.ProductFactory = new ProductFactory();
            this.storageFactory = new StorageFactory();
        }

        public string AddProduct(string type, double price)
        {
            Product product = this.ProductFactory.CreateProduct(type,price);
            if (!this.products.ContainsKey(type))
            {
                this.products.Add(type, new Stack<Product>());
            }
            this.products[type].Push(product);
            return $"Added {type} to pool";
        }

        public string RegisterStorage(string type, string name)
        {
            Storage storage = this.storageFactory.CreateStorage(type,name);
            this.storages.Add(name,storage);
            return $"Registered {storage.GetType().Name}";
        }

        public string SelectVehicle(string storageName, int garageSlot)
        {
            Storage storage = this.storages[storageName];
            this.currentVehicle = storage.GetVehicle(garageSlot);
            return $"Selected {this.currentVehicle.GetType().Name}";
        }

        public string LoadVehicle(IEnumerable<string> productNames)
        {
            int loadedProductsCount = 0;
            foreach (var productName in productNames)
            {
                if (this.currentVehicle.IsFull)
                {
                    break;
                }
                if (!this.products.ContainsKey(productName)
                    || this.products[productName].Count == 0)
                {
                    throw new InvalidOperationException($"{productName} is out of stock!");
                }
                Product product = this.products[productName].Pop();
                this.currentVehicle.LoadProduct(product);
                loadedProductsCount++;
            }
            return $"Loaded {loadedProductsCount}/{productNames.Count()} products into {this.currentVehicle.GetType().Name}";
        }

        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {
            if (!this.storages.ContainsKey(sourceName))
            {
                throw new InvalidOperationException("Invalid source storage!");
            }
            if (!this.storages.ContainsKey(destinationName))
            {
                throw new InvalidOperationException("Invalid destination storage!");
            }
            Storage soursNameStorage = this.storages[sourceName];
            Storage destinationStorgae = this.storages[destinationName];
            Vehicle vehicle = soursNameStorage.GetVehicle(sourceGarageSlot);
            int destinationGarageSlot = soursNameStorage.SendVehicleTo(sourceGarageSlot,destinationStorgae);
            return $"Sent {vehicle.GetType().Name} to {destinationName} (slot {destinationGarageSlot})";

        }

        public string UnloadVehicle(string storageName, int garageSlot)
        {
            Storage storage = this.storages[storageName];
            int productsInVehicle = storage.GetVehicle(garageSlot).Trunk.Count();
           int unloadedProductsCount = storage.UnloadVehicle(garageSlot);
            return $"Unloaded {unloadedProductsCount}/{productsInVehicle} products at {storageName}";
        }

        public string GetStorageStatus(string storageName)
        {
            Storage storage = this.storages[storageName];
            Dictionary<string, int> productsAndCount = new Dictionary<string, int>();
            foreach (var product in storage.Products)
            {
                string productTypeName = product.GetType().Name;
                if (!productsAndCount.ContainsKey(productTypeName))
                {
                    productsAndCount.Add(productTypeName,1);
                }
                else
                {
                    productsAndCount[productTypeName]++;
                }
            }
            var productsSum = storage.Products.Sum(p => p.Weight);
            int storageCapacity = storage.Capacity;
            Dictionary<string, int> sortedProducts = productsAndCount
                .OrderByDescending(x => x.Key)
                .ThenBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Value);
            string[] productsAsString = new string[sortedProducts.Count()];
            int index = 0;
            foreach (var product in sortedProducts)
            {
                string currentResult = $"{product.Key} ({product.Value})";
                productsAsString[index++] = currentResult;
            }
            //     productsAndCount.OrderByDescending(x => x.Value)
            //        .ThenBy(x => x.Key)
            //        .Select(kvp => $"{kvp.Key} ({kvp.Value})")
            //        .ToArray();
            string stockLine = $"Stock({productsSum}/{storageCapacity}): [{string.Join(", ",productsAsString)}]";
            //  string[] storageStringRepresentive =
            //       storage.Garage
            //      .Select(g => g == null ? "empty" : g.GetType().Name)
            //      .ToArray();
            string[] storageStringRepresentive = new string[storage.GarageSlots];
            int indexVehicle = 0;
            foreach (var vehicle in storage.Garage)
            {
                if (vehicle == null)
                {
                    storageStringRepresentive[indexVehicle++] = "empty";
                }
                else
                {
                    storageStringRepresentive[indexVehicle++] = vehicle.GetType().Name;
                }
            }
            string lineResult = $"Garage: [{string.Join("|",storageStringRepresentive)}]";
            string result = 
                stockLine + Environment.NewLine
                + lineResult;
            return result;
        }
        
        public string GetSumarry()
        {
            Storage[] sortedStorages =
                this.storages
                .Select(s => s.Value)
                .OrderByDescending(s => s.Products.Sum(p => p.Price))
                .ToArray();
            StringBuilder sb = new StringBuilder();
            foreach (var storage in sortedStorages)
            {
                double totalMoney = storage.Products.Sum(p => p.Price);
                sb.AppendLine($"{storage.Name}:");
                sb.AppendLine($"Storage worth: ${totalMoney:F2}");
            }
            string result = sb.ToString().TrimEnd();
            return result;
        }
    }
}
