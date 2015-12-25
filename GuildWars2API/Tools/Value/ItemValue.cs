using GuildWars2API.Model.Items;
using GuildWars2API.Model.Market;

namespace GuildWars2API.Tools.Value
{
    public class ItemValue
    {
        public bool IsBound { get; set; }

        public ItemStack ItemStack { get; set; }

        public ItemListing ItemListing { get; set; }

        public ItemPrice VendorPrice { get; set; }

        public ItemPrice BuyValue {
            get
            {
                if(IsBound) {
                    return VendorPrice;
                }
                return new ItemPrice(ItemListing.Buys.UnitPrice * ItemStack.Count);
            }
        }  

        public ItemPrice SellValue
        {
            get
            {
                if(IsBound) {
                    return VendorPrice;
                }
                return new ItemPrice(ItemListing.Sells.UnitPrice * ItemStack.Count);
            }
        }
    }
}