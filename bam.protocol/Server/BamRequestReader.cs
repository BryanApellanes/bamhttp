using System.Text;
using Bam.Net;

namespace Bam.Protocol.Server;

public class BamRequestReader : IBamRequestReader
{
    public BamRequestReader(BamRequestReaderOptions options)
    {
        this.Options = options;
    }
    
    protected BamRequestReaderOptions Options { get; set; }

    public int BufferSize => Options.RequestBufferSize;

    public virtual IBamRequest ReadRequest(Stream stream)
    {
        BamRequestLine line = ReadRequestLine(stream);
        BamRequest bamRequest = new BamRequest(line)
        {
            Headers = ReadHeaders(stream),
            Content = ReadContentString(stream)
        };

        // TODO: ensure other request properties are set
        
        return bamRequest;
    }

    protected BamRequestLine ReadRequestLine(Stream stream)
    {
        return new BamRequestLine(ReadLineString(stream));
    }
    
    protected Dictionary<string, string> ReadHeaders(Stream stream)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        string line = ReadLineString(stream);
        while (!string.IsNullOrEmpty(line))
        {
            string[] split = line.DelimitSplit(":");
            if (split.Length == 2)
            {
                headers.Add(split[0].ToLowerInvariant(), split[1]);
            }

            line = ReadLineString(stream);
        }

        return headers;
    }
    
    protected string ReadLineString(Stream stream, Encoding encoding = null)
    {
        encoding = encoding ?? Encoding.ASCII;
        byte[] line = ReadLine(stream);
        return encoding.GetString(line).Trim();
    }

    protected byte[] ReadLine(Stream stream)
    {
        byte[] buffer = new byte[BufferSize];
        int totalBytesRead = 0;
        int bytesRead = 0;
        do
        {
            bytesRead = stream.Read(buffer, totalBytesRead, 1);
            byte[] currentBuffer = buffer.Trim();
            if (currentBuffer.TailEquals("\r\n"))
            {
                break;
            }

            totalBytesRead += bytesRead;
        } while (bytesRead > 0);

        return buffer.Trim();
    }

    protected string ReadContentString(Stream stream, Encoding encoding = null)
    {
        encoding = encoding ?? Encoding.ASCII;
        byte[] content = ReadContent(stream);
        return encoding.GetString(content).Trim();
    }
    
    protected byte[] ReadContent(Stream stream)
    {
        byte[] buffer = new byte[BufferSize];
        int totalBytesRead = 0;
        int bytesRead = 0;
        do
        {
            /*if (totalBytesRead == BufferSize)
            {
                byte[] newBuffer = new byte[buffer.Length + BufferSize];
                buffer.CopyTo(newBuffer, 0);
                buffer = newBuffer;
                totalBytesRead = 0;
            }*/

            bytesRead = stream.Read(buffer, totalBytesRead, 1);
            totalBytesRead += bytesRead;
        } while (bytesRead > 0);

        return buffer.Trim();
    }
}