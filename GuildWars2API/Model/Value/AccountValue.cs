using GuildWars2API.Model.Account;
using GuildWars2API.Model.Commerce;
using System.Collections.Generic;
using System.Linq;

namespace GuildWars2API.Model.Value
{
    public class AccountValue
    {
        private ItemPrice _baseValue;
        private ItemPrice _totalBuyValue;
        private ItemPrice _totalSellValue;
        private List<CharacterValue> _characters;

        private ItemPrice BaseValue
        {
            get {
                if(_baseValue != null)
                    return _baseValue;

                ItemPrice price = new ItemPrice();
                SellListings.ForEach(s => { price.Add(s.Transaction.Quantity * s.Transaction.Price); });
                BuyListings.ForEach(b => { price.Add(b.Transaction.Quantity * b.Transaction.Price); });
                price.Add(Wallet.Single(e => e.ID == 1).Value);     //ID 1 is coins/gold

                _baseValue = price;
                return price;
            }
        }

        public ItemPrice TotalBuyValue {
            get {
                if(_totalBuyValue != null)
                    return _totalBuyValue;

                ItemPrice price = new ItemPrice();
                price.Add(BaseValue);
                Bank.ForEach(i => { price.Add(i.BuyValue); });
                Material.ForEach(i => { price.Add(i.BuyValue); });
                Characters.ForEach(c => { price.Add(c.TotalBuyValue); });

                _totalBuyValue = price;
                return _totalBuyValue;
            }
        }       

        public ItemPrice TotalSellValue {
            get {
                if(_totalSellValue != null)
                    return _totalSellValue;

                ItemPrice price = new ItemPrice();
                price.Add(BaseValue);
                Bank.ForEach(i => { price.Add(i.SellValue); });
                Material.ForEach(i => { price.Add(i.SellValue); });
                Characters.ForEach(c => { price.Add(c.TotalSellValue); });

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

        public List<TransactionValue> SellListings { get; set; }

        public List<TransactionValue> BuyListings { get; set; }
    }
}