using System.Runtime.Serialization;

namespace HarHar
{
    /// <summary>
    /// This object describes various phases within request-response round trip. 
    /// All times are specified in milliseconds.
    /// 
    /// The send, wait and receive timings are not optional and must have non-negative values.
    /// 
    /// An exporting tool can omit the blocked, dns, connect and ssl, timings on every 
    /// request if it is unable to provide them. Tools that can provide these timings 
    /// can set their values to -1 if they don’t apply. For example, connect would be -1 
    /// for requests which re-use an existing connection.
    /// 
    /// The time value for the request must be equal to the sum of the timings supplied 
    /// in this section (excluding any -1 values).
    /// 
    /// Following must be true in case there are no -1 values (entry is an object in log.entries) :
    /// entry.time == entry.timings.blocked + entry.timings.dns +
    ///     entry.timings.connect + entry.timings.send + entry.timings.wait +
    ///     entry.timings.receive;
    /// </summary>
    [DataContract]
    public class Timings
    {
        /// <summary>
        /// blocked [number, optional] - Time spent in a queue waiting for a network connection. 
        /// Use -1 if the timing does not apply to the current request.
        /// </summary>
        [DataMember(Name = "blocked")]
        public int Blocked { get; set; }

        /// <summary>
        /// dns [number, optional] - DNS resolution time. The time required to resolve a host name. 
        /// Use -1 if the timing does not apply to the current request.
        /// </summary>
        [DataMember(Name = "dns")]
        public int Dns { get; set; }

        /// <summary>
        /// connect [number, optional] - Time required to create TCP connection. 
        /// Use -1 if the timing does not apply to the current request.
        /// </summary>
        [DataMember(Name = "connect")]
        public int Connect { get; set; }

        /// <summary>
        /// send [number] - Time required to send HTTP request to the server.
        /// 
        /// The send, wait and receive timings are not optional and must have non-negative values.
        /// </summary>
        [DataMember(Name = "send")]
        public int Send { get; set; }

        /// <summary>
        /// wait [number] - Waiting for a response from the server.
        /// 
        /// The send, wait and receive timings are not optional and must have non-negative values.
        /// </summary>
        [DataMember(Name = "wait")]
        public int Wait { get; set; }

        /// <summary>
        /// receive [number] - Time required to read entire response from the server (or cache).
        /// 
        /// The send, wait and receive timings are not optional and must have non-negative values.
        /// </summary>
        [DataMember(Name = "receive")]
        public int Receive { get; set; }

        /// <summary>
        /// ssl [number, optional] (new in 1.2) - Time required for SSL/TLS negotiation. 
        /// If this field is defined then the time is also included in the connect field 
        /// (to ensure backward compatibility with HAR 1.1). Use -1 if the timing does not 
        /// apply to the current request.
        /// </summary>
        [DataMember(Name = "ssl")]
        public int Ssl { get; set; }

        /// <summary>
        /// comment [string, optional] (new in 1.2) - A comment provided by the user or 
        /// the application.
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        public long GetTotal()
        {
            var total = 0;
            if (Blocked != -1)
                total += Blocked;
            if (Dns != -1)
                total += Dns;
            if (Connect != -1)
                total += Connect;
            if (Send != -1)
                total += Send;
            if (Wait != -1)
                total += Wait;
            if (Receive != -1)
                total += Receive;
            if (Ssl != -1)
                total += Ssl;
            return total;
        }
    }
}