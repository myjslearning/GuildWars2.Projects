using System;
using System.Collections.Generic;

namespace GuildWars2API.Network
{
    class HttpRequestHandler : IWebHandler
    {
        public T GetRequest<T>(string url, Dictionary<string, string> headers) {
            throw new NotImplementedException();
        }
    }
}
