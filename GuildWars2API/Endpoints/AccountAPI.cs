using GuildWars2API.Model.Account;
using GuildWars2API.Model.Character;
using GuildWars2API.Model.Commerce;
using GuildWars2API.Model.Items;
using GuildWars2API.Network;
using System.Collections.Generic;
using static GuildWars2API.Network.NetworkManager;

namespace GuildWars2API
{
    public static class AccountAPI
    {
#pragma warning disable CSE0003
        public static List<Character> GetCharacters(string APIKey) {
            return AuthorizedRequest<List<Character>>(URLBuilder.GetCharacters(), APIKey);
        }

        public static List<ItemStack> GetMaterialStorage(string APIKey) {
            return AuthorizedRequest<List<ItemStack>>(URLBuilder.GetMaterialStorage(), APIKey);
        }

        public static List<ItemStack> GetBank(string APIKey) {
            return AuthorizedRequest<List<ItemStack>>(URLBuilder.GetBank(), APIKey);
        }

        public static List<WalletCurrency> GetWallet(string APIKey) {
            return AuthorizedRequest<List<WalletCurrency>>(URLBuilder.GetWallet(), APIKey);
        }
        
        public static List<Transaction> GetCurrentSellListing(string APIKey) {
            return AuthorizedRequest<List<Transaction>>(URLBuilder.GetCurrentSellListings(), APIKey);
        }

        public static List<Transaction> GetCurrentBuyListing(string APIKey) {
            return AuthorizedRequest<List<Transaction>>(URLBuilder.GetCurrentBuyListings(), APIKey);
        }

        public static AccountInventory GetAccountInventory(string APIKey) {
            return new AccountInventory() {
                Characters = GetCharacters(APIKey),
                Bank = GetBank(APIKey),
                MaterialStorage = GetMaterialStorage(APIKey),
                OwnBuyListings = GetCurrentBuyListing(APIKey),
                OwnSellListings = GetCurrentSellListing(APIKey)
            };
        }
#pragma warning restore CSE0003
    }
}