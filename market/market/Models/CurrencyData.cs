using Newtonsoft.Json;
using System.Collections.Generic;

namespace market.Models
{
    public class CurrencyData
    {
        [JsonProperty("Data")]
        public List<Currency> Currencies { get; set; }

        [JsonProperty("Timestamp")]
        public long Timestamp { get; set; }
    }
}
