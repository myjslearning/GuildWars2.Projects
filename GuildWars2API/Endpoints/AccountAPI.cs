using GuildWars2API.Model.Account;
using GuildWars2API.Model.Character;
using GuildWars2API.Model.Item;
using GuildWars2API.Model.Items;
using GuildWars2API.Model.Market;
using GuildWars2API.Network;
using GuildWars2API.Tools.Value;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GuildWars2API
{
    public static class AccountAPI
    {
        public static AccountValue GetAccountValue(string APIKey) {
            HashSet<int> itemIDs = new HashSet<int>();
            List<Character> characters = AccountAPI.GetCharacters(APIKey);

            //Gather all Items and ID's, and add all ID's to the main collection
            List<ItemStack> bank = AccountAPI.GetBank(APIKey);
            List<ItemStack> materialStorage = AccountAPI.GetMaterialStorage(APIKey);

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

            //Request ItemListing and ItemObject info for all gathered ID's
            List<Item> items = ItemAPI.GetItem(itemIDs);
            List<ItemListing> itemListings = ItemAPI.GetPriceListing(itemIDs);

            //Parse them in AccountValue object
            AccountValue account = new AccountValue();
            account.Bank = GetItemValues(bank, itemListings, items);
            account.Material = GetItemValues(materialStorage, itemListings, items);
            account.Wallet = GetWalletEntries(APIKey);
            foreach(KeyValuePair<Character, Dictionary<string, List<ItemStack>>> character in charactersInventory) {
                account.Characters.Add(new CharacterValue() {
                    Name = character.Key.Name,
                    Inventory = GetItemValues(character.Value["Inventory"], itemListings, items),
                    Equipment = GetItemValues(character.Value["Equipment"], itemListings, items),
                });
            }
            return account;
        }

        public static List<Character> GetCharacters(string APIKey) {
            string response = NetworkManager.AuthorizedRequest(URLBuilder.GetCharacters(), APIKey);
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<List<Character>>(response);
            }
            return null;
        }

        public static List<ItemStack> GetMaterialStorage(string APIKey) {
            string response = NetworkManager.AuthorizedRequest(URLBuilder.GetMaterialStorage(), APIKey);
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<List<ItemStack>>(response);
            }
            return null;
        }

        public static List<ItemStack> GetBank(string APIKey) {
            string response = NetworkManager.AuthorizedRequest(URLBuilder.GetBank(), APIKey);
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<List<ItemStack>>(response);
            }
            return null;
        }

        public static List<WalletCurrencyInfo> GetCurrencyInfo() {
            string response = NetworkManager.UnauthorizedRequest(URLBuilder.GetCurrencies());
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<List<WalletCurrencyInfo>>(response);
            }
            return null;
        }

        public static List<WalletCurrency> GetWallet(string APIKey) {
            string response = NetworkManager.AuthorizedRequest(URLBuilder.GetWallet(), APIKey);
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<List<WalletCurrency>>(response);
            }
            return null;
        }
        
        #region Private Methods

        private static List<WalletEntry> GetWalletEntries(string APIKey) {
            List<WalletCurrency> currenciesValue = GetWallet(APIKey);
            List<WalletCurrencyInfo> currencies = GetCurrencyInfo();

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

        private static List<ItemValue> GetItemValues(List<ItemStack> itemStacks, List<ItemListing> itemListings, List<Item> items) {
            List<ItemValue> itemValues = new List<ItemValue>();
            foreach(ItemStack itemStack in itemStacks) {
                if(itemStack == null)
                    continue;

                //Retrieve item bound status and vendor price
                bool isBound = false;
                ItemPrice vendorPrice = null;

                if(items.Any(i => i.ID == itemStack.ID)) {
                    Item item = items.Single(i => i.ID == itemStack.ID);
                    if(IsBound(item)) {
                        isBound = true;
                    }
                    if(items.Any(i => i.ID == item.ID)) {
                        if(IsSellable(item)) {
                            vendorPrice = new ItemPrice(items.Single(i => i.ID == item.ID).VendorValue);
                        }
                    }
                }

                //Retrieve itemListing
                ItemListing itemListing = null;

                if(itemListings.Any(i => i.ID == itemStack.ID)) {
                    itemListing = itemListings.Single(i => i.ID == itemStack.ID);
                }

                itemValues.Add(new ItemValue() {
                    IsBound = isBound,
                    ItemStack = itemStack,
                    VendorPrice = vendorPrice,
                    ItemListing = itemListing
                });
            }
            return itemValues;
        }

        private static bool IsBound(Item item) {
            if(item.Flags.Contains("AccountBound") || item.Flags.Contains("SoulbindOnAcquire")) {
                return true;
            }
            return false;
        }

        private static bool IsSellable(Item item) {
            if(item.Flags.Contains("NoSell")) {
                return false;
            }
            return true;
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

        #endregion Private Methods
    }
}
