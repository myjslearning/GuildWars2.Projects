using GuildWars2API.Model.Account;
using GuildWars2API.Model.Character;
using GuildWars2API.Model.Commerce;
using GuildWars2API.Model.Items;
using GuildWars2API.Network;
using System.Collections.Generic;
using static GuildWars2API.Network.NetworkManager;
using static Newtonsoft.Json.JsonConvert;

namespace GuildWars2API
{
    public static class AccountAPI
    {
        public static List<Character> GetCharacters(string APIKey) {
            string response = AuthorizedRequest(URLBuilder.GetCharacters(), APIKey);
            if(response.Length > 0) {
                return DeserializeObject<List<Character>>(response);
            }
            return null;
        }

        public static List<ItemStack> GetMaterialStorage(string APIKey) {
            string response = AuthorizedRequest(URLBuilder.GetMaterialStorage(), APIKey);
            if(response.Length > 0) {
                return DeserializeObject<List<ItemStack>>(response);
            }
            return null;
        }

        public static List<ItemStack> GetBank(string APIKey) {
            string response = AuthorizedRequest(URLBuilder.GetBank(), APIKey);
            if(response.Length > 0) {
                return DeserializeObject<List<ItemStack>>(response);
            }
            return null;
        }

        public static List<WalletCurrency> GetWallet(string APIKey) {
            string response = AuthorizedRequest(URLBuilder.GetWallet(), APIKey);
            if(response.Length > 0) {
                return DeserializeObject<List<WalletCurrency>>(response);
            }
            return null;
        }
        
        public static List<Transaction> GetCurrentSellListing(string APIKey) {
            string response = AuthorizedRequest(URLBuilder.GetCurrentSellListings(), APIKey);
            if(response.Length > 0) {
                return DeserializeObject<List<Transaction>>(response);
            }
            return null;
        }

        public static List<Transaction> GetCurrentBuyListing(string APIKey) {
            string response = AuthorizedRequest(URLBuilder.GetCurrentBuyListings(), APIKey);
            if(response.Length > 0) {
                return DeserializeObject<List<Transaction>>(response);
            }
            return null;
        }

#pragma warning disable CSE0003
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