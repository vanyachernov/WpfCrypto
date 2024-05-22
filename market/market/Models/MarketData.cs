using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace market.Models
{
    /// <summary>
    /// Represents a market data.
    /// </summary>
    public class MarketData
    {
        [JsonProperty("data")]
        public ObservableCollection<Market> Markets { get; set; }
    }
}
