using Newtonsoft.Json;

namespace GuildWars2API.Model.Recipes
{
    public class Ingredient
    {
        /// <summary>
        /// !!! In case of external request(http://gw2profits.com/json/): This could be negative. In that case its a currency.
        /// Possible values (value, explanation):
        /// -1 = Coin, -2 = Karma, -3 = Laurel, -4 = Gem
        /// -5 = Ascalonian Tear, -6 = Shard of Zhaitan, -7 = Fractal Relic
        /// -9 = Seal of Beetletun, -10 = Manifesto of the Moletariate
        /// -11 = Deadly Bloom, -12 = Symbol of Koda, -13 = Flame Legion Charr Carving
        /// -14 = Knowledge Crystal, -15 = Badge of Honor, -16 = Guild Commendation
        /// -18 = Transmutation Charge, -19 = Airship Part, -20 = Ley Line Crystal
        /// -22 = Lump of Aurillium, -23 = Spirit Shard, -24 = Pristine Fractal Relic
        /// -25 = Geode, -26 = WvW Tournament Claim Ticket, -27 = Bandit Crest
        /// -28 = Magnetite Shard, -29 = Provisioner Token, -30 = PvP League Ticket
        /// </summary>
        [JsonProperty("item_id")]
        public int ItemID { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}