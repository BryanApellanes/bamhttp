namespace Bam.Protocol.Client;

public interface IBamClientRequestBuilder
{
    IBamClientRequestBuilder Host(string host);
    IBamClientRequestBuilder Path(string path);
    IBamClientRequestBuilder QueryString(IEnumerable<KeyValuePair<string, object>> queryString);
    IBamClientRequestBuilder QueryString(params KeyValuePair<string, object>[] queryString);
    IBamClientRequestBuilder HttpMethod(HttpMethods method);
    IBamClientRequestBuilder Content(string content);
    IBamClientRequest Build();
}