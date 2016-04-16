﻿using System;
using System.IO;
using System.Media;

namespace GuildWars2.Classes
{
    [Obsolete]
    internal class SoundManager
    {
        /// <summary>
        /// Starts the procedure of playing a notification
        /// </summary>
        /// <param name="type"></param>
        public static void PlaySound(SoundType type) {
            var player = new SoundPlayer();
            player.Stream = GetResourceStream(type);
            player.Play();
        }

        private static UnmanagedMemoryStream GetResourceStream(SoundType type) {
            if(type == SoundType.WorldBoss) {
                return Properties.Resources.NotificationWorldBoss;
            }
            else if(type == SoundType.TradePost) {
                //return Properties.Resources.NotificationTradePost;
            }
            return null;
        }
    }

    [Obsolete]
    public enum SoundType
    {
        WorldBoss,
        TradePost
    }
}