using GuildWars2API.Model.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace GuildWars2API.Model.Value.PriceEvent
{
    public class PriceTriggerManager
    {
        public event EventHandler<PriceTriggerEventArgs> OnThresholdReached;
        public delegate bool IsTriggered(ItemPrice threshold, ItemPrice currentPrice);

        private Timer _timer;
        private List<PriceTrigger> _triggers;

        public void AddTrigger(int itemID, IsTriggered trigger, ItemPrice threshold, ListingType type) {
            if(_timer == null) {
                _timer = new Timer(1000 * (60 * 3));  //Interval is 3 minute.
                _timer.Elapsed += TimerElapsed;
                _timer.Start();
            }

            if(_triggers == null)
                _triggers = new List<PriceTrigger>();

            _triggers.Add(new PriceTrigger() { ItemID = itemID, Trigger = trigger, Threshold = threshold, ListingType = type });
        }

        public void RemoveTrigger(int itemID) {
            if(_triggers == null)
                return;

            PriceTrigger triggerToDelete = _triggers.Find(t => t.ItemID == itemID);
            if(triggerToDelete != null)
                _triggers.Remove(triggerToDelete);
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e) {
            if(_triggers == null)
                return;

            HashSet<int> itemIDs = new HashSet<int>(_triggers.Select(t => t.ItemID).ToList());
            List<ItemListing> itemListings = ItemAPI.GetItemListing(itemIDs);

            foreach(PriceTrigger trigger in _triggers) {
                ItemListing listing = itemListings.Find(l => l.ID == trigger.ItemID);
                if(listing == null)
                    continue;

                int itemCount;
                ItemPrice currentPrice;

                if(trigger.ListingType == ListingType.BuyListing) {
                    itemCount = listing.Buys.Quantity;
                    currentPrice = new ItemPrice(listing.Buys.UnitPrice);
                }
                else {
                    itemCount = listing.Sells.Quantity;
                    currentPrice = new ItemPrice(listing.Sells.UnitPrice);
                }

                if(trigger.Trigger(trigger.Threshold, currentPrice))
                    ThresholdReached(trigger.ItemID, itemCount, currentPrice, trigger.Threshold);
            }
        }

        private void ThresholdReached(int itemID, int itemCount, ItemPrice currentPrice, ItemPrice threshold) {
            if(OnThresholdReached != null) 
                OnThresholdReached(this, new PriceTriggerEventArgs() { ItemID = itemID, ItemCount = itemCount, CurrentPrice = currentPrice, Threshold = threshold });
        }

        #region Defined Triggers
        public bool IsBelow(ItemPrice threshold, ItemPrice currentPrice) => threshold < currentPrice;
        public bool IsAbove(ItemPrice threshold, ItemPrice currentPrice) => threshold > currentPrice;
        #endregion Defined Triggers
    }

    public enum ListingType
    {
        SellListing,
        BuyListing
    }
}