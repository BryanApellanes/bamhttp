namespace Bam.Protocol.Client;

public class UdpBamClientRequestBuilder : BamClientRequestBuilder
{
    public UdpBamClientRequestBuilder() : base()
    {
        _options.Host = new BamHostBinding();
        _options.Method = HttpMethods.PUT;
    }
    
    public override IBamClientRequest Build()
    {
        return new UdpClientRequest(_options.Content)
        {
            Host = _options.Host,
            Path = _options.Path,
            QueryString = _options.GetQueryString(),
            HttpMethod = _options.Method
        };
    }
}