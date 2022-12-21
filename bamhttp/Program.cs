using Bam.Net;

namespace Bam.Protocol
{
    [Serializable]
    public class Program : CommandLineTool
    {
        public static void Main(string[] args)
        {
            TryWritePid(false);
            AddConfigurationSwitches();
            ExecuteMainOrInteractive(args);
        }
    }
}