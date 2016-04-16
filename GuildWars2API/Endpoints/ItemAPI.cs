using GuildWars2API.Model.Commerce;
using GuildWars2API.Model.Items;
using GuildWars2API.Network;
using System.Collections.Generic;
using System.Linq;
using static GuildWars2API.Network.NetworkManager;

namespace GuildWars2API
{
    public static class ItemAPI {
        public static List<Item> SearchItem(string itemName) {
            List<ItemSearch> itemsFound = UnauthorizedRequest<List<ItemSearch>>(URLBuilder.GetItemByName(itemName));
            if(itemsFound != null) {
                HashSet<int> itemIDs = new HashSet<int>();
                itemsFound.ForEach(i => itemIDs.Add(i.ItemID));

                return GetItem(itemIDs);
            }
            return null;
        }

#pragma warning disable CSE0003
        public static Item GetItem(int itemID) {
            return UnauthorizedRequest<Item>(URLBuilder.GetItemByID(itemID));
        }

        public static List<Item> GetItem(List<ItemStack> items) => GetItem(new HashSet<int>(items.Select(i => i.ID)));

        public static List<Item> GetItem(HashSet<int> itemIDs) => LargeRequest<Item>(itemIDs, "items");

        public static ItemListing GetItemListing(int itemID) {
            return UnauthorizedRequest<ItemListing>(URLBuilder.GetItemListing(itemID));
        }
#pragma warning restore CSE0003

        public static List<ItemListing> GetItemListing(HashSet<int> itemIDs) => LargeRequest<ItemListing>(itemIDs, "commerce/prices");

        public static bool IsPromotionItem(int itemID) {
            List<int> promotionItemIDs = new List<int>() {
                19743, 19745, 19741, 19748, 19739, 19732, 19728, 19729, 19730, 19731, 74724, 74798, 74084, 74825, 75123, 24307, 24308, 24310, 38014, 38024, 38023,
                75536, 76313, 76572, 76209, 76456, 75542, 76044, 76342, 70545, 70724, 73158, 70779, 71049, 70956, 73229, 70792, 72012, 70537, 70675, 71900, 72677,
                73415, 46733, 45884, 45885, 12244, 19701, 19703, 19697, 19698, 19702, 19699, 24304, 13243, 24324, 13248, 24339, 24329, 13256, 13244, 13245, 13257,
                13258, 13260, 13246, 19700, 13259, 13247, 24314, 13255, 24319, 24309, 19726, 19727, 19723, 19722, 19724, 19725, 19744, 19720, 46741, 19742, 19740,
                19746, 72845, 19747, 19738, 19735, 19733, 46739, 19736, 19734, 19737, 19685, 46743, 73537, 19686, 19681, 19688, 19684, 19680, 19683, 19687, 19679,
                75640, 19682, 46738, 46732, 19712, 19713, 19714, 19709, 19710, 19711, 46736, 24343, 24345, 24358, 24341, 24344, 24351, 24350, 24348, 24347, 24349,
                24275, 24273, 24276, 24274, 24277, 24356, 24357, 24354, 24353, 24355, 24288, 24289, 24285, 24287, 24286, 24363, 24297, 24300, 24299, 24298, 24280,
                24282, 24283, 24281, 24279, 24293, 24295, 24294, 24292, 24291, 24302, 24304, 24305, 24303, 24339, 24338, 24337, 24340, 24330, 24329, 24277, 24327,
                24328, 46683, 46682, 24325, 24323, 24324, 71653, 24322, 77190, 77082, 24335, 24334, 38024, 24331, 24332, 38014, 74544, 71163, 75801, 71852, 71856,
                72445, 71437, 71036, 74457, 70439, 73671, 70493, 76379, 72142, 71873, 75781, 72934, 75043, 71260, 72331, 73753, 70730, 72846, 72454, 72551, 73332,
                72337, 76380, 24333, 38023, 76405, 75818, 76806, 73381, 75504, 74643, 75769, 75899, 75939, 75215, 70658, 71979, 73375, 75228, 73111, 74378, 76157,
                72766, 24317, 24318, 24319, 24320, 24312, 24314, 24313, 24315, 24309, 24332, 24333, 24334, 24335
            };
            return promotionItemIDs.Contains(itemID);
        }
    }
}
