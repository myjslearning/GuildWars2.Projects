using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace GuildWars2API.Network
{
    public static class NetworkManager
    {
        private const int MAX_ITEMS_PER_REQUEST = 100;

        /// <summary>
        /// Reads the world wide web
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Response string or an empty string if nothing is found/error occurred</returns>
        public static string UnauthorizedRequest(string url) => GetResponse(url);

        public static string AuthorizedRequest(string url, string apiKey) => GetResponse(url, apiKey);

        public static List<T> GetLargeRequest<T>(List<int> IDs, string APIEndpoint, int itemsPerRequest = MAX_ITEMS_PER_REQUEST, string APIKey = null) {
            List<T> results = new List<T>();
            if(IDs.Count <= 0) {
                return results;
            }

            //Devide collection in smaller parts
            if(IDs.Count > itemsPerRequest) {
                List<int> selected = new List<int>();

                selected.AddRange(IDs.GetRange(itemsPerRequest, IDs.Count - itemsPerRequest));
                IDs.RemoveRange(itemsPerRequest, IDs.Count - itemsPerRequest);

                results.AddRange(GetLargeRequest<T>(selected, APIEndpoint));
            }

            //Authorized or Unauthorized Request
            string response = "";
            if(APIKey == null) {
                response = UnauthorizedRequest(URLBuilder.LargeRequestURL(APIEndpoint, IDs));
            }
            else {
                response = AuthorizedRequest(URLBuilder.LargeRequestURL(APIEndpoint, IDs), APIKey);
            }

            //Read response
            if(response.Length > 0) {
                results.AddRange(JsonConvert.DeserializeObject<List<T>>(response));
            }
            return results;
        }
        
        private static string GetResponse(string url, string apiKey = null) {
            try {
                using(WebClient client = new WebClient()) {
                    if(apiKey != null) {
                        client.Headers.Add("Authorization", $"Bearer {apiKey}");
                    }
#if DEBUG
                    Console.WriteLine("[INFO] Loading... " + url);
                    DateTime start = DateTime.Now;
#endif
                    string result = client.DownloadString(url);
#if DEBUG
                    Console.WriteLine("[INFO] Response Time " + (DateTime.Now - start).TotalSeconds + "sec");
#endif
                    return result;
                }
            }
            catch(Exception ex) {
                Console.WriteLine("[ERROR] " + ex.Message);
                return string.Empty;
            }
        }
    }
}