using Bam.Net.Server;
using Bam.Protocol.Server;

namespace Bam.Protocol.Client;

public class HttpBamClientRequestBuilder : BamClientRequestBuilder
{
    public override IBamClientRequest Build()
    {
        return new HttpClientRequest()
        {
            Host = _options.Host,
            Path = _options.Path,
            QueryString = _options.GetQueryString(),
            HttpMethod = _options.Method,
            Content = _options.Content
        };
    }
}