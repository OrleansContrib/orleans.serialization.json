#if NEWTONSOFT
namespace Orleans.Serialization.Newtonsoft.Json
#elif RAVENDB
namespace Orleans.Serialization.RavenDB.Json
#endif
{
    using System;
    using System.Reflection;
#if NEWTONSOFT
    using global::Newtonsoft.Json;
    using global::Newtonsoft.Json.Serialization;
#elif RAVENDB
    using Raven.Imports.Newtonsoft.Json;
    using Raven.Imports.Newtonsoft.Json.Serialization;
#endif
    internal class GrainReferenceAwareContractResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract = base.CreateContract(objectType);

            if (typeof(IGrain).IsAssignableFrom(objectType))
            {
                contract.Converter = new GrainReferenceConverter();
            }

            return contract;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            property.ShouldSerialize = instance => !typeof(GrainReference).IsAssignableFrom(property.DeclaringType);
            return property;
        }
    }
}