namespace Bam.Protocol.Client;

public abstract class BamClientRequestBuilder : IBamClientRequestBuilder
{
    private readonly BamClientRequestOptions _options;
    public BamClientRequestBuilder()
    {
        this._options = new BamClientRequestOptions();
    }
    
    
    public IBamClientRequestBuilder Host(string host)
    {
        _options.Host = host;
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

    public IBamClientRequestBuilder Content(string content)
    {
        _options.Content = content;
        return this;
    }

    public abstract IBamClientRequest Build();
}