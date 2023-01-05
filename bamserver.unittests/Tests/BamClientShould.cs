using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Testing.Unit;
using Bam.Protocol.Client;
using Bam.Protocol.Server;

namespace Bam.Protocol.Tests;

public class BamClientShould
{
    [UnitTest]
    public void ReceiveResponse()
    {
        BamServer server = new BamServer();
        BamServerInfo info = server.GetInfo();
        Message.PrintLine(info.ToJson(true), ConsoleColor.Cyan);
        server.Start();

        BamClient client = new BamClient();

        server.Stop();
    }
}