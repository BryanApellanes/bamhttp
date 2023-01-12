using System.Net;
using System.Text;
using System.Linq;

namespace Bam.Protocol.Server;

public class BamResponse : IBamResponse
{
    public BamResponse(Stream outputStream, int statusCode = 404)
    {
        OutputStream = outputStream;
        ContentEncoding = Encoding.UTF8;
        Cookies = new CookieCollection();
        Headers = new Dictionary<string, List<BamHeaderValue>>();
        statusCode = statusCode;
    }
    public Encoding ContentEncoding { get; set; }
    public long ContentLength64 { get; set; }
    public string ContentType { get; set; }
    public CookieCollection Cookies { get; set; }
    public Dictionary<string, List<BamHeaderValue>> Headers { get; set; }
    public Stream OutputStream { get; }
    public string RedirectLocation { get; set; }
    public int StatusCode { get; set; }

    public void SetHeader(string name, string value)
    {
        if (this.Headers.ContainsKey(name))
        {
            this.Headers[name] = new System.Collections.Generic.List<BamHeaderValue>();
        }
        
        this.AddHeader(name, value);
    }
    
    public void AddHeader(string name, string value)
    {
        if (!this.Headers.ContainsKey(name))
        {
            this.Headers.Add(name, new System.Collections.Generic.List<BamHeaderValue>());
        }

        this.Headers[name].Add(new BamHeaderValue { Name = name, Value = value });
    }

    public void AppendCookie(Cookie cookie)
    {
        this.Cookies.Add(cookie);
    }

    public void AppendHeader(string name, string value)
    {
        AddHeader(name, value);
    }

    public void Send()
    {
        throw new NotImplementedException();
    }

    public void Send(byte[] responseEntity)
    {
        throw new NotImplementedException();
    }

    public void Redirect(string url)
    {
        this.RedirectLocation = url;
        this.StatusCode = 302;
    }

    public void SetCookie(Cookie cookie)
    {
        Cookie existing = Cookies.FirstOrDefault(c => c.Name.Equals(cookie.Name));
        if (existing != null)
        {
            Cookies.Remove(existing);
        }

        Cookies.Add(cookie);
    }
}