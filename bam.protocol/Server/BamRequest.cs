using System.Net;
using System.Text;

namespace Bam.Protocol.Server;

public class BamRequest : IBamRequest
{
    public BamRequest()
    {
    }

    public BamRequest(BamRequestLine line)
    {
        this.Line = line;
        this.HttpMethod = line.Method;
        this.Url = new Uri(line.RequestUri);
        this.ProtocolVersion = line.ProtocolVersion;
    }
    internal BamRequestLine Line { get; set; }
    public string ProtocolVersion { get; internal set; }
    public string Content { get; set; }
    public string[] AcceptTypes { get; set; }
    public Encoding ContentEncoding { get; set; }
    public long ContentLength64 { get; internal set;}
    public Dictionary<string, string> QueryString { get; internal set;}
    public string ContentType { get; internal set;}
    public CookieCollection Cookies { get; internal set;}
    public Dictionary<string, string> Headers { get; internal set; }
    public HttpMethods HttpMethod { get; internal set; }
    public Uri Url { get; internal set; }

    public Uri UrlReferrer => Headers.ContainsKey("referer") ? new Uri(Headers["referrer"]) : null;
    
    public string UserAgent => Headers.ContainsKey("user-agent") ? Headers["user-agent"] : string.Empty;
    
    public string UserHostAddress { get; internal set;}
    
    public string UserHostName { get; internal set;}
    public string[] UserLanguages { get; internal set;}
    public string RawUrl { get; internal set;}
}