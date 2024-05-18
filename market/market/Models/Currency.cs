using Newtonsoft.Json;

namespace market.Models
{
    public class Currency
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Rank")]
        public string Rank { get; set; }

        [JsonProperty("Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Supply")]
        public string Supply { get; set; }

        [JsonProperty("MaxSupply")]
        public string MaxSupply { get; set; }

        [JsonProperty("MarketCapUsd")]
        public string MarketCapUsd { get; set; }

        [JsonProperty("VolumeUsd24Hr")]
        public string VolumeUsd24Hr { get; set; }

        [JsonProperty("PriceUsd")]
        public string PriceUsd { get; set; }

        [JsonProperty("ChangePercent24Hr")]
        public string ChangePercent24Hr { get; set; }

        [JsonProperty("Vwap24Hr")]
        public string Vwap24Hr { get; set; }
    }
}
