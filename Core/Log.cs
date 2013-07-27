using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack.Text;

namespace HarHar
{
    /// <summary>
    /// This object represents the root of exported data.
    /// 
    /// There is one Page object for every exported web page and one Entry object for 
    /// every HTTP request. In case when an HTTP trace tool isn't able to group requests 
    /// by a page, the Pages object is empty and individual requests doesn't have a parent page.
    /// 
    /// http://www.softwareishard.com/blog/har-12-spec/
    /// </summary>
    [DataContract(Name="log")]
    public class Log
    {
        static Log()
        {
            JsConfig<DateTime>.SerializeFn = time => time.ToUniversalTime().ToString("o");
        }

        public Log()
        {
            Version = "1.2";
            Creator = new Creator
                {
                    Name = "HarHar",
                    Version = "0.1",
                    Comment = "HAR v1.2 API by in2bits at https://github.com/in2bits/harhar"
                };
        }

        /// <summary>
        /// version [string] - Version number of the format. If empty, string "1.1" is assumed by default.
        /// </summary>
        [DataMember(Name="version")]
        public string Version { get; set; }

        /// <summary>
        /// creator [object] - Name and version info of the log creator application.
        /// </summary>
        [DataMember(Name = "creator")]
        public Creator Creator { get; set; }

        /// <summary>
        /// browser [object, optional] - Name and version info of used browser.
        /// </summary>
        [DataMember(Name = "browser")]
        public Browser Browser { get; set; }

        /// <summary>
        /// pages [array, optional] - List of all exported (tracked) pages. Leave out this field if the 
        /// application does not support grouping by pages.
        /// 
        /// This object represents an array with all exported HTTP requests. Sorting entries by 
        /// startedDateTime (starting from the oldest) is preferred way how to export data since 
        /// it can make importing faster. However the reader application should always make sure 
        /// the array is sorted (if required for the import).
        /// </summary>
        [DataMember(Name = "pages")]
        public IList<Page> Pages { get; set; }

        /// <summary>
        /// entries [array] - List of all exported (tracked) requests.
        /// </summary>
        [DataMember(Name = "entries")]
        public IList<Entry> Entries { get; set; }

        /// <summary>
        /// comment [string, optional] (new in 1.2) - A comment provided by the user or the application.
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        public string ToJson()
        {
            return JsonSerializer.SerializeToString(new LogRoot(this));
        }

        [DataContract]
        public class LogRoot
        {
            public LogRoot(Log log)
            {
                _log = log;
            }

            private readonly Log _log;
            [DataMember(Name="log")]
            public Log Log { get { return _log; } }
        }
    }
}
