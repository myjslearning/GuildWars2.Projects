using GuildWars2API.Model.Account;
using GuildWars2API.Model.Commerce;
using System.Collections.Generic;

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
                
                /*
                Sell listing is a different story than buylisting, because the value is added in raw gold to your account. So the buy/sell listing value of the item 
                does not matter, because it will sell for the amount of gold you listed it for.
                */
                OwnSellListings.ForEach(s => { price.Add(s.Transaction.Quantity * s.ItemListing.Sells.UnitPrice); });
                price.Add(Wallet.Find(e => e.ID == 1).Value);                               //ID 1 = gold  
                price.Add(GemConversion.CoinsPerGem * Wallet.Find(e => e.ID == 4).Value);   //ID 4 = gems                    
                                                                                                                    
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

                /* The gold to buy these items is already deducted from your account. 
                So we just imagine you already have these items in your account and just calculate the TP price as if you tried to sell them. */
                OwnBuyListings.ForEach(b => { price.Add(b.Transaction.Quantity * b.ItemListing.Buys.UnitPrice); });

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

                /* The gold to buy these items is already deducted from your account. 
                So we just imagine you already have these items in your account and just calculate the TP price as if you tried to sell them. */
                OwnBuyListings.ForEach(b => { price.Add(b.Transaction.Quantity * b.ItemListing.Sells.UnitPrice); });

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

        public List<TransactionValue> OwnSellListings { get; set; }         

        public List<TransactionValue> OwnBuyListings { get; set; }

        public GemExchange GemConversion { get; set; }
    }
}