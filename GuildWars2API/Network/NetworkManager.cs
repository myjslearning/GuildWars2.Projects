using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace GuildWars2API.Network
{
    public static class NetworkManager
    {
        private const int MAX_ITEMS_PER_REQUEST = 50;

        /// <summary>
        /// Reads the world wide web
        /// </summary>
        /// <param name="url"></param>
        /// <returns>Response string or an empty string if nothing is found/error occurred</returns>
        public static string UnauthorizedRequest(string url) => GetResponse(url);

        public static string AuthorizedRequest(string url, string apiKey) => GetResponse(url, apiKey);

        private static string GetResponse(string url, string apiKey = null) {
            try {
                using(WebClient client = new WebClient()) {
                    if(apiKey != null) {
                        client.Headers.Add("Authorization", $"Bearer {apiKey}");
                    }

                    Console.WriteLine("[INFO] Loading... " + url);
                    DateTime start = DateTime.Now;

                    string result = client.DownloadString(url);

                    Console.WriteLine("[INFO] Response Time " + (DateTime.Now - start).TotalSeconds + "sec");

                    return result;
                }
            }
            catch(Exception ex) {
                Console.WriteLine("[ERROR] " + ex.Message);
                return string.Empty;
            }
        }

        public static List<T> GetLargeRequest<T>(List<int> IDs, string apiCategory, string APIKey = null) {
            List<T> results = new List<T>();
            if(IDs.Count <= 0) {
                return results;
            }

            //Devide collection in smaller parts
            if(IDs.Count > MAX_ITEMS_PER_REQUEST) {
                List<int> selected = new List<int>();

                selected.AddRange(IDs.GetRange(MAX_ITEMS_PER_REQUEST, IDs.Count - MAX_ITEMS_PER_REQUEST));
                IDs.RemoveRange(MAX_ITEMS_PER_REQUEST, IDs.Count - MAX_ITEMS_PER_REQUEST);

                results.AddRange(GetLargeRequest<T>(selected, apiCategory));
            }

            //Authorized or Unauthorized Request
            string response = "";
            if(APIKey == null) {
                response = UnauthorizedRequest(URLBuilder.LargeRequestURL(apiCategory, IDs));
            }
            else {
                response = AuthorizedRequest(URLBuilder.LargeRequestURL(apiCategory, IDs), APIKey);
            }

            //Read response
            if(response.Length > 0) {
                results.AddRange(JsonConvert.DeserializeObject<List<T>>(response));
            }
            return results;
        }
    }
}