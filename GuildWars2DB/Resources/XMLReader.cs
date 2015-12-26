using System.Collections.Generic;
using System.Xml;

namespace GuildWars2DB.Resources
{
    internal class XMLReader
    {
        public static List<Dictionary<string, object>> ReadWorldBosses() {
            XmlDocument xml = new XmlDocument();
            List<Dictionary<string, object>> entries = new List<Dictionary<string, object>>();
            //return entries;

            xml.LoadXml(Properties.Resources.WorldBosses);
            foreach(XmlNode node in xml.SelectNodes("ArrayOfDisplayWorldBoss")[0].ChildNodes) {
                Dictionary<string, object> row = new Dictionary<string, object>();
                row.Add("Name", node["Name"].InnerText);
                row.Add("Description", node["Description"].InnerText);
                row.Add("Waypoint", node["Waypoint"].InnerText);
                row.Add("EventID", node["EventID"].InnerText);
                row.Add("Level", node["Level"].InnerText);
                row.Add("IsDone", node["IsDone"].InnerText);
                row.Add("Times", ReadTimes(node["Times"]));
                row.Add("DragoniteLootMinimum", node["DragoniteLootMinimum"].InnerText);
                row.Add("DragoniteLootMaximum", node["DragoniteLootMaximum"].InnerText);
                row.Add("ItemLoot", node["ItemLoot"].InnerText);
                row.Add("BoxesLoot", node["BoxesLoot"].InnerText);
                row.Add("IsTracking", node["IsTracking"].InnerText);
                entries.Add(row);
            }
            return entries;
        }

        private static List<string> ReadTimes(XmlNode node) {
            List<string> times = new List<string>();
            foreach(XmlNode childeNode in node.ChildNodes) {
                times.Add(childeNode.InnerText);
            }
            return times;
        }
    }
}