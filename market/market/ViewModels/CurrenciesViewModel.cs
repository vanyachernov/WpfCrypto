using market.Models;
using market.Services;
using market.Utilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace market.ViewModels
{
    public class CurrenciesViewModel : ViewModelBase
    {
        private readonly CurrencyData _currencyData;
        private readonly CurrencyService _currencyService;

        public List<Currency> Currencies
        {
            get { return _currencyData.Currencies; }
            set { _currencyData.Currencies = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Load currencies data from service.
        /// </summary>
        /// <returns>Currencies data.</returns>
        public async Task LoadCurrenciesAsync()
        {
            try
            {
                Currencies = await _currencyService.GetAssetsDataAsync();
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"There is a problem! \n{e.Message}");
            }
        }

        public CurrenciesViewModel()
        {
            _currencyData = new CurrencyData
            {
                Currencies = new List<Currency>(),
                Timestamp = new long()
            };
            _currencyService = new CurrencyService();
        }
    }
}
