using Newtonsoft.Json;
using System;

namespace GuildWars2API.Model.Guild
{
    class Member
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }

        /// <summary>
        /// ISO-8601 format
        /// </summary>
        [JsonProperty("joined")]
        public DateTime Joined { get; set; } 
    }
}
