using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Protocol;

namespace Bam.CommandlineOptions;

public class Experimental : CommandLineTool
{
    [ConsoleAction]
    public void Run()
    {
        BamHttpServer bamHttpServer = new BamHttpServer();
        
    }
}