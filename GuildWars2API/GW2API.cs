using GuildWars2API.Model.Account;
using GuildWars2API.Model.Character;
using GuildWars2API.Model.Items;
using GuildWars2API.Model.Market;
using GuildWars2API.Model.Recipes;
using GuildWars2API.Network;
using GuildWars2API.Tools.Value;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GuildWars2API
{
    public static class GW2API      //TODO Split this class in smaller end-point
    {
        private const int MAX_ITEMS_PER_REQUEST = 50;       

        #region Items

        public static List<Item> SearchItem(string itemName) {
            string response = NetworkManager.UnauthorizedRequest(URLBuilder.GetItemByName(itemName));
            if(response.Length > 0) {
                List<ItemSearch> itemsFound = JsonConvert.DeserializeObject<List<ItemSearch>>(response);

                HashSet<int> itemIDs = new HashSet<int>();
                itemsFound.ForEach(i => itemIDs.Add(i.ItemID));

                return GetItem(itemIDs);
            }
            return null;
        }

        public static Item GetItem(int itemID) {
            string response = NetworkManager.UnauthorizedRequest(URLBuilder.GetItemByID(itemID));
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<Item>(response);
            }
            return null;
        }

        public static List<Item> GetItem(HashSet<int> itemIDs) => GetLargeRequest<Item>(new List<int>(itemIDs), "items");

        public static ItemListing GetPrice(int itemID) {
            string response = NetworkManager.UnauthorizedRequest(URLBuilder.GetItemListing(itemID));
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<ItemListing>(response);
            }
            return null;
        }

        public static List<ItemListing> GetPrice(HashSet<int> itemIDs) => GetLargeRequest<ItemListing>(new List<int>(itemIDs), "commerce/prices");

        #endregion Items

        #region Recipes

        public static List<Recipe> AvailableRecipes(int itemID) {
            string response = NetworkManager.UnauthorizedRequest(URLBuilder.GetRecipesByItemID(itemID));
            if(response.Length > 0) {
                List<int> recipeIDs = JsonConvert.DeserializeObject<List<int>>(response);
                return GetRecipe(new HashSet<int>(recipeIDs));
            }
            return null;
        }

        public static Recipe GetRecipe(int recipeID) {
            string response = NetworkManager.UnauthorizedRequest(URLBuilder.GetRecipeByRecipeID(recipeID));
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<Recipe>(response);
            }
            return null;
        }

        /// <summary>
        /// Gets the recipe with the corresponding recipeID
        /// </summary>
        /// <param name="recipeIDs"></param>
        /// <returns></returns>
        public static List<Recipe> GetRecipe(HashSet<int> recipeIDs) => GetLargeRequest<Recipe>(new List<int>(recipeIDs), "recipes");

        #endregion Recipes

        #region Account

        public static List<Character> GetCharacters(string APIKey) {
            string response = NetworkManager.AuthorizedRequest(URLBuilder.GetCharacters(), APIKey);
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<List<Character>>(response);
            }
            return null;
        }

        internal static List<ItemStack> GetMaterialStorage(string APIKey) {
            string response = NetworkManager.AuthorizedRequest(URLBuilder.GetMaterialStorage(), APIKey);
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<List<ItemStack>>(response);
            }
            return null;
        }

        internal static List<ItemStack> GetBank(string APIKey) {
            string response = NetworkManager.AuthorizedRequest(URLBuilder.GetBank(), APIKey);
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<List<ItemStack>>(response);
            }
            return null;
        }

        internal static List<WalletCurrencyInfo> GetCurrencyInfo() {
            string response = NetworkManager.UnauthorizedRequest(URLBuilder.GetCurrencies());
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<List<WalletCurrencyInfo>>(response);
            }
            return null;
        }

        internal static List<WalletCurrency> GetWallet(string APIKey) {
            string response = NetworkManager.AuthorizedRequest(URLBuilder.GetWallet(), APIKey);
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<List<WalletCurrency>>(response);
            }
            return null;
        }

        private static List<T> GetLargeRequest<T>(List<int> IDs, string apiCategory, string APIKey = null) {
            List<T> results = new List<T>();
            if(IDs.Count <= 0) {
                return results;
            }

            //Devide collection in smaller parts
            if(IDs.Count > MAX_ITEMS_PER_REQUEST) {
                List<int> selected = new List<int>();

                selected.AddRange(IDs.GetRange(MAX_ITEMS_PER_REQUEST, IDs.Count - MAX_ITEMS_PER_REQUEST));
                IDs.RemoveRange(MAX_ITEMS_PER_REQUEST, IDs.Count - MAX_ITEMS_PER_REQUEST);

                results.AddRange(GetLargeRequest<T>(selected, apiCategory));
            }

            //Authorized or Unauthorized Request
            string response = "";
            if(APIKey == null) {
                response = NetworkManager.UnauthorizedRequest(URLBuilder.LargeRequestURL(apiCategory, IDs));
            }
            else {
                response = NetworkManager.AuthorizedRequest(URLBuilder.LargeRequestURL(apiCategory, IDs), APIKey);
            }

            //Read response
            if(response.Length > 0) {
                results.AddRange(JsonConvert.DeserializeObject<List<T>>(response));
            }
            return results;
        }

        #endregion Account

        #region Tools

        public static AccountValue GetAccountValue(string APIKey) => ValueManager.GetAccountValue(APIKey);

        #endregion Tools

        [SuppressMessage("", "CSE0003")]
        public static object Test(string APIKey) {
            return GetWallet(APIKey);
        }
    }
}