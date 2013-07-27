using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HarHar
{
    /// <summary>
    /// This object describes posted data, if any (embedded in <request> object).
    /// </summary>
    [DataContract]
    public class PostData
    {
        /// <summary>
        /// mimeType [string] - Mime type of posted data.
        /// </summary>
        [DataMember(Name = "mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// params [array] - List of posted parameters (in case of URL encoded parameters).
        /// 
        /// List of posted parameters, if any (embedded in <postData> object).
        /// 
        /// Note that text and params fields are mutually exclusive.
        /// </summary>
        [DataMember(Name = "params")]
        public IEnumerable<PostParam> Params { get; set; }

        /// <summary>
        /// text [string] - Plain text posted data
        /// 
        /// Note that text and params fields are mutually exclusive.
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

        /// <summary>
        /// comment [string, optional] (new in 1.2) - A comment provided by the user or 
        /// the application.
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }
    }
}