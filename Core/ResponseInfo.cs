using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HarHar
{
    /// <summary>
    /// This object contains detailed info about the response.
    /// </summary>
    [DataContract]
    public class ResponseInfo
    {
        public ResponseInfo()
        {
            HeadersSize = -1;
            BodySize = -1;

            StatusText = string.Empty;
            Cookies = new List<CookieInfo>();
            Headers = new List<NameValuePairInfo>();
            RedirectUrl = string.Empty;
            Content = new ContentInfo();
        }

        /// <summary>
        /// status [number] - Response status.
        /// </summary>
        [DataMember(Name = "status")]
        public int Status { get; set; }

        /// <summary>
        /// statusText [string] - Response status description.
        /// </summary>
        [DataMember(Name = "statusText")]
        public string StatusText { get; set; }

        /// <summary>
        /// httpVersion [string] - Response HTTP Version.
        /// </summary>
        [DataMember(Name = "httpVersion")]
        public string HttpVersion { get; set; }

        /// <summary>
        /// cookies [array] - List of cookie objects.
        /// 
        /// This object contains list of all cookies (used in <request> and <response> objects).
        /// </summary>
        [DataMember(Name = "cookies")]
        public IList<CookieInfo> Cookies { get; set; }

        /// <summary>
        /// headers [array] - List of header objects.
        /// 
        /// This object contains list of all headers (used in <request> and <response> objects).
        /// </summary>
        [DataMember(Name = "headers")]
        public IList<NameValuePairInfo> Headers { get; set; }

        /// <summary>
        /// content [object] - Details about the response body.
        /// </summary>
        [DataMember(Name = "content")]
        public ContentInfo Content { get; set; }

        /// <summary>
        /// redirectURL [string] - Redirection target URL from the Location response header.
        /// </summary>
        [DataMember(Name = "redirectURL")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// headersSize [number]* - Total number of bytes from the start of the HTTP 
        /// response message until (and including) the double CRLF before the body. 
        /// Set to -1 if the info is not available.
        /// 
        ///  The size of received response-headers is computed only from headers that 
        /// are really received from the server. Additional headers appended by the 
        /// browser are not included in this number, but they appear in the list of 
        /// header objects.
        /// 
        /// 
        /// The total response size received can be computed as follows (if both 
        /// values are available):
        /// 
        /// var totalSize = entry.response.headersSize + entry.response.bodySize;
        /// </summary>
        [DataMember(Name = "headersSize")]
        public int HeadersSize { get; set; }

        /// <summary>
        /// bodySize [number] - Size of the received response body in bytes. Set to 
        /// zero in case of responses coming from the cache (304). Set to -1 if the 
        /// info is not available.
        /// 
        /// 
        /// The total response size received can be computed as follows (if both 
        /// values are available):
        /// 
        /// var totalSize = entry.response.headersSize + entry.response.bodySize;
        /// </summary>
        [DataMember(Name = "bodySize")]
        public long BodySize { get; set; }

        /// <summary>
        /// comment [string, optional] (new in 1.2) - A comment provided by the user 
        /// or the application.
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }
    }
}