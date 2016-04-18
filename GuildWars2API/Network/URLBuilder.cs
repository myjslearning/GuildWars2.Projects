using System;
using System.Collections.Generic;

namespace GuildWars2API.Network
{
    internal class URLBuilder {
        private const string ROOT_GW2_V1 = "https://api.guildwars2.com/v1";
        private const string ROOT_GW2_V2 = "https://api.guildwars2.com/v2";
        private const string ROOT_GW2PRO = "http://gw2profits.com/json/v2/forge";
        private const string ROOT_GW2SHI = "http://www.gw2shinies.com/api/json/idbyname";

        //Second Endpoints
        private const string FILES = "files";
        private const string ITEMS = "items";
        private const string ACCOUNT = "account";
        private const string COMMERCE = "commerce";
        private const string CHARACTERS = "characters";
        private const string CURRENCIES = "currencies";

        private const string GUILD = "guild";
        private const string GUILD_DETAILS = "guild_details";

        //Third Endpoints
        private const string BANK = "bank";
        private const string WALLET = "wallet";
        private const string SEARCH = "search";                 //Used for searching recipes
        private const string PRICES = "prices";                 //Returns highest TP listings
        private const string RECIPES = "recipes";
        private const string EXCHANGE = "exchange";
        private const string MATERIALS = "materials";
        private const string TRANSACTIONS = "transactions";     //Returns all current account TP listings

        #region Items

        public static string GetItemByName(string itemName) => string.Format("{0}/{1}", ROOT_GW2SHI, Encode(itemName.ToLower()).Replace("+", "%20"));

        public static string GetItemByID(int itemID) => string.Format("{0}/{1}/{2}", ROOT_GW2_V2, ITEMS, itemID);

        public static string GetItemByID(HashSet<int> itemIDs) {
            string baseURL = string.Format("{0}/{1}?ids=", ROOT_GW2_V2, ITEMS);
            return AddIds(baseURL, itemIDs);
        }

        #endregion Items

        #region Recipes

        public static string GetRecipeByID(int recipeID) => string.Format("{0}/{1}/{2}", ROOT_GW2_V2, RECIPES, recipeID);

        public static string GetRecipeByID(HashSet<int> recipeIDs) {
            string baseURL = string.Format("{0}/{1}?ids=", ROOT_GW2_V2, RECIPES);
            return AddIds(baseURL, recipeIDs);
        }

        public static string GetRecipesByItemID(int itemID) => string.Format("{0}/{1}/{2}?output={3}", ROOT_GW2_V2, RECIPES, SEARCH, itemID);

        #region Mystic Forge

        public static string GetMysticForgeRecipes() => ROOT_GW2PRO;

        #endregion Mystic Forge

        #endregion Recipes

        #region Market

        public static string GetItemListing(int itemID) => string.Format("{0}/{1}/{2}/{3}", ROOT_GW2_V2, COMMERCE, PRICES, itemID);

        public static string GetItemListing(HashSet<int> itemIDs) {
            string baseURL = string.Format("{0}/{1}/{2}?ids=", ROOT_GW2_V2, COMMERCE, PRICES);
            return AddIds(baseURL, itemIDs);
        }

        public static string GetCurrentSellListings() => string.Format("{0}/{1}/{2}/{3}/{4}", ROOT_GW2_V2, COMMERCE, TRANSACTIONS, "current", "sells");
        public static string GetCurrentBuyListings() => string.Format("{0}/{1}/{2}/{3}/{4}", ROOT_GW2_V2, COMMERCE, TRANSACTIONS, "current", "buys");

        public static string GetGoldToGemConversion => string.Format("{0}/{1}/{2}/{3}", ROOT_GW2_V2, COMMERCE, EXCHANGE, "coins");
        public static string GetGemToGoldConversion => string.Format("{0}/{1}/{2}/{3}", ROOT_GW2_V2, COMMERCE, EXCHANGE, "gems");

        #endregion Market

        #region Account

        public static string GetCharacters() => string.Format("{0}/{1}?page=0", ROOT_GW2_V2, CHARACTERS);

        public static string GetMaterialStorage() => string.Format("{0}/{1}/{2}", ROOT_GW2_V2, ACCOUNT, MATERIALS);

        public static string GetBank() => string.Format("{0}/{1}/{2}", ROOT_GW2_V2, ACCOUNT, BANK);

        public static string GetWallet() => string.Format("{0}/{1}/{2}", ROOT_GW2_V2, ACCOUNT, WALLET);

        public static string GetCurrencies() => string.Format("{0}/{1}?page=0", ROOT_GW2_V2, CURRENCIES);

        #endregion Account

        #region Guild

        public static string GetGuildDetailsByID(string guildID) => string.Format("{0}/{1}.json?guild_id={2}", ROOT_GW2_V1, GUILD_DETAILS, guildID);
    
        public static string GetGuildDetailsByName(string guildName) => string.Format("{0}/{1}.json?guild_name={2}", ROOT_GW2_V1, GUILD_DETAILS, Encode(guildName));

        public static string GetGuildLog(string guildID) => string.Format("{0}/{1}/{2}/log", ROOT_GW2_V2, GUILD, guildID);

        #endregion Guild

        public static string GetAssetFromID(string assetID) => string.Format("{0}/{1}?ids={2}", ROOT_GW2_V2, FILES, Encode(assetID));

        #region Utility

        private static string Encode(string value) {
            var result = Uri.EscapeDataString(value);
            return result;
        }

        private static string AddIds(string baseURL, HashSet<int> IDs) {
            foreach(int id in IDs) {
                baseURL = baseURL + id + ",";
            }
            baseURL = baseURL.Remove(baseURL.Length - 1);   //Remove acces comma
            return baseURL;
        }

        public static string LargeRequestURL(string category, HashSet<int> IDs) {
            string baseURL = string.Format("{0}/{1}?ids=", ROOT_GW2_V2, category);
            return AddIds(baseURL, IDs);
        }

        #endregion Utility
    }
}