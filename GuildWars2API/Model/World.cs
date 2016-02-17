using Newtonsoft.Json;

namespace GuildWars2API.Model
{
    public class World
    {
        /// <summary>
        /// The first digit of the id indicates the world's region. 1 is North America, 2 is Europe
        /// The second digit of the id currently correlates with the world's assigned language: 1 means French, 2 means German, and 3 means Spanish
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Possible values:
        /// Low, Medium, High, VeryHigh, Full
        /// </summary>
        [JsonProperty("population")]
        public string Population { get; set; }
    }
}
