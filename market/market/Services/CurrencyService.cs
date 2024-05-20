using market.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
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
        public async Task<List<Currency>> GetAssetsDataAsync()
        {
            try
            {
                var endPoint = "https://api.coincap.io/v2/assets?limit=10";
                var response = await _httpClient.GetAsync(endPoint);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var assetData = JsonConvert.DeserializeObject<CurrencyData>(json);
                    return assetData.Currencies;
                }
                else
                {
                    MessageBox.Show("Error fetching data from API");
                    return new List<Currency>();
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"There is a problem! \n{e.Message}");
                return new List<Currency>();
            }
        }
    }
}
