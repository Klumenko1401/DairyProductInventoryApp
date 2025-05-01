using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DairyProductInventoryApp.Models;
using Newtonsoft.Json;

namespace DairyProductInventoryApp.Services
{
    public class DairyProductManager
    {
        private List<DairyProduct> products;
        private readonly string filePath = "products.json";

        public DairyProductManager()
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var loadedProducts = JsonConvert.DeserializeObject<List<DairyProduct>>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
                products = loadedProducts ?? new List<DairyProduct>();
            }
            else
            {
                // Ініціалізація початкового списку, якщо файл не існує
                products = new List<DairyProduct>
                {
                    new Milk("Молоко 2.5%", "Простоквашино", "Картон", 2.5, 1.0, DateTime.Now.AddDays(7), 30.0m),
                    new Cheese("Гауда", "Яготинське", "Вакуум", 45.0, 0.3, DateTime.Now.AddDays(30), 80.0m),
                    new Yogurt("Йогурт натуральний", "Агуша", "Пластик", 3.2, 0.2, DateTime.Now.AddDays(10), 15.0m)
                };
                SaveProducts();
            }
        }

        private void SaveProducts()
        {
            string json = JsonConvert.SerializeObject(products, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            File.WriteAllText(filePath, json);
        }

        public List<DairyProductDTO> GetAllProducts()
        {
            return products.Select(p => new DairyProductDTO
            {
                Name = p.Name,
                Brand = p.Brand,
                Packaging = p.Packaging,
                FatContent = p.FatContent,
                WeightOrVolume = p.WeightOrVolume,
                ExpiryDate = p.ExpiryDate,
                Price = p.Price,
                Type = p.GetType().Name,
                Description = p.GetDescription()
            }).ToList();
        }

        public List<DairyProductDTO> Search(string type, string name, string brand)
        {
            return products.Where(p =>
                (string.IsNullOrEmpty(type) || p.GetType().Name.Equals(type, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(name) || p.Name.ToLower().Contains(name.ToLower())) &&
                (string.IsNullOrEmpty(brand) || p.Brand.ToLower().Contains(brand.ToLower()))
            ).Select(p => new DairyProductDTO
            {
                Name = p.Name,
                Brand = p.Brand,
                Packaging = p.Packaging,
                FatContent = p.FatContent,
                WeightOrVolume = p.WeightOrVolume,
                ExpiryDate = p.ExpiryDate,
                Price = p.Price,
                Type = p.GetType().Name,
                Description = p.GetDescription()
            }).ToList();
        }

        public void AddProduct(DairyProduct product)
        {
            products.Add(product);
            SaveProducts();
        }

        public void UpdateProduct(int index, DairyProduct product)
        {
            products[index] = product;
            SaveProducts();
        }

        public void DeleteProduct(int index)
        {
            products.RemoveAt(index);
            SaveProducts();
        }
    }
}