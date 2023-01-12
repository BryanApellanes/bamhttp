using Bam.Net.Server;

namespace Bam.Protocol.Client;

public abstract class BamClientRequestBuilder : IBamClientRequestBuilder
{
    protected readonly BamClientRequestOptions _options;
    public BamClientRequestBuilder()
    {
        this._options = new BamClientRequestOptions();
    }
    
    public IBamClientRequestBuilder BaseAddress(string host)
    {
        return BaseAddress(new HostBinding(host));
    }

    public IBamClientRequestBuilder BaseAddress(HostBinding hostBinding)
    {
        _options.Host = hostBinding;
        return this;
    }

    public IBamClientRequestBuilder Path(string path)
    {
        _options.Path = path;
        return this;
    }

    public IBamClientRequestBuilder QueryString(IEnumerable<KeyValuePair<string, object>> queryString)
    {
        _options.QueryString = queryString;
        return this;
    }

    public IBamClientRequestBuilder QueryString(params KeyValuePair<string, object>[] queryString)
    {
        _options.QueryString = queryString;
        return this;
    }

    public IBamClientRequestBuilder HttpMethod(HttpMethods method)
    {
        _options.Method = method;
        return this;
    }

    public IBamClientRequestBuilder Content(object content)
    {
        _options.Content = content;
        return this;
    }

    public abstract IBamClientRequest Build();
}