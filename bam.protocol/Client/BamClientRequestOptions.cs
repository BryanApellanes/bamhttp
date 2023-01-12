using System.Text;
using Bam.Net.Server;

namespace Bam.Protocol.Client;

public class BamClientRequestOptions
{
    public BamClientRequestOptions()
    {
        QueryString = new List<KeyValuePair<string, object>>();
        Host = new HostBinding();
        Method = HttpMethods.GET;
    }
    public HostBinding Host { get; set; }
    public string Path { get; set; }
    public IEnumerable<KeyValuePair<string, object>> QueryString { get; set; }
    public HttpMethods Method { get; set; }
    public object Content { get; set; }

    public string GetQueryString()
    {
        List<string> parameters = new List<string>();
        foreach (KeyValuePair<string, object> kvp in QueryString)
        {
            parameters.Add($"{kvp.Key}={kvp.Value}");
        }

        return string.Join(",", parameters);
    }
}