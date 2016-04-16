using System.Collections.Generic;

namespace GuildWars2API.Network
{
    public interface IWebHandler
    {
        T GetRequest<T>(string url, Dictionary<string, string> headers);
    }
}
