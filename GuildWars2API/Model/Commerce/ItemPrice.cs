namespace GuildWars2API.Model.Commerce
{
    public class ItemPrice
    {
        private int TotalCoins { get; set; }

        public int Gold { get; private set; }

        public int Silver { get; private set; }

        public int Copper { get; private set; }

        public ItemPrice() : this(0) {
        }

        public ItemPrice(int totalCoins) {
            TotalCoins = totalCoins;
            CalculateCoins();
        }

        public void Add(ItemPrice price) => Add(price.TotalCoins);

        public void Add(int coins) {
            TotalCoins = TotalCoins + coins;
            CalculateCoins();
        }

        private int[] CalculateCoins() {
            int[] values = new int[3];

            Gold = TotalCoins / 10000;
            int rest = TotalCoins - (Gold * 10000);
            Silver = rest / 100;
            rest = rest - (Silver * 100);
            Copper = rest;

            return values;
        }
    }
}