using System.Windows;
using DairyProductInventoryApp.Models;
using DairyProductInventoryApp.Services;
using DairyProductInventoryApp.ViewModels;

namespace DairyProductInventoryApp.Views
{
    public partial class ProductWindow : Window
    {
        public ProductWindow(DairyProductManager manager, DairyProductDTO product = null, int? index = null)
        {
            InitializeComponent();
            DataContext = new ProductViewModel(product, manager, index);
        }
    }
}