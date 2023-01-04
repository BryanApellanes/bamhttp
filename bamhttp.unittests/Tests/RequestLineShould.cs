using Bam.Net;
using Bam.Net.Testing.Unit;
using Bam.Protocol.Server;

namespace Bam.Protocol.Tests;

public class RequestLineShould
{
    [UnitTest]
    public void ParseInputData()
    {
        string method = "POST";
        string uri = "/path/of/file";
        string protocolVersion = "BAM/2.0";
        string line = $"{method} {uri} {protocolVersion}";
        BamRequestLine bamRequestLine = new BamRequestLine(line);

        bamRequestLine.Method.ShouldBeEqualTo(HttpMethods.POST);
        bamRequestLine.RequestUri.ShouldBeEqualTo(uri);
        
        
        bamRequestLine.ProtocolVersion.ShouldBeEqualTo(protocolVersion);
    }
}