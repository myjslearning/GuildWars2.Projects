using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API.Model.Guild
{
    class Rank
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        /// <summary>
        /// An array of permission ids - See Permission
        /// </summary>
        [JsonProperty("permissions")]
        public List<string> Permissions { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
