using System.Collections.Generic;

namespace GuildWars2.Other.Sound
{
    internal class SoundDictionary
    {
        private Dictionary<Notification, string> _notificationDictionary;

        public Dictionary<Notification, string> NotificationDictionary {
            get {
                if(_notificationDictionary == null) {
                    InitializeDictionary();
                }
                return _notificationDictionary;
            }
            set {
                _notificationDictionary = value;
            }
        }

        /// <summary>
        /// Initializes hte dictionary
        /// </summary>
        private void InitializeDictionary() {
            Dictionary<Notification, string> dictionary = new Dictionary<Notification, string>();
            dictionary.Add(Notification.WorldBoss, @"\Resources\Sounds\Notification1.wav");
            _notificationDictionary = dictionary;
        }
    }
}