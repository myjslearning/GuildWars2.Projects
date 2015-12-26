using Newtonsoft.Json;

namespace GuildWars2API.Model.Commerce
{
    public class Transaction
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("item_id")]
        public int ItemID { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("purchased")]
        public string Purchased { get; set; }
    }
}