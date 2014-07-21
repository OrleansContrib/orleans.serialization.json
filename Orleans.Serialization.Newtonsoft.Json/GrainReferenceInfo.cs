#if NEWTONSOFT
namespace Orleans.Serialization.Newtonsoft.Json
#elif RAVENDB
namespace Orleans.Serialization.RavenDB.Json
#endif
{
    internal class GrainReferenceInfo
    {
        public string Key { get; set; }

        public byte[] Data { get; set; }
    }
}