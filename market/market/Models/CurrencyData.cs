using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace market.Models
{
    public class CurrencyData
    {
        [JsonProperty("Data")]
        public ObservableCollection<Currency> Currencies { get; set; }

        [JsonProperty("Timestamp")]
        public long Timestamp { get; set; }
    }
}
