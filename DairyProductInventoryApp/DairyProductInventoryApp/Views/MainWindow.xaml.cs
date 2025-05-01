using System.Windows;
using System.Windows.Controls;
using DairyProductInventoryApp.ViewModels;

namespace DairyProductInventoryApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is DataGrid grid && grid.SelectedItem != null)
            {
                var viewModel = (MainViewModel)DataContext;
                viewModel.ShowDetailsCommand.Execute(null);
            }
        }
    }
}