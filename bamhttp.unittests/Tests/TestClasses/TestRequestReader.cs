using Bam.Protocol.Server;

namespace Bam.Protocol.Tests;

public class TestBamRequestReader : BamRequestReader
{
    public TestBamRequestReader(BamRequestReaderOptions options) : base(options)
    {
    }
    
    public byte[] ReadLineAccessor(Stream stream)
    {
        return base.ReadLine(stream);
    }

    public string ReadStringLineAccessor(Stream stream)
    {
        return base.ReadLineString(stream);
    }
}