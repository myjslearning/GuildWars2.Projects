using GuildWars2Web.Classes;
using System;
using System.Collections.Generic;

namespace GuildWars2Web.Models
{
    public class Profile
    {
        public int ID { get; set; }

        public string Role { get; set; }

        public AuthRoles AuthRole { private get; set; }

        public int Level { get; set; }
        
        public string Rank { get; set; }
        
        public string Username { get; set; }
        
        public string Avatar { get; set; }
        
        public DateTime SubDate { get; set; }

        public Profile() { }

        public Profile(Dictionary<string, object> dictionary) {
            Role = TryGetValue<string>(dictionary, "Role");
            Level = TryGetValue<int>(dictionary, "Level");
            Rank = TryGetValue<string>(dictionary, "Rank");
            Username = TryGetValue<string>(dictionary, "Username");
            Avatar = TryGetValue<string>(dictionary, "Avatar");
            SubDate = TryGetValue<DateTime>(dictionary, "SubDate");

            AuthRole = Authorization.ConvertRole(TryGetValue<int>(dictionary, "Authrole"));
        }

        private T TryGetValue<T>(Dictionary<string, object> dictionary, string key) {
            if(dictionary.ContainsKey(key)) {
                object value = dictionary[key];
                if(value.GetType() == typeof(T)) {
                    return (T)value;
                }
            }
            return default(T);
        }
    }
}
