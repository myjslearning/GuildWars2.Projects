using GuildWars2API.Model.Commerce;
using GuildWars2API.Model.Items;
using System.Collections.Generic;

namespace GuildWars2API.Model.Account
{
    public class AccountInventory {
        public List<Character.Character> Characters { get; set; }

        public List<ItemStack> Bank { get; set; }

        public List<ItemStack> MaterialStorage { get; set; }

        public List<Transaction> OwnBuyListings { get; set; }

        public List<Transaction> OwnSellListings { get; set; }
    }
}