using System.Runtime.Serialization;

namespace HarHar
{
    [DataContract]
    public class NameValuePairInfo
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "comment")]
        public string Comment { get; set; }
    }
}