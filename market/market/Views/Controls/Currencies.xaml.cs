using market.Models;
using market.ViewModels;
using market.Views.Windows;
using System.Windows;
using System.Windows.Controls;

namespace market.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для Currencies.xaml
    /// </summary>
    public partial class Currencies : UserControl
    {
        private readonly CurrenciesViewModel _currenciesModel;

        public Currencies()
        {
            InitializeComponent();
            _currenciesModel = new CurrenciesViewModel();
            DataContext = _currenciesModel;
        }

        private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await _currenciesModel.LoadCurrenciesAsync();
        }

        private void DataCurrencies_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataCurrencies.SelectedItem is Currency selectedCurrency)
            {
                var selectedCoinDetails = new CoinDetails(selectedCurrency);

                selectedCoinDetails.ShowDialog();
            }
        }
    }
}
