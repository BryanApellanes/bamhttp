using System.Net;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Server;
using Bam.Protocol;
using Bam.Protocol.Server;

namespace Bam.CommandlineOptions;

public class Experimental : CommandLineTool
{
    [ConsoleAction]
    public void Run()
    {
        string host = "test";
        string address = "https://test:80/";
        HostBinding hostBinding = new HostBinding(host);
        Uri uri = new Uri(hostBinding.ToString());
        Message.PrintLine(hostBinding.ToString());
        Message.PrintLine(uri.ToString());
    }
}