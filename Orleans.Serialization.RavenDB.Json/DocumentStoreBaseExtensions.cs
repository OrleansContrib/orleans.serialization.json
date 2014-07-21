namespace Orleans.Serialization.RavenDB.Json
{
    using Raven.Client;

    public static class DocumentStoreBaseExtensions
    {
        /// <summary>
        /// Registers a special IContractResolver which takes care of proper Json serialization of grains
        /// </summary>
        /// <param name="documentStore">The document store where the contract resolver should be registered</param>
        /// <returns>The original document store with modified contract resolver.</returns>
        public static DocumentStoreBase ConfigureContractResolver(this DocumentStoreBase documentStore)
        {
            documentStore.Conventions.JsonContractResolver = new GrainReferenceAwareContractResolver();
            return documentStore;
        }
    }
}