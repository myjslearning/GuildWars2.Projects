using GuildWars2Web.Classes;
using System;

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

        public string Serialize() => ID + ";" + AuthRole.ToString();
    }
}
