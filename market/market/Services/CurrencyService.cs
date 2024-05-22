using market.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace market.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;

        public CurrencyService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Get a list of currencies by popularity.
        /// </summary>
        /// <returns>List of currencies.</returns>
        public async Task<ObservableCollection<Currency>> GetAssetsDataAsync()
        {
            try
            {
                var endPoint = "https://api.coincap.io/v2/assets?limit=10";
                var response = await _httpClient.GetAsync(endPoint);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var assetData = JsonConvert.DeserializeObject<CurrencyData>(json);

                    foreach (var currency in assetData.Currencies)
                    {
                        if (decimal.TryParse(currency.PriceUsd, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
                        {
                            price = Math.Round(price, 2);
                            currency.PriceUsd = $"{price.ToString(CultureInfo.InvariantCulture)} $";

                        }
                        if (decimal.TryParse(currency.VolumeUsd24Hr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal volume))
                        {
                            volume = Math.Round(volume, 2);
                            currency.VolumeUsd24Hr = volume.ToString(CultureInfo.InvariantCulture);
                        }
                        if (decimal.TryParse(currency.ChangePercent24Hr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal percent))
                        {
                            percent = Math.Round(percent, 2);
                            currency.ChangePercent24Hr = percent.ToString(CultureInfo.InvariantCulture);
                        }

                        // Get markets data for each coin.
                        currency.Markets = await GetMarketDataAsync(currency.Id);
                    }

                    return assetData.Currencies;
                }
                else
                {
                    MessageBox.Show("Error fetching data from API");
                    return new ObservableCollection<Currency>();
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"There is a problem! \n{e.Message}");
                return new ObservableCollection<Currency>();
            }
        }

        /// <summary>
        /// Get market data for a specific coin.
        /// </summary>
        /// <param name="coinId">The ID of the coin.</param>
        /// <returns>List of markets.</returns>
        public async Task<ObservableCollection<Market>> GetMarketDataAsync(string coinId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.coincap.io/v2/assets/{coinId}/markets?limit=5");
                request.Headers.Add("Accept", "application/json");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var marketData = JsonConvert.DeserializeObject<MarketData>(json);

                    foreach (var market in marketData.Markets)
                    {
                        if (decimal.TryParse(market.PriceUsd, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
                        {
                            price = Math.Round(price, 2);
                            market.PriceUsd = $"{price.ToString(CultureInfo.InvariantCulture)} $";
                        }
                        if (decimal.TryParse(market.VolumePercent, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal percent))
                        {
                            percent = Math.Round(percent, 2);
                            market.VolumePercent = percent.ToString(CultureInfo.InvariantCulture);
                        }
                    }

                    return marketData.Markets;
                }
                else
                {
                    MessageBox.Show("Error fetching market data from API");
                    return new ObservableCollection<Market>();
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"There is a problem! \n{e.Message}");
                return new ObservableCollection<Market>();
            }
        }
    }
}
