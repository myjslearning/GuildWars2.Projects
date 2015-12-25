using GuildWars2API.Model.Account;
using GuildWars2API.Model.Market;
using System.Collections.Generic;
using System.Linq;

namespace GuildWars2API.Tools.Value
{
    public class AccountValue
    {
        private ItemPrice _totalBuyValue;
        private ItemPrice _totalSellValue;
        private List<CharacterValue> _characters;

        public ItemPrice TotalBuyValue {
            get {
                if(_totalBuyValue != null)
                    return _totalBuyValue;

                ItemPrice price = new ItemPrice();
                Bank.ForEach(i => { price.Add(i.BuyValue); });
                Material.ForEach(i => { price.Add(i.BuyValue); });
                Characters.ForEach(c => { price.Add(c.TotalBuyValue); });
                price.Add(Wallet.Single(e => e.ID == 1).Value);     //ID 1 is coins/gold

                _totalBuyValue = price;
                return _totalBuyValue;
            }
        }

        public ItemPrice TotalSellValue {
            get {
                if(_totalSellValue != null)
                    return _totalSellValue;

                ItemPrice price = new ItemPrice();
                Bank.ForEach(i => { price.Add(i.SellValue); });
                Material.ForEach(i => { price.Add(i.SellValue); });
                Characters.ForEach(c => { price.Add(c.TotalSellValue); });
                price.Add(Wallet.Single(e => e.ID == 1).Value);     //ID 1 is coins/gold
                _totalSellValue = price;
                return _totalSellValue;
            }
        }

        public List<WalletEntry> Wallet { get; internal set; }

        public List<ItemValue> Bank { get; internal set; }

        public List<ItemValue> Material { get; internal set; }

        public List<CharacterValue> Characters {
            get {
                if(_characters == null) {
                    _characters = new List<CharacterValue>();
                }
                return _characters;
            }
        }
    }
}