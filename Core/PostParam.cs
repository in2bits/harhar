using System.Runtime.Serialization;

namespace HarHar
{
    /// <summary>
    /// Posted parameter embedded in <postData> object.
    /// </summary>
    [DataContract]
    public class PostParam
    {
        /// <summary>
        /// name [string] - name of a posted parameter.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// value [string, optional] - value of a posted parameter or content of 
        /// a posted file.
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }

        /// <summary>
        /// fileName [string, optional] - name of a posted file.
        /// </summary>
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// contentType [string, optional] - content type of a posted file.
        /// </summary>
        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// comment [string, optional] (new in 1.2) - A comment provided by 
        /// the user or the application.
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }
    }
}