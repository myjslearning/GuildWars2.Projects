using System;

namespace GuildWars2.Classes.Logger
{
    static class LogManager
    {
        public static void LogMessage(string message, bool closeApp) {
#if DEBUG
            LogMessage(new ConsoleLogger(), message, closeApp);
#endif
#if RELEASE
            LogMessage(new FileLogger(), message, closeApp);
#endif
        }

        public static void LogException(Exception ex, string message, bool closeApp) {
#if DEBUG
            LogException(new ConsoleLogger(), ex, message, closeApp);
#endif
#if RELEASE
            LogException(new FileLogger(), ex, message, closeApp);
#endif
        }

        public static void LogMessage(ILogger logger, string message, bool closeApp) {
            logger.LogMessage(message);

            if(closeApp)
                CloseApplication();
        }

        public static void LogException(ILogger logger, Exception ex, string message, bool closeApp) {
            logger.LogException(ex, message);

            if(closeApp)
                CloseApplication();
        }

        private static void CloseApplication() {
            Environment.Exit(1);
        }
    }
}