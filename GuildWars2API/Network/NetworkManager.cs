using System;
using System.Net;

namespace GuildWars2API.Network
{
    public static class NetworkManager
    {
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
    }
}