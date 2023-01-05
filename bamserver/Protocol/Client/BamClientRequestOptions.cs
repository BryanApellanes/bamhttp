namespace Bam.Protocol.Client;

public class BamClientRequestOptions
{
    public string Host { get; set; }
    public string Path { get; set; }
    public IEnumerable<KeyValuePair<string, object>> QueryString { get; set; }
    public HttpMethods Method { get; set; }
    public string Content { get; set; }
}