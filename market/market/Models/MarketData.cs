using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace market.Models
{
    public class MarketData
    {
        [JsonProperty("data")]
        public ObservableCollection<Market> Markets { get; set; }
    }
}
