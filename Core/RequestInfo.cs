using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HarHar
{
    /// <summary>
    /// This object contains detailed info about performed request.
    /// </summary>
    [DataContract]
    public class RequestInfo
    {
        public RequestInfo()
        {
            Cookies = new List<CookieInfo>();
            Headers = new List<NameValuePairInfo>();
            QueryString = new List<NameValuePairInfo>();
            HeadersSize = -1;
            BodySize = -1;
        }

        /// <summary>
        /// method [string] - Request method (GET, POST, ...).
        /// </summary>
        [DataMember(Name = "method")]
        public string Method { get; set; }

        /// <summary>
        /// url [string] - Absolute URL of the request (fragments are not included).
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// httpVersion [string] - Request HTTP Version.
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
        /// queryString [array] - List of query parameter objects.
        /// 
        /// This object contains list of all parameters & values parsed from a query string, if any (embedded in <request> object).
        /// 
        /// HAR format expects NVP (name-value pairs) formatting of the query string.
        /// </summary>
        [DataMember(Name = "queryString")]
        public IList<NameValuePairInfo> QueryString { get; set; }

        /// <summary>
        /// postData [object, optional] - Posted data info.
        /// </summary>
        [DataMember(Name = "postData")]
        public PostData PostData { get; set; }

        /// <summary>
        /// headersSize [number] - Total number of bytes from the start of the HTTP request 
        /// message until (and including) the double CRLF before the body. Set to -1 if the 
        /// info is not available.
        /// </summary>
        [DataMember(Name = "headersSize")]
        public int HeadersSize { get; set; }

        /// <summary>
        /// bodySize [number] - Size of the request body (POST data payload) in bytes. Set 
        /// to -1 if the info is not available.
        /// </summary>
        [DataMember(Name = "bodySize")]
        public long BodySize { get; set; }

        /// <summary>
        /// comment [string, optional] (new in 1.2) - A comment provided by the user or the 
        /// application.
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }
    }
}