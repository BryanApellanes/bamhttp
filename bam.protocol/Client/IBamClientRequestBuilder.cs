using Bam.Net.Server;

namespace Bam.Protocol.Client;

public interface IBamClientRequestBuilder
{
    IBamClientRequestBuilder BaseAddress(string host);
    IBamClientRequestBuilder BaseAddress(HostBinding hostBinding);
    IBamClientRequestBuilder Path(string path);
    IBamClientRequestBuilder QueryString(IEnumerable<KeyValuePair<string, object>> queryString);
    IBamClientRequestBuilder QueryString(params KeyValuePair<string, object>[] queryString);
    IBamClientRequestBuilder HttpMethod(HttpMethods method);
    IBamClientRequestBuilder Content(object content);
    IBamClientRequest Build();
}