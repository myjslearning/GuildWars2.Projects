using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace GuildWars2API.Network
{
    class WebRequestHandler : IWebHandler
{
        public T GetRequest<T>(string url, Dictionary<string, string> headers) => ReadResponse<T>(CreateRequest(url, headers));

        private WebRequest CreateRequest(string url, Dictionary<string, string> headers) {
            var request = WebRequest.Create(url);
            if(headers != null) {
                foreach(KeyValuePair<string, string> header in headers) {
                    request.Headers[header.Key] = header.Value;
                }
            }
            return request;
        }

        private T ReadResponse<T>(WebRequest request) {
            try {
                using(var response = (HttpWebResponse)request.GetResponse()) {
                    using(Stream data = response.GetResponseStream()) {
                        using(var sr = new StreamReader(data)) {
                            return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                        }
                    }
                }
            }
            catch(WebException ex) {
                Console.WriteLine(GetWebExDetail(ex));
                return default(T);
            }
            catch(Exception) {
                return default(T);
            }
        }

        private string GetWebExDetail(WebException ex) {
            WebResponse errResp = ex.Response;
            if(errResp != null) {
                using(Stream respStream = errResp.GetResponseStream()) {
                    var reader = new StreamReader(respStream);
                    return reader.ReadToEnd();
                }
            }
            return null;
        }
    }
}
