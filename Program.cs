using CommandLine;

namespace ImportAPIClient
{
    class Program
    {
        public class Options
        {
            [Option('s', "HTTPS", Required = false, Default = true,
              HelpText = "Pass true if the API server scheme is https.")]
            public bool? IsHttps { get; set; }

            [Option('h', "host", Required = true,
              HelpText = "API server host")]
            public string Host { get; set; }

            [Option('p', "port", Required = true,
              HelpText = "API server port")]
            public int Port { get; set; }

            [Option('t', "token", Required = true,
              HelpText = "API server authorization token")]
            public string Token { get; set; }

            [Option('v', "version", Required = false, Default = "v1.0",
              HelpText = "API version")]
            public string Version { get; set; }
        }

        static void Main(string[] args)
        {
            Util.NLogConfig.ConfigureToConsole();

            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(RunOptionsAndReturnExitCode);
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            Client.ImportAPIClient client = new Client.ImportAPIClient(opts.Host, opts.Port, opts.Token, opts.IsHttps == true, opts.Version);
            Manager.ImportManager importManager = new Manager.ImportManager(new Repo.MockedCaseRepo(), client);

            importManager.ImportAllCasesAndEvents();
        }
    }
}