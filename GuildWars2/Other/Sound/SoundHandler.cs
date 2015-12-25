using System.IO;
using System.Media;

namespace GuildWars2.Other.Sound
{
    internal class SoundHandler
    {
        /// <summary>
        /// Starts the procedure of playing a notification
        /// </summary>
        /// <param name="type"></param>
        public static void PlayNotification(Notification type) {
            SoundPlayer player = new SoundPlayer();
            player.Stream = GetResourceStream(type);
            player.Play();
        }

        private static UnmanagedMemoryStream GetResourceStream(Notification type) {
            if(type == Notification.WorldBoss) {
                return Properties.Resources.NotificationWorldBoss;
            }
            else if(type == Notification.TradePost) {
                //return Properties.Resources.NotificationTradePost;
            }
            return null;
        }
    }

    public enum Notification
    {
        WorldBoss,
        TradePost
    }
}