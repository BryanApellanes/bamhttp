using System.Text;
using Bam.Net;

namespace Bam.Protocol.Server;

public class BamRequestLine
{
    public BamRequestLine(byte[] requestLineData) :this(Encoding.ASCII.GetString(requestLineData))
    {
    }

    public BamRequestLine(string requestLine)
    {
        this.Value = requestLine;
    }

    private readonly string _value;
    public string Value
    {
        get => _value;
        private init
        {
            _value = value;
            Parse();
        }
    }
    
    public HttpMethods Method { get; private set; }
    
    public string RequestUri { get; private set; }
    
    public string ProtocolVersion { get; private set; }

    public override string ToString()
    {
        return Value;
    }

    private void Parse()
    {
        string[] split = Value.DelimitSplit(" ");
        if (split.Length != 3)
        {
            throw new InvalidOperationException($"Unrecognized request line: {_value}");
        }

        Method = Enum.Parse<HttpMethods>(split[0]);
        RequestUri = split[1];
        ProtocolVersion = split[2];
    }
}