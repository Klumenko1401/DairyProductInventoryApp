using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DairyProductInventoryApp.Models;
using DairyProductInventoryApp.Services;
using DairyProductInventoryApp.Views;

namespace DairyProductInventoryApp.ViewModels
{
    public class MainViewModel : NotifyPropertyChangedBase
    {
        private readonly DairyProductManager manager;
        private ObservableCollection<DairyProductDTO> products;
        private DairyProductDTO selectedProduct;
        private string searchType;
        private string searchName;
        private string searchBrand;

        public MainViewModel()
        {
            manager = new DairyProductManager();
            Products = new ObservableCollection<DairyProductDTO>(manager.GetAllProducts());
            AddCommand = new RelayCommand(AddProduct);
            EditCommand = new RelayCommand(EditProduct, () => SelectedProduct != null);
            DeleteCommand = new RelayCommand(DeleteProduct, () => SelectedProduct != null);
            SearchCommand = new RelayCommand(Search);
            ShowDetailsCommand = new RelayCommand(ShowDetails, () => SelectedProduct != null);
        }

        public ObservableCollection<DairyProductDTO> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        public DairyProductDTO SelectedProduct
        {
            get => selectedProduct;
            set => SetProperty(ref selectedProduct, value);
        }

        public string SearchType
        {
            get => searchType;
            set => SetProperty(ref searchType, value);
        }

        public string SearchName
        {
            get => searchName;
            set => SetProperty(ref searchName, value);
        }

        public string SearchBrand
        {
            get => searchBrand;
            set => SetProperty(ref searchBrand, value);
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ShowDetailsCommand { get; }

        private void AddProduct()
        {
            var window = new ProductWindow(manager);
            if (window.ShowDialog() == true)
            {
                Products = new ObservableCollection<DairyProductDTO>(manager.GetAllProducts());
            }
        }

        private void EditProduct()
        {
            int index = Products.IndexOf(SelectedProduct);
            var window = new ProductWindow(manager, SelectedProduct, index);
            if (window.ShowDialog() == true)
            {
                Products = new ObservableCollection<DairyProductDTO>(manager.GetAllProducts());
            }
        }

        private void DeleteProduct()
        {
            int index = Products.IndexOf(SelectedProduct);
            manager.DeleteProduct(index);
            Products = new ObservableCollection<DairyProductDTO>(manager.GetAllProducts());
        }

        private void Search()
        {
            Products = new ObservableCollection<DairyProductDTO>(
                manager.Search(SearchType, SearchName, SearchBrand));
        }

        private void ShowDetails()
        {
            MessageBox.Show(SelectedProduct.Description, "Деталі продукту");
        }
    }
}