using market.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace market
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task<List<Currency>> GetExchangeDataAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var endPoint = "https://api.coincap.io/v2/assets";
                    var response = await client.GetAsync(endPoint);

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
}
