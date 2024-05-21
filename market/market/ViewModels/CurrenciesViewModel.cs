using market.Models;
using market.Services;
using market.Utilities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace market.ViewModels
{
    public class CurrenciesViewModel : ViewModelBase
    {
        private readonly CurrencyData _currencyData;
        private readonly CurrencyService _currencyService;

        private ObservableCollection<Currency> _filteredCurrencies;
        private string _searchQuery;

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
                FilterCurrencies();
            }
        }

        /// <summary>
        /// Filtered coins storage after searching.
        /// </summary>
        public ObservableCollection<Currency> FilteredCurrencies
        {
            get { return _filteredCurrencies; }
            set
            {
                _filteredCurrencies = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Search query field.
        /// </summary>
        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
                FilterCurrencies();
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
                Currencies = await _currencyService.GetAssetsDataAsync();
                FilterCurrencies();
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"There is a problem! \n{e.Message}");
            }
        }

        private void FilterCurrencies()
        {
            if (_currencyData?.Currencies == null)
                return;

            var filteredCurrencies = string.IsNullOrWhiteSpace(_searchQuery)
                ? new ObservableCollection<Currency>(_currencyData.Currencies)
                : new ObservableCollection<Currency>(_currencyData.Currencies.Where(c => c.Id.ToLower().Contains(_searchQuery.ToLower()) || c.Name.ToLower().Contains(_searchQuery.ToLower())));

            FilteredCurrencies = filteredCurrencies;
        }

        /// <summary>
        /// Initialize a CurrenciesViewModel instance.
        /// </summary>
        public CurrenciesViewModel()
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
