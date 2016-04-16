using GuildWars2.Classes;
using GuildWars2API.Model.Items;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GuildWars2.Model
{
    internal class DisplayItem : Item, INotifyPropertyChanged
    {
        public string RarityColor => ColorManager.GetItemColor(Rarity);

        public string LevelColor => ColorManager.GreenToRed(Level, 0, 80).ToString();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}