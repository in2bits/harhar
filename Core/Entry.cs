﻿using System;
using System.Runtime.Serialization;

namespace HarHar
{
    /// <summary>
    /// This object represents an exported HTTP request.
    /// </summary>
    [DataContract]
    public class Entry
    {
        public Entry()
        {
            StartedDateTime = DateTime.Now;
            Cache = new CacheInfo();
            Timings = new Timings();
        }

        /// <summary>
        /// pageref [string, unique, optional] - Reference to the parent page. Leave out this 
        /// field if the application does not support grouping by pages.
        /// </summary>
        [DataMember(Name = "pageref")]
        public string PageRef { get; set; }

        /// <summary>
        /// startedDateTime [string] - Date and time stamp of the request start 
        /// (ISO 8601 - YYYY-MM-DDThh:mm:ss.sTZD).
        /// </summary>
        [DataMember(Name = "startedDateTime")]
        public DateTime StartedDateTime { get; set; }

        /// <summary>
        /// time [number] - Total elapsed time of the request in milliseconds. This is the sum 
        /// of all timings available in the timings object (i.e. not including -1 values).
        /// </summary>
        [DataMember(Name = "time")]
        public long Time { get; set; }

        /// <summary>
        /// request [object] - Detailed info about the request.
        /// </summary>
        [DataMember(Name = "request")]
        public RequestInfo Request { get; set; }

        /// <summary>
        /// response [object] - Detailed info about the response.
        /// </summary>
        [DataMember(Name = "response")]
        public ResponseInfo Response { get; set; }

        /// <summary>
        /// cache [object] - Info about cache usage.
        /// </summary>
        [DataMember(Name = "cache")]
        public CacheInfo Cache { get; set; }

        /// <summary>
        /// timings [object] - Detailed timing info about request/response round trip.
        /// </summary>
        [DataMember(Name = "timings")]
        public Timings Timings { get; set; }

        /// <summary>
        /// serverIPAddress [string, optional] (new in 1.2) - IP address of the server that 
        /// was connected (result of DNS resolution).
        /// </summary>
        [DataMember(Name = "serverIPAddress")]
        public string ServerIpAddress { get; set; }

        /// <summary>
        /// connection [string, optional] (new in 1.2) - Unique ID of the parent TCP/IP 
        /// connection, can be the client or server port number. Note that a port number 
        /// doesn't have to be unique identifier in cases where the port is shared for 
        /// more connections. If the port isn't available for the application, any other 
        /// unique connection ID can be used instead (e.g. connection index). Leave out 
        /// this field if the application doesn't support this info.
        /// </summary>
        [DataMember(Name = "connection")]
        public string Connection { get; set; }

        /// <summary>
        /// comment [string, optional] (new in 1.2) - A comment provided by the user or the 
        /// application.
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }
    }
}