using System.Runtime.Serialization;

namespace HarHar
{
    /// <summary>
    /// Creator and browser objects share the same structure.
    /// </summary>
    [DataContract]
    public abstract class Entity
    {
        /// <summary>
        /// name [string] - Name of the application/browser used to export the log.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        ///  version [string] - Version of the application/browser used to export the log.
        /// </summary>
        [DataMember(Name = "version")]
        public string Version { get; set; }

        /// <summary>
        /// comment [string, optional] (new in 1.2) - A comment provided by the user or the application.
        /// </summary>
        [DataMember(Name = "comment")]
        public string Comment { get; set; }
    }
}