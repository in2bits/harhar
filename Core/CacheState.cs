using System;
using System.Runtime.Serialization;

namespace HarHar
{
    /// <summary>
    /// This objects contains info about the cache state of a resource
    /// </summary>
    [DataContract]
    public class CacheState
    {
        /// <summary>
        /// expires [string, optional] - Expiration time of the cache entry.
        /// </summary>
        [DataMember(Name = "expires")]
        public DateTime? Expires { get; set; }

        /// <summary>
        /// lastAccess [string] - The last time the cache entry was opened.
        /// </summary>
        [DataMember(Name = "lastAccess")]
        public DateTime? LastAccess { get; set; }

        /// <summary>
        /// eTag [string] - Etag
        /// </summary>
        [DataMember(Name = "eTag")]
        public string ETag { get; set; }

        /// <summary>
        /// hitCount [number] - The number of times the cache entry has been opened.
        /// </summary>
        [DataMember(Name = "hitCount")]
        public long HitCount { get; set; }

        /// <summary>
        /// comment [string, optional] (new in 1.2) - A comment provided by the user or the application.
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }
    }
}