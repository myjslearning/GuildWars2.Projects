using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Achievements
{
    class Group
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("categories")]
        public List<int> Categories { get; set; }
    }
}
