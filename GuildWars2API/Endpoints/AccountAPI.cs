using GuildWars2API.Model.Account;
using GuildWars2API.Model.Character;
using GuildWars2API.Model.Commerce;
using GuildWars2API.Model.Item;
using GuildWars2API.Model.Items;
using GuildWars2API.Model.Value;
using GuildWars2API.Network;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GuildWars2API
{
    public static class AccountAPI
    {
        #region Public Account Value

        /// <summary>
        /// Calculates Account Value. All-in-one method. 
        /// Heavy API Call.
        /// </summary>
        /// <param name="APIKey"></param>
        /// <returns></returns>
        public static AccountValue GetAccountValue(string APIKey) => GetAccountValue(APIKey, new List<ItemListing>(), new List<Item>());

        /// <summary>
        /// Calculates Account Value. Uses known ItemListings and Items to calculate more quickly.
        /// Lighter API Call(Depends on the amount of known Itemlistings and Items).
        /// </summary>
        /// <param name="APIKey"></param>
        /// <param name="knownItemListings"></param>
        /// <param name="knownItems"></param>
        /// <returns></returns>
        public static AccountValue GetAccountValue(string APIKey, List<ItemListing> knownItemListings, List<Item> knownItems) {
            AccountInventory accountInv = GetAccountInventory(APIKey);  
            
            //Retrive all IDs that need to be called from the Official GWAPI    
            HashSet<int> itemIDs = GetAccountIDs(APIKey, accountInv);
            itemIDs.ExceptWith(GetKnownItemIDs(knownItemListings, knownItems));   //Redesign GetKnownItemIDs method

            //Combine known and newly found Items and ItemListings
            List<Item> items = ItemAPI.GetItem(itemIDs);
            items.AddRange(knownItems);
            List<ItemListing> itemListings = ItemAPI.GetPriceListing(itemIDs);
            itemListings.AddRange(knownItemListings);

            //Parse it into object
            AccountValue account = new AccountValue();
            account.Bank = GetItemValues(accountInv.Bank, itemListings, items);
            account.Material = GetItemValues(accountInv.MaterialStorage, itemListings, items);
            account.Wallet = GetWalletEntries(APIKey);
            account.SellListings = GetCurrentSellListing(APIKey);
            foreach(Character character in accountInv.Characters) {
                account.Characters.Add(new CharacterValue() {
                    Name = character.Name,
                    Inventory = GetItemValues(GetInventory(character.Bags), itemListings, items),
                    Equipment = GetItemValues(GetEquiment(character.Equipment), itemListings, items),
                });
            }
            return account;
        }
        
        public static AccountInventory GetAccountInventory(string APIKey) {
            return new AccountInventory() {
                Characters = GetCharacters(APIKey),
                Bank = GetBank(APIKey),
                MaterialStorage = GetMaterialStorage(APIKey)
            };
        }

        #endregion Public Account Value

        #region Public API Getters

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
        
        public static List<Transaction> GetCurrentSellListing(string APIKey) {
            string response = NetworkManager.AuthorizedRequest(URLBuilder.GetCurrentSellListings(), APIKey);
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<List<Transaction>>(response);
            }
            return null;
        }

        public static List<Transaction> GetCurrentBuyListing(string APIKey) {
            string response = NetworkManager.AuthorizedRequest(URLBuilder.GetCurrentBuyListings(), APIKey);
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<List<Transaction>>(response);
            }
            return null;
        }

        #endregion Public API Getters

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
        
        private static HashSet<int> GetIDs(List<Item> items) {
            HashSet<int> itemIDs = new HashSet<int>();
            foreach(Item item in items) {
                if(item != null) {
                    itemIDs.Add(item.ID);
                }
            }
            return itemIDs;
        }

        private static HashSet<int> GetIDs(List<ItemStack> items) {
            HashSet<int> itemIDs = new HashSet<int>();
            foreach(ItemStack itemStack in items) {
                if(itemStack != null) {
                    itemIDs.Add(itemStack.ID);
                }
            }
            return itemIDs;
        }
        
        private static HashSet<int> GetIDs(List<ItemListing> itemListings) {
            HashSet<int> itemIDs = new HashSet<int>();
            foreach(ItemListing itemListing in itemListings) {
                if(itemListing != null) {
                    itemIDs.Add(itemListing.ID);
                }
            }
            return itemIDs;
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

        private static HashSet<int> GetAccountIDs(string APIKey, AccountInventory accountInv) {
            HashSet<int> itemIDs = new HashSet<int>();

            Dictionary<Character, Dictionary<string, List<ItemStack>>> charactersInventory = new Dictionary<Character, Dictionary<string, List<ItemStack>>>();
            foreach(Character character in accountInv.Characters) {
                itemIDs.UnionWith(GetIDs(GetInventory(character.Bags)));
                itemIDs.UnionWith(GetIDs(GetEquiment(character.Equipment)));
            }
            itemIDs.UnionWith(GetIDs(accountInv.Bank));
            itemIDs.UnionWith(GetIDs(accountInv.MaterialStorage));

            return itemIDs;
        }

        private static HashSet<int> GetKnownItemIDs(List<ItemListing> knownItemListings, List<Item> knownItems) {
            HashSet<int> knownItemListingsIDs = GetIDs(knownItemListings);
            HashSet<int> knownItemsIDs = GetIDs(knownItems);
            knownItemListingsIDs.IntersectWith(knownItemsIDs);
            return knownItemListingsIDs;
        }

        #endregion Private Methods
    }
}