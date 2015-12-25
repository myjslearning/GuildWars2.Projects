using System.Windows.Media;

namespace GuildWars2.Other
{
    public class ColorManager
    {
        public static Color GreenToRed(int rangeValue, int rangeStart = 0, int rangeEnd = 80) {
            if(rangeStart >= rangeEnd) {
                return Colors.Black;
            }

            int max = rangeEnd - rangeStart;
            int value = rangeValue - rangeStart;

            int red = (255 * value) / max;
            int green = 255 - red;

            return Color.FromRgb((byte)red, (byte)green, 0);
        }

        public static string GetItemColor(string type) {
            if(type.Equals("Junk")) {
                return "#FFA6A6A6";
            }
            else if(type.Equals("Basic")) {
                return "#FFFFFF";
            }
            else if(type.Equals("Fine")) {
                return "#4099ff";
            }
            else if(type.Equals("Masterwork")) {
                return "#669900";
            }
            else if(type.Equals("Rare")) {
                return "#FFFF00";
            }
            else if(type.Equals("Exotic")) {
                return "#FF9933";
            }
            else if(type.Equals("Ascended")) {
                return "#FF3399";
            }
            else if(type.Equals("Legendary")) {
                return "#CC00CC";
            }
            else {
                return "#FFFFFF";
            }
        }
    }
}