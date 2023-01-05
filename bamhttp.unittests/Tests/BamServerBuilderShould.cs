using Bam.Net;
using Bam.Net.Testing.Unit;
using Bam.Protocol.Server;

namespace Bam.Protocol.Tests;

public class BamServerBuilderShould
{
    [UnitTest]
    public void BuildServer()
    {
        BamServer server = new BamServerBuilder().Build();
        server.ShouldNotBeNull();
    }
}