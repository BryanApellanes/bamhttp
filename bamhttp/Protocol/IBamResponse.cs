/*
	Copyright © Bryan Apellanes 2015  
*/

using System.Text;
using System.Net;

namespace Bam.Protocol
{
    public interface IBamResponse
    {
        /// <summary>
        /// Gets or sets the System.Text.Encoding for this response's OutputStream.
        /// </summary>
        Encoding ContentEncoding { get; set; }
        
        /// <summary>
        /// Gets or sets the number of bytes in the body data included in the response.
        /// </summary>
        long ContentLength64 { get; set; }
        
        /// <summary>
        /// Gets or sets the MIME type of the content returned.
        /// </summary>
        string ContentType { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of cookies returned with the response.
        /// </summary>
        CookieCollection Cookies { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of header name/value pairs returned by the server.
        /// </summary>
        Dictionary<string, List<BamHeaderValue>> Headers { get; set; }
        
        /// <summary>
        /// Gets a System.IO.Stream object to which a response can be written.
        /// </summary>
        Stream OutputStream { get; }
                
        /// <summary>
        /// Gets or sets the value of the HTTP Location header in this response.
        /// </summary>
        string RedirectLocation { get; set; }
        
        /// <summary>
        /// Gets or sets the HTTP status code to be returned to the client.
        /// </summary>
        int StatusCode { get; set; }

        void SetHeader(string name, string value);
        
        /// <summary>
        /// Adds the specified header and value to the HTTP headers for this response.
        /// </summary>
        /// <param name="name">The name of the HTTP header to set.</param>
        /// <param name="value">The value for the name header.</param>
        void AddHeader(string name, string value);
        
        /// <summary>
        /// Adds the specified System.Net.Cookie to the collection of cookies for this response.
        /// </summary>
        /// <param name="cookie">The System.Net.Cookie to add to the collection to be sent with this response</param>
        void AppendCookie(Cookie cookie);

        /// <summary>
        /// Appends a value to the specified HTTP header to be sent with this response.
        /// </summary>
        /// <param name="name">The name of the HTTP header to append value to.</param>
        /// <param name="value">The value to append to the name header.</param>
        void AppendHeader(string name, string value);
        
        /// <summary>
        /// Sends the response to the client.
        /// </summary>
        void Send();

        /// <summary>
        /// Returns the specified byte array to the client.
        /// </summary>
        /// <param name="responseEntity">A System.Byte array that contains the response to send to the client.</param>
        /// <param name="willBlock"></param>
        void Send(byte[] responseEntity);

        /// <summary>
        /// Configures the response to redirect the client to the specified URL.
        /// </summary>
        /// <param name="url">The URL that the client should use to locate the requested resource.</param>
        void Redirect(string url);

        /// <summary>
        /// Adds or updates a System.Net.Cookie in the collection of cookies sent with this response.
        /// </summary>
        /// <param name="cookie">A System.Net.Cookie for this response.</param>
        void SetCookie(Cookie cookie);
    }
}
