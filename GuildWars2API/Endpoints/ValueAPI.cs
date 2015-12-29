using GuildWars2API.Model.Account;
using GuildWars2API.Model.Character;
using GuildWars2API.Model.Commerce;
using GuildWars2API.Model.Item;
using GuildWars2API.Model.Items;
using GuildWars2API.Model.Value;
using System;
using System.Collections.Generic;
using System.Linq;

using static GuildWars2API.AccountAPI;

namespace GuildWars2API
{
    public static class ValueAPI
    {

        /// <summary>
        /// Calculates Account Value. All-in-one method. 
        /// Heavy API Call.
        /// </summary>
        /// <param name="APIKey"></param>
        /// <returns></returns>
        public static AccountValue GetAccountValue(string APIKey) => GetAccountValue(APIKey, new List<Item>());

        /// <summary>
        /// Calculates Account Value. Uses known Items to reduce amount of calls.
        /// Lighter API Call(Depends on the amount of known Itemlistings and Items).
        /// </summary>
        /// <param name="APIKey"></param>
        /// <param name="knownItemListings"></param>
        /// <param name="knownItems"></param>
        /// <returns></returns>
        public static AccountValue GetAccountValue(string APIKey, List<Item> knownItems) {
            AccountInventory accountInv = GetAccountInventory(APIKey);

            //Retrive all IDs that need to be called from the Official GWAPI    
            HashSet<int> itemIDs = GetAccountIDs(APIKey, accountInv);
            Console.WriteLine("Count = " + itemIDs.Count);
            //Determines what items are already known and are removed from the request
            HashSet<int> unknownItemIDs = new HashSet<int>(itemIDs.Except(GetIDs(knownItems)).ToList());
            List<Item> items = ItemAPI.GetItem(unknownItemIDs);
            items.AddRange(knownItems);
            Console.WriteLine("Count = " + itemIDs.Count);
            List<ItemListing> itemListings = ItemAPI.GetPriceListing(itemIDs);

            //Parse it into object
            AccountValue account = new AccountValue();
            account.Bank = GetItemValues(accountInv.Bank, itemListings, items);
            account.Material = GetItemValues(accountInv.MaterialStorage, itemListings, items);
            account.Wallet = GetWalletEntries(APIKey);
            account.SellListings = GetTransactionValues(itemListings, accountInv.SellListings);
            account.BuyListings = GetTransactionValues(itemListings, accountInv.BuyListings);
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
                MaterialStorage = GetMaterialStorage(APIKey),
                BuyListings = GetCurrentBuyListing(APIKey),
                SellListings = GetCurrentSellListing(APIKey)
            };
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

        private static List<TransactionValue> GetTransactionValues(List<ItemListing> itemListings, List<Transaction> transactions) {
            List<TransactionValue> transactionValues = new List<TransactionValue>();
            foreach(Transaction transaction in transactions) {
                if(itemListings.Any(i => i.ID == transaction.ItemID)) {
                    transactionValues.Add(new TransactionValue() {
                        ItemListing = itemListings.Single(i => i.ID == transaction.ItemID),
                        Transaction = transaction
                    });
                }
            }
            return transactionValues;
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

        private static HashSet<int> GetIDs(List<Transaction> transactions) {
            HashSet<int> itemIDs = new HashSet<int>();
            foreach(Transaction transaction in transactions) {
                if(transaction != null) {
                    itemIDs.Add(transaction.ItemID);
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

            itemIDs.UnionWith(GetIDs(accountInv.SellListings));
            itemIDs.UnionWith(GetIDs(accountInv.BuyListings));

            return itemIDs;
        }

        #endregion Private Methods
    }
}
