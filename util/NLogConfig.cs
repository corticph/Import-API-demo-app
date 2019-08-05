using NLog;

namespace ImportAPIClient.Util
{
    public static class NLogConfig
    {
        public static void ConfigureToConsole()
        {
            var config = new NLog.Config.LoggingConfiguration();
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole);
            LogManager.Configuration = config;
        }
    }
}
