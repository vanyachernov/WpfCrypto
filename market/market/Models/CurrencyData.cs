using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace market.Models
{
    /// <summary>
    /// Represents a currency data.
    /// </summary>
    public class CurrencyData
    {
        [JsonProperty("Data")]
        public ObservableCollection<Currency> Currencies { get; set; }

        [JsonProperty("Timestamp")]
        public long Timestamp { get; set; }
    }
}
