using GuildWars2API.Model.Commerce;
using GuildWars2API.Network;
using System.Collections.Generic;

using static GuildWars2API.Network.NetworkManager;
using static Newtonsoft.Json.JsonConvert;

namespace GuildWars2API
{
    public static class MarketAPI
    {
        public static GemExchange GetGemToGoldConversion() {
            string response = UnauthorizedRequest(URLBuilder.GetGemToGoldConversion);
            if(response.Length > 0) {
                return DeserializeObject<GemExchange>(response);
            }
            return null;
        }

        public static GemExchange GetGoldToGemConversion() {
            string response = UnauthorizedRequest(URLBuilder.GetGoldToGemConversion);
            if(response.Length > 0) {
                return DeserializeObject<GemExchange>(response);
            }
            return null;
        }

        public static List<WalletCurrencyInfo> GetCurrencyInfo() {
            string response = UnauthorizedRequest(URLBuilder.GetCurrencies());
            if(response.Length > 0) {
                return DeserializeObject<List<WalletCurrencyInfo>>(response);
            }
            return null;
        }
    }
}
