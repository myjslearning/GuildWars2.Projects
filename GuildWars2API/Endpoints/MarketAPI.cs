using GuildWars2API.Model.Commerce;
using GuildWars2API.Network;
using System.Collections.Generic;

using static GuildWars2API.Network.NetworkManager;

namespace GuildWars2API
{
    public static class MarketAPI
    {
#pragma warning disable CSE0003 
        public static GemExchange GetGemToGoldConversion() {
            return UnauthorizedRequest<GemExchange>(URLBuilder.GetGemToGoldConversion);
        }

        public static GemExchange GetGoldToGemConversion() {
            return UnauthorizedRequest<GemExchange>(URLBuilder.GetGoldToGemConversion);
        }

        public static List<WalletCurrencyInfo> GetCurrencyInfo() {
            return UnauthorizedRequest<List<WalletCurrencyInfo>>(URLBuilder.GetCurrencies());
        }
#pragma warning restore CSE0003
    }
}
