using System.Net;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Protocol;
using Bam.Protocol.Server;

namespace Bam.CommandlineOptions;

public class Experimental : CommandLineTool
{
    [ConsoleAction]
    public void Run()
    {
        BamServerBuilder builder = new BamServerBuilder();
        /*BamProtocolServer bamProtocolServer = new BamProtocolServer();
        BamClient client = new BamClient()
        HttpRequestMessage requestMessage = new HttpRequestMessage();
        requestMessage.Headers.Set#1#*/
    }
}