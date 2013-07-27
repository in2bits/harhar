using System.IO;
using System.Runtime.Serialization;

namespace HarHar
{
    /// <summary>
    /// This object describes details about response content (embedded in <response> object).
    /// </summary>
    [DataContract]
    public class ContentInfo
    {
        public ContentInfo()
        {
            MimeType = string.Empty;
        }

        /// <summary>
        /// size [number] - Length of the returned content in bytes. Should be equal to 
        /// response.bodySize if there is no compression and bigger when the content has 
        /// been compressed.
        /// </summary>
        [DataMember(Name = "size")]
        public long Size { get; set; }

        /// <summary>
        /// compression [number, optional] - Number of bytes saved. Leave out this field 
        /// if the information is not available.
        /// </summary>
        [DataMember(Name = "compression")]
        public long? Compression { get; set; }

        /// <summary>
        /// mimeType [string] - MIME type of the response text (value of the Content-Type 
        /// response header). The charset attribute of the MIME type is included (if available).
        /// </summary>
        [DataMember(Name = "mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// text [string, optional] - Response body sent from the server or loaded from the 
        /// browser cache. This field is populated with textual content only. The text field 
        /// is either HTTP decoded text or a encoded (e.g. "base64") representation of the 
        /// response body. Leave out this field if the information is not available.
        /// 
        /// Before setting the text field, the HTTP response is decoded (decompressed & 
        /// unchunked), than trans-coded from its original character set into UTF-8. 
        /// Additionally, it can be encoded using e.g. base64. Ideally, the application 
        /// should be able to unencode a base64 blob and get a byte-for-byte identical resource 
        /// to what the browser operated on.
        /// 
        /// Here is another example with encoded response. The original response is:
        /// <html><head></head><body/></html>\n
        /// "content": {
        ///     "size": 33,
        ///     "compression": 0,
        ///     "mimeType": "text/html; charset=utf-8",
        ///     "text": "PGh0bWw+PGhlYWQ+PC9oZWFkPjxib2R5Lz48L2h0bWw+XG4=",
        ///     "encoding": "base64",
        ///     "comment": ""
        /// }
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        /// encoding [string, optional] (new in 1.2) - Encoding used for response text field 
        /// e.g "base64". Leave out this field if the text field is HTTP decoded 
        /// (decompressed & unchunked), than trans-coded from its original character set into UTF-8.
        /// 
        /// Encoding field is useful for including binary responses (e.g. images) into the HAR file.
        /// 
        /// Here is another example with encoded response. The original response is:
        /// <html><head></head><body/></html>\n
        /// "content": {
        ///     "size": 33,
        ///     "compression": 0,
        ///     "mimeType": "text/html; charset=utf-8",
        ///     "text": "PGh0bWw+PGhlYWQ+PC9oZWFkPjxib2R5Lz48L2h0bWw+XG4=",
        ///     "encoding": "base64",
        ///     "comment": ""
        /// }
        /// </summary>
        [DataMember(Name = "encoding")]
        public string Encoding { get; set; }

        /// <summary>
        /// comment [string, optional] (new in 1.2) - A comment provided by the user or the application.
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }
    }
}