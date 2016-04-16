using System;

namespace GuildWars2.Classes.Logger
{
    class ConsoleLogger : ILogger
    {
        public void LogMessage(string message) {
            Console.WriteLine("[MESSAGE]" + message);
        }

        public void LogException(Exception ex, string message) {
            Console.WriteLine("[ERROR]" + message + " | " + ex.Message);
            Console.WriteLine("[ERROR]" + ex.ToString());
        }
    }
}
