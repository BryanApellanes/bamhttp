using System.Text;
using Bam.Net;
using Bam.Net.Testing.Unit;
using Bam.Protocol.Server;

namespace Bam.Protocol.Tests;

public class RequestReaderShould
{
    [UnitTest]
    public void ReadLineFromStream()
    {
        MemoryStream stream = new MemoryStream();
        string firstLine = "this is the first line";
        string secondLine = "this is the second line";
        string multiLineValue = $@"{firstLine}
{secondLine}";
        stream.Write(Encoding.ASCII.GetBytes(multiLineValue), 0, multiLineValue.Length);
        TestBamRequestReader reader = new TestBamRequestReader(new BamRequestReaderOptions(new BamServerOptions()));
        stream.Seek(0, SeekOrigin.Begin);
        string line = reader.ReadStringLineAccessor(stream);
        
        line.Trim().ShouldBeEqualTo(firstLine);
    }

    [UnitTest]
    public void ReadRequestFromStream()
    {
        string requestBody = @"This is the request body";
        string requestStream = $@"POST bam://test.com/file/path BAM/2.0
Content-Type: {MediaTypes.BamPipeline}
Accept: {MediaTypes.Json}
X-Bam-Test: another header value

{requestBody}
";
        MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(requestStream));

        TestBamRequestReader reader = new TestBamRequestReader(new BamRequestReaderOptions(new BamServerOptions()));
        IBamRequest bamRequest = reader.ReadRequest(stream);
        
        bamRequest.Content.ShouldBeEqualTo(requestBody);
        bamRequest.Headers.ContainsKey("content-type").ShouldBeTrue();
        bamRequest.Headers.ContainsKey("accept").ShouldBeTrue();
    }
}