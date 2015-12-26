using GuildWars2API.Model.Commerce;
using GuildWars2API.Model.Items;
using GuildWars2API.Network;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GuildWars2API
{
    public static class ItemAPI
    {
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

        public static List<Item> GetItem(HashSet<int> itemIDs) => NetworkManager.GetLargeRequest<Item>(new List<int>(itemIDs), "items");

        public static ItemListing GetPriceListing(int itemID) {
            string response = NetworkManager.UnauthorizedRequest(URLBuilder.GetItemListing(itemID));
            if(response.Length > 0) {
                return JsonConvert.DeserializeObject<ItemListing>(response);
            }
            return null;
        }

        public static List<ItemListing> GetPriceListing(HashSet<int> itemIDs) => NetworkManager.GetLargeRequest<ItemListing>(new List<int>(itemIDs), "commerce/prices");
    }
}
