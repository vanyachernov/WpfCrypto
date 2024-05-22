using market.Models;
using market.Services;
using market.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace market.ViewModels
{
    public class ConverterViewModel : ViewModelBase
    {
        private readonly CurrencyService _currencyService;
        private readonly CurrencyData _currencyData;

        private decimal _amount;
        private Currency _selectedFromCurrency;
        private Currency _selectedToCurrency;
        private decimal _convertedAmount;

        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
                ConvertCurrency();
            }
        }

        public Currency SelectedFromCurrency
        {
            get => _selectedFromCurrency;
            set
            {
                _selectedFromCurrency = value;
                OnPropertyChanged();
                ConvertCurrency();
            }
        }

        public Currency SelectedToCurrency
        {
            get => _selectedToCurrency;
            set
            {
                _selectedToCurrency = value;
                OnPropertyChanged();
                ConvertCurrency();
            }
        }

        public decimal ConvertedAmount
        {
            get => _convertedAmount;
            set
            {
                _convertedAmount = value;
                OnPropertyChanged();
            }
        }

        private bool IsValidData()
        {
            if (SelectedFromCurrency == null || SelectedToCurrency == null || Amount <= 0)
            {
                return false;
            }
            return true;
        }

        private void ConvertCurrency()
        {
            if(!IsValidData())
            {
                ConvertedAmount = 0;
                return;
            }
            try
            {
                var fromCurrencyPrice = SelectedFromCurrency.PriceUsd.Replace(" $", "");
                var toCurrencyPrice = SelectedToCurrency.PriceUsd.Replace(" $", "");
                var fromPriceDecimal = Convert.ToDecimal(fromCurrencyPrice, CultureInfo.InvariantCulture);
                var toPriceDecimal = Convert.ToDecimal(toCurrencyPrice, CultureInfo.InvariantCulture);

                ConvertedAmount = Math.Round((Amount * fromPriceDecimal) / toPriceDecimal, 2);
            }
            catch (FormatException)
            {
                MessageBox.Show("Error converting currencies. Please check the input values.");
                ConvertedAmount = 0;
            }
            
        }

        /// <summary>
        /// Full data coins storage.
        /// </summary>
        public ObservableCollection<Currency> Currencies
        {
            get { return _currencyData.Currencies; }
            set
            {
                _currencyData.Currencies = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Load currencies data from service.
        /// </summary>
        /// <returns>Currencies data.</returns>
        public async Task LoadCurrenciesAsync()
        {
            try
            {
                _currencyData.Currencies = await _currencyService.GetAssetsDataAsync();
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"There is a problem! \n{e.Message}");
            }
        }

        public ConverterViewModel()
        {
            _currencyData = new CurrencyData
            {
                Currencies = new ObservableCollection<Currency>(),
                Timestamp = new long()
            };
            _currencyService = new CurrencyService();
        }
    }
}
