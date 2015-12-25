using GuildWars2API.Model.Market;
using System.Collections.Generic;
using System.Linq;

namespace GuildWars2API.Tools.Value
{
    public class CharacterValue
    {
        private ItemPrice _totalBuyValue;
        private ItemPrice _totalSellValue;

        public ItemPrice TotalBuyValue {
            get {
                if(_totalBuyValue != null)
                    return _totalBuyValue;

                ItemPrice price = new ItemPrice();
                Equipment.Where(i => i.ItemListing != null)
                    .ToList()
                    .ForEach(i => {
                        price.Add(i.ItemListing.Buys.UnitPrice * i.ItemStack.Count);
                    });
                Inventory
                    .Where(i => i.ItemListing != null)
                    .ToList().ForEach(i => {
                        price.Add(i.ItemListing.Buys.UnitPrice * i.ItemStack.Count);
                    });
                _totalBuyValue = price;
                return price;
            }
        }

        public ItemPrice TotalSellValue {
            get {
                if(_totalSellValue != null)
                    return _totalSellValue;

                ItemPrice price = new ItemPrice();
                Equipment.Where(i => i.ItemListing != null)
                    .ToList()
                    .ForEach(i => {
                        price.Add(i.ItemListing.Sells.UnitPrice * i.ItemStack.Count);
                    });
                Inventory.Where(i => i.ItemListing != null)
                    .ToList().ForEach(i => {
                        price.Add(i.ItemListing.Sells.UnitPrice * i.ItemStack.Count);
                    });
                _totalSellValue = price;
                return price;
            }
        }

        public string Name { get; set; }

        public List<ItemValue> Equipment { get; set; }

        public List<ItemValue> Inventory { get; set; }
    }
}