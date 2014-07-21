#if NEWTONSOFT
namespace Orleans.Serialization.Newtonsoft.Json
#elif RAVENDB
namespace Orleans.Serialization.RavenDB.Json
#endif
{
    using System;
#if NEWTONSOFT
    using global::Newtonsoft.Json;
#elif RAVENDB
    using Raven.Imports.Newtonsoft.Json;
#endif

    internal class GrainReferenceConverter : JsonConverter
    {
        public override bool CanRead
        {
            get { return true; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var reference = (GrainReference)value;
            string key = reference.ToKeyString();
            var info = new GrainReferenceInfo
            {
                Key = key, 
                Data = SerializationManager.SerializeToByteArray(value)
            };
            serializer.Serialize(writer, info);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                return null;
            }

            var info = new GrainReferenceInfo();
            serializer.Populate(reader, info);
            return SerializationManager.Deserialize(objectType, new BinaryTokenStreamReader(info.Data));
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IGrain).IsAssignableFrom(objectType);
        }
    }
}