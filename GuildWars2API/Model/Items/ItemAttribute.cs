using Newtonsoft.Json;

namespace GuildWars2API.Model.Items
{
    public class ItemAttribute
    {
        /// <summary>
        /// Possible values (value, explanation):
        /// ConditionDamage – Condition Damage, CritDamage – Ferocity
        /// Healing – Healing Power, Power – Power, Precision – Precision
        /// Toughness – Toughness, Vitality – Vitality
        /// </summary>
        [JsonProperty("attribute")]
        public string Attribute { get; set; }

        [JsonProperty("modifier")]
        public int Modifier { get; set; }
    }
}