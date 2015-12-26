using System;
using System.Collections.Generic;

namespace GuildWars2API.Network
{
    internal class URLBuilder
    {
        private const string ROOT_GW2 = "https://api.guildwars2.com/v2";
        private const string ROOT_GW2TNO = "http://www.gw2shinies.com/api/json/idbyname";

        private const string BANK = "bank";
        private const string FILES = "files";
        private const string ITEMS = "items";
        private const string WALLET = "wallet";
        private const string SEARCH = "search";
        private const string ACCOUNT = "account";

        private const string PRICES = "prices";                 //Returns highest TP listings
        private const string LISTINGS = "listings";             //Returns all TP listings
        private const string TRANSACTIONS = "transactions";     //Returns all current account TP listings

        private const string RECIPES = "recipes";
        private const string COMMERCE = "commerce";
        private const string MATERIALS = "materials";
        private const string CHARACTERS = "characters";
        private const string CURRENCIES = "currencies";

        #region Items

        public static string GetItemByName(string itemName) => string.Format("{0}/{1}", ROOT_GW2TNO, Encode(itemName.ToLower()).Replace("+", "%20"));

        public static string GetItemByID(int itemID) => string.Format("{0}/{1}/{2}", ROOT_GW2, ITEMS, itemID);

        public static string GetItemByID(List<int> itemIDs) {
            string baseURL = string.Format("{0}/{1}?ids=", ROOT_GW2, ITEMS);
            return AddIds(baseURL, itemIDs);
        }

        #endregion Items

        #region Recipes

        public static string GetRecipeByID(List<int> recipeIDs) {
            string baseURL = string.Format("{0}/{1}?ids=", ROOT_GW2, RECIPES);
            return AddIds(baseURL, recipeIDs);
        }

        public static string GetRecipeByRecipeID(int recipeID) => string.Format("{0}/{1}/{2}", ROOT_GW2, RECIPES, recipeID);

        public static string GetRecipesByItemID(int itemID) => string.Format("{0}/{1}/{2}?output={3}", ROOT_GW2, RECIPES, SEARCH, itemID);

        #endregion Recipes

        #region Tradepost

        public static string GetItemListing(int itemID) => string.Format("{0}/{1}/{2}/{3}", ROOT_GW2, COMMERCE, PRICES, itemID);

        public static string GetItemListing(List<int> itemIDs) {
            string baseURL = string.Format("{0}/{1}/{2}?ids=", ROOT_GW2, COMMERCE, PRICES);
            return AddIds(baseURL, itemIDs);
        }

        public static string GetCurrentSellListings() => string.Format("{0}/{1}/{2}/{3}/{4}", ROOT_GW2, COMMERCE, TRANSACTIONS, "current", "sells");
        public static string GetCurrentBuyListings() => string.Format("{0}/{1}/{2}/{3}/{4}", ROOT_GW2, COMMERCE, TRANSACTIONS, "current", "buys");

        #endregion Tradepost

        #region Account

        public static string GetCharacters() => string.Format("{0}/{1}?page=0", ROOT_GW2, CHARACTERS);

        public static string GetMaterialStorage() => string.Format("{0}/{1}/{2}", ROOT_GW2, ACCOUNT, MATERIALS);

        public static string GetBank() => string.Format("{0}/{1}/{2}", ROOT_GW2, ACCOUNT, BANK);

        public static string GetWallet() => string.Format("{0}/{1}/{2}", ROOT_GW2, ACCOUNT, WALLET);

        public static string GetCurrencies() => string.Format("{0}/{1}?page=0", ROOT_GW2, CURRENCIES);

        #endregion Account

        public static string GetAssetFromID(string assetID) => string.Format("{0}/{1}?ids={2}", ROOT_GW2, FILES, Encode(assetID));

        #region Utility

        private static string Encode(string value) {
            var result = Uri.EscapeDataString(value);
            return result;
        }

        private static string AddIds(string baseURL, List<int> IDs) {
            foreach(int id in IDs) {
                baseURL = baseURL + id + ",";
            }
            baseURL = baseURL.Remove(baseURL.Length - 1);   //Remove acces comma
            return baseURL;
        }

        public static string LargeRequestURL(string category, List<int> IDs) {
            string baseURL = string.Format("{0}/{1}?ids=", ROOT_GW2, category);
            return AddIds(baseURL, IDs);
        }

        #endregion Utility
    }
}