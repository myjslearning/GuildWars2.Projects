using System;

namespace GuildWars2.Classes.Logger
{
    interface ILogger
    {
        void LogMessage(string message);
        void LogException(Exception ex, string message);
    }
}
