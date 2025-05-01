using System;

namespace DairyProductInventoryApp.Models
{
    public class DairyProductDTO
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Packaging { get; set; }
        public double FatContent { get; set; }
        public double WeightOrVolume { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}