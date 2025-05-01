using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DairyProductInventoryApp.Models;
using DairyProductInventoryApp.Services;

namespace DairyProductInventoryApp.ViewModels
{
    public class ProductViewModel : NotifyPropertyChangedBase
    {
        private readonly DairyProductManager manager;
        private readonly int? index;
        private string name;
        private string brand;
        private string packaging;
        private double fatContent;
        private double weightOrVolume;
        private DateTime expiryDate;
        private decimal price;
        private string selectedType;

        public ProductViewModel(DairyProductDTO product, DairyProductManager manager, int? index = null)
        {
            this.manager = manager;
            this.index = index;
            SaveCommand = new RelayCommand(SaveProduct);
            Types = new[] { "Milk", "Cheese", "Yogurt" };

            if (product != null)
            {
                Name = product.Name;
                Brand = product.Brand;
                Packaging = product.Packaging;
                FatContent = product.FatContent;
                WeightOrVolume = product.WeightOrVolume;
                ExpiryDate = product.ExpiryDate;
                Price = product.Price;
                SelectedType = product.Type;
            }
            else
            {
                ExpiryDate = DateTime.Now;
            }
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Brand
        {
            get => brand;
            set => SetProperty(ref brand, value);
        }

        public string Packaging
        {
            get => packaging;
            set => SetProperty(ref packaging, value);
        }

        public double FatContent
        {
            get => fatContent;
            set => SetProperty(ref fatContent, value);
        }

        public double WeightOrVolume
        {
            get => weightOrVolume;
            set => SetProperty(ref weightOrVolume, value);
        }

        public DateTime ExpiryDate
        {
            get => expiryDate;
            set => SetProperty(ref expiryDate, value);
        }

        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public string SelectedType
        {
            get => selectedType;
            set => SetProperty(ref selectedType, value);
        }

        public string[] Types { get; }

        public ICommand SaveCommand { get; }

        private void SaveProduct()
        {
            try
            {
                DairyProduct product = null;
                switch (SelectedType)
                {
                    case "Milk":
                        product = new Milk(Name, Brand, Packaging, FatContent, WeightOrVolume, ExpiryDate, Price);
                        break;
                    case "Cheese":
                        product = new Cheese(Name, Brand, Packaging, FatContent, WeightOrVolume, ExpiryDate, Price);
                        break;
                    case "Yogurt":
                        product = new Yogurt(Name, Brand, Packaging, FatContent, WeightOrVolume, ExpiryDate, Price);
                        break;
                    default:
                        throw new Exception("Виберіть тип продукту");
                }

                if (index.HasValue)
                    manager.UpdateProduct(index.Value, product);
                else
                    manager.AddProduct(product);

                if (Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this) is Window window)
                {
                    window.DialogResult = true;
                    window.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}