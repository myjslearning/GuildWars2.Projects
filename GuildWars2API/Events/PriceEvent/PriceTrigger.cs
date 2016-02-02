using GuildWars2API.Model.Value;

namespace GuildWars2API.Event.PriceEvent
{
    internal class PriceTrigger
    {
        public int ItemID { get; internal set; }

        public ItemPrice Threshold { get; internal set; }

        public ListingType ListingType { get; internal set; }

        public PriceTriggerManager.IsTriggered Trigger { get; internal set; }
    }
}