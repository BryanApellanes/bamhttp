namespace Bam.Protocol.Client;

public class TcpBamClientRequestBuilder : BamClientRequestBuilder
{
    public TcpBamClientRequestBuilder() : base()
    {
        _options.Host = new BamHostBinding();
        _options.Method = HttpMethods.POST;
    }
    public override IBamClientRequest Build()
    {
        return new TcpClientRequest()
        {
            Host = _options.Host,
            Path = _options.Path,
            QueryString = _options.GetQueryString(),
            HttpMethod = _options.Method,
            Content = _options.Content
        };
    }
}