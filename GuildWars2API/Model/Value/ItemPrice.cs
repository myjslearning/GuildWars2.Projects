using System;

namespace GuildWars2API.Model.Value
{
    public class ItemPrice
    {
        private int TotalCoins { get; set; }

        public int Gold => TotalCoins / 10000;

        public int Silver {
            get {
                int rest = TotalCoins - (Gold * 10000);
                return rest / 100;
            }
        }

        public int Copper { 
            get {
                int rest = TotalCoins - (Gold * 10000);
                return rest - (Silver * 100);
            }
        }

        public ItemPrice() : this(0) {
        }

        public ItemPrice(int totalCoins) {
            Add(totalCoins);
        }

        public ItemPrice(int totalCoins, int priceDeduction) {
            Add(totalCoins, priceDeduction);
        }

        public void Add(ItemPrice price) => Add(price.TotalCoins);

        public void Add(int coins) {
            TotalCoins = TotalCoins + coins;
        }

        internal void Add(int coins, int procentDeduction) {
            double multiplier = 1 - (procentDeduction / 100.0);
            Add(Convert.ToInt32(coins * multiplier));
        }
    }
}