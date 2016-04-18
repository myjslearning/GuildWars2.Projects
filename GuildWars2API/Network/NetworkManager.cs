using System.Collections.Generic;

namespace GuildWars2API.Network
{
    public static class NetworkManager
    {
        private const int MAX_ITEMS_PER_REQUEST = 100;

        public static T UnauthorizedRequest<T>(string url) => UnauthorizedRequest<T>(new WebRequestHandler(), url);
        
        private static T UnauthorizedRequest<T>(IWebHandler handler, string url) => handler.GetRequest<T>(url, null);
        
        public static T AuthorizedRequest<T>(string url, string apiKey) => AuthorizedRequest<T>(new WebRequestHandler(), url, apiKey);

        private static T AuthorizedRequest<T>(IWebHandler handler, string url, string apiKey) => handler.GetRequest<T>(url, new Dictionary<string, string>() { { "Authorization", string.Format("Bearer {0}", apiKey) } });

        public static List<T> LargeRequest<T>(HashSet<int> IDs, string APIEndpoint, int itemsPerRequest = MAX_ITEMS_PER_REQUEST, string APIKey = null) {
            List<int> ids = new List<int>(IDs);
            List<T> results = new List<T>();
            if(ids.Count <= 0) {
                return results;
            }

            //Devide collection in smaller parts
            if(ids.Count > itemsPerRequest) {
                List<int> selected = new List<int>();

                selected.AddRange(ids.GetRange(itemsPerRequest, ids.Count - itemsPerRequest));
                ids.RemoveRange(itemsPerRequest, ids.Count - itemsPerRequest);

                results.AddRange(LargeRequest<T>(new HashSet<int>(selected), APIEndpoint));
            }

            //Authorized or Unauthorized Request
            List<T> response;
            if(APIKey == null) {
                response = UnauthorizedRequest<List<T>>(URLBuilder.LargeRequestURL(APIEndpoint, new HashSet<int>(ids)));
            }
            else {
                response = AuthorizedRequest<List<T>>(URLBuilder.LargeRequestURL(APIEndpoint, new HashSet<int>(ids)), APIKey);
            }

            //Read response
            if(response.Count > 0) {
                results.AddRange(response);
            }
            return results;
        }
    }
}

/*
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

    */