using System.Text;
using Bam.Net;
using Bam.Net.Testing.Unit;

namespace Bam.Protocol.Tests;

public class RequestReaderTests
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
        TestBamRequestReader reader = new TestBamRequestReader();
        stream.Seek(0, SeekOrigin.Begin);
        string line = reader.ReadStringLineAccessor(stream);
        
        line.Trim().ShouldBeEqualTo(firstLine);
    }

    [UnitTest]
    public void ReadRequestFromStream()
    {
        MemoryStream stream = new MemoryStream();
        string requestBody = @"This is the request body";
        string requestStream = $@"POST bam://test.com/file/path BAM/2.0
Content-Type: {MediaTypes.BamPipeline}
Accept: {MediaTypes.Json}
X-Bam-Test: another header value

{requestBody}
";
        stream.Write(Encoding.ASCII.GetBytes(requestStream));
        stream.Seek(0, SeekOrigin.Begin);

        TestBamRequestReader reader = new TestBamRequestReader();
        IBamRequest bamRequest = reader.ReadRequest(stream);
        
        bamRequest.Content.ShouldBeEqualTo(requestBody);
        bamRequest.Headers.ContainsKey("Content-Type").ShouldBeTrue();
        bamRequest.Headers.ContainsKey("Accept").ShouldBeTrue();
    }
}