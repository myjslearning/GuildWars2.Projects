using GuildWars2API.Model.Account;
using GuildWars2API.Model.Character;
using GuildWars2API.Model.Item;
using GuildWars2API.Model.Items;
using GuildWars2API.Model.Market;
using System.Collections.Generic;
using System.Linq;

namespace GuildWars2API.Tools.Value
{
    internal static class ValueManager
    {
        public static AccountValue GetAccountValue(string APIKey) {        //TODO Implement vendor price
            HashSet<int> itemIDs = new HashSet<int>();
            List<Character> characters = GW2API.GetCharacters(APIKey);

            //Gather all Items and ID's, and add all ID's to the main collection
            List<ItemStack> bank = GW2API.GetBank(APIKey);
            List<ItemStack> materialStorage = GW2API.GetMaterialStorage(APIKey);

            Dictionary<Character, Dictionary<string, List<ItemStack>>> charactersInventory = new Dictionary<Character, Dictionary<string, List<ItemStack>>>();
            foreach(Character character in characters) {
                List<ItemStack> Inventory = GetInventory(character.Bags);
                List<ItemStack> Equipment = GetEquiment(character.Equipment);
                charactersInventory.Add(character, new Dictionary<string, List<ItemStack>>() {
                    { "Inventory", Inventory },
                    { "Equipment", Equipment }
                });
                itemIDs.UnionWith(GetItemIDs(Inventory));
                itemIDs.UnionWith(GetItemIDs(Equipment));
            }
            itemIDs.UnionWith(GetItemIDs(bank));
            itemIDs.UnionWith(GetItemIDs(materialStorage, true));

            //Request ItemListing info for all gathered ID's
            List<ItemListing> itemListings = GW2API.GetPrice(itemIDs);

            //Parse them in AccountValue object
            AccountValue account = new AccountValue();
            account.Bank = GetItemValues(bank, itemListings);
            account.Material = GetItemValues(materialStorage, itemListings);
            account.Wallet = GetWallet(APIKey);
            foreach(KeyValuePair<Character, Dictionary<string, List<ItemStack>>> character in charactersInventory) {
                account.Characters.Add(new CharacterValue() {
                    Name = character.Key.Name,
                    Inventory = GetItemValues(character.Value["Inventory"], itemListings),
                    Equipment = GetItemValues(character.Value["Equipment"], itemListings),
                });
            }
            return account;
        }

        private static List<WalletEntry> GetWallet(string APIKey) {
            List<WalletCurrency> currenciesValue = GW2API.GetWallet(APIKey);
            List<WalletCurrencyInfo> currencies = GW2API.GetCurrencyInfo();

            List<WalletEntry> wallet = new List<WalletEntry>();
            foreach(WalletCurrencyInfo currency in currencies) {
                WalletEntry entry = new WalletEntry();
                entry.ID = currency.ID;
                entry.Name = currency.Name;
                entry.Description = currency.Description;
                entry.Icon = currency.Icon;
                entry.Order = currency.Order;
                if(currenciesValue.Any(v => v.ID == currency.ID)) {
                    entry.Value = currenciesValue.Single(v => v.ID == currency.ID).Value;
                }
                wallet.Add(entry);
            }
            return wallet;
        }

        private static List<ItemStack> GetInventory(List<Bag> bags) {
            List<ItemStack> items = new List<ItemStack>();
            foreach(Bag bag in bags) {
                if(bag != null) {
                    bag.Inventory.Where(i => i != null)
                            .ToList()
                            .ForEach(i => items.Add(i));
                }
            }
            return items;
        }

        /// <summary>
        /// Vendor value if account/soul bound.
        /// If it's bind on use, it's considered NOT bound.
        /// </summary>
        /// <param name="equiment"></param>
        /// <returns></returns>
        private static List<ItemStack> GetEquiment(List<Equipment> equiment) {
            List<ItemStack> items = new List<ItemStack>();
            foreach(Equipment item in equiment) {
                items.Add(new ItemStack() {
                    ID = item.ID,
                    Count = 1
                });
            }
            return items;
        }

        private static HashSet<int> GetItemIDs(List<ItemStack> items, bool onlyAvailableItems = false) {
            HashSet<int> itemIDs = new HashSet<int>();
            foreach(ItemStack itemStack in items) {
                if(itemStack != null) {
                    if(onlyAvailableItems && itemStack.Count > 0) {
                        itemIDs.Add(itemStack.ID);
                    }
                    else {
                        itemIDs.Add(itemStack.ID);
                    }
                }
            }
            return itemIDs;
        }

        private static List<ItemValue> GetItemValues(List<ItemStack> items, List<ItemListing> itemListings) {
            List<ItemValue> itemValues = new List<ItemValue>();
            foreach(ItemStack item in items) {
                if(item == null)
                    continue;

                ItemListing listing = null;
                if(itemListings.Any(i => i.ID == item.ID)) {
                    listing = itemListings.Single(i => i.ID == item.ID);
                }

                itemValues.Add(new ItemValue() {
                    ItemStack = item,
                    ItemListing = listing
                });
            }
            return itemValues;
        }
    }
}