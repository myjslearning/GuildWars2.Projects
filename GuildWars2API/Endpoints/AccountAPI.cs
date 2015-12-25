using GuildWars2API.Model.Account;
using GuildWars2API.Model.Character;
using GuildWars2API.Model.Items;
using GuildWars2API.Network;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API
{
    public static class AccountAPI
    {
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
    }
}
