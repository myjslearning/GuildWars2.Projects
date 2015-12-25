using System;

namespace GuildWars2.Other
{
    internal class ErrorHandler
    {
        public string Message { get; set; }

        public Exception ExceptionThrown { get; set; }

        public DateTime Date { get; set; }

        public ErrorHandler(Exception ex, string message, bool logError, bool closeApp) {
            Message = message;
            Date = DateTime.Now;
            ExceptionThrown = ex;

            if(logError) {
                Log();
            }

            if(closeApp) {
                Environment.Exit(1);
            }
        }

        private void Log() {
            Console.WriteLine("[ERROR]" + Message + " | " + ExceptionThrown.Message);
            //Console.WriteLine("[ERROR]" + ExceptionThrown.ToString());
        }
    }
}