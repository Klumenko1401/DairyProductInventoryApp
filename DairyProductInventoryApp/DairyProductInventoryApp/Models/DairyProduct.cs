using System;

namespace DairyProductInventoryApp.Models
{
    public abstract class DairyProduct
    {
        private string name;
        private string brand;
        private string packaging;
        private double fatContent;
        private double weightOrVolume;
        private DateTime expiryDate;
        private decimal price;

        protected DairyProduct(string name, string brand, string packaging, double fatContent,
            double weightOrVolume, DateTime expiryDate, decimal price)
        {
            this.name = name;
            this.brand = brand;
            this.packaging = packaging;
            this.fatContent = fatContent;
            this.weightOrVolume = weightOrVolume;
            this.expiryDate = expiryDate;
            this.price = price;
        }

        public string Name { get => name; set => name = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Packaging { get => packaging; set => packaging = value; }
        public double FatContent { get => fatContent; set => fatContent = value; }
        public double WeightOrVolume { get => weightOrVolume; set => weightOrVolume = value; }
        public DateTime ExpiryDate { get => expiryDate; set => expiryDate = value; }
        public decimal Price { get => price; set => price = value; }

        public virtual string GetDescription()
        {
            return $"Назва: {Name}, Бренд: {Brand}, Упаковка: {Packaging}, Жирність: {FatContent}%, " +
                   $"Вага/Об'єм: {WeightOrVolume}, Термін придатності: {ExpiryDate.ToShortDateString()}, Ціна: {Price} грн";
        }
    }
}