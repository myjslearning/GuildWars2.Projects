using GuildWars2API.Model.Commerce;
using System.Collections.Generic;

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
                Equipment.ForEach(i => { price.Add(i.BuyValue); });
                Inventory.ForEach(i => { price.Add(i.BuyValue); });
                _totalBuyValue = price;
                return price;
            }
        }

        public ItemPrice TotalSellValue {
            get {
                if(_totalSellValue != null)
                    return _totalSellValue;

                ItemPrice price = new ItemPrice();
                Equipment.ForEach(i => { price.Add(i.SellValue); });
                Inventory.ForEach(i => { price.Add(i.SellValue); });
                _totalSellValue = price;
                return price;
            }
        }

        public string Name { get; set; }

        public List<ItemValue> Equipment { get; set; }

        public List<ItemValue> Inventory { get; set; }
    }
}