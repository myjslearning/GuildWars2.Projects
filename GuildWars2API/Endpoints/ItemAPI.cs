using GuildWars2API.Model.Commerce;
using GuildWars2API.Model.Items;
using GuildWars2API.Network;
using System.Collections.Generic;
using System.Linq;
using static GuildWars2API.Network.NetworkManager;
using static Newtonsoft.Json.JsonConvert;

namespace GuildWars2API
{
    public static class ItemAPI {
        public static List<Item> SearchItem(string itemName) {
            string response = UnauthorizedRequest(URLBuilder.GetItemByName(itemName));
            if(response.Length > 0) {
                List<ItemSearch> itemsFound = DeserializeObject<List<ItemSearch>>(response);

                HashSet<int> itemIDs = new HashSet<int>();
                itemsFound.ForEach(i => itemIDs.Add(i.ItemID));

                return GetItem(itemIDs);
            }
            return null;
        }

        public static Item GetItem(int itemID) {
            string response = UnauthorizedRequest(URLBuilder.GetItemByID(itemID));
            if(response.Length > 0) {
                return DeserializeObject<Item>(response);
            }
            return null;
        }

        public static List<Item> GetItem(List<ItemStack> items) => GetItem(new HashSet<int>(items.Select(i => i.ID)));

        public static List<Item> GetItem(HashSet<int> itemIDs) => GetLargeRequest<Item>(new List<int>(itemIDs), "items");

        public static ItemListing GetPriceListing(int itemID) {
            string response = UnauthorizedRequest(URLBuilder.GetItemListing(itemID));
            if(response.Length > 0) {
                return DeserializeObject<ItemListing>(response);
            }
            return null;
        }

        public static List<ItemListing> GetPriceListing(HashSet<int> itemIDs) => GetLargeRequest<ItemListing>(new List<int>(itemIDs), "commerce/prices");
    }
}
