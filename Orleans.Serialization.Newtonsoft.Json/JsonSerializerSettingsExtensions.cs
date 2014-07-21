namespace Orleans.Serialization.Newtonsoft.Json
{
    using global::Newtonsoft.Json;

    public static class JsonSerializerSettingsExtensions
    {
        /// <summary>
        /// Registers a special IContractResolver which takes care of proper Json serialization of grains
        /// </summary>
        /// <param name="settings">The settings where the contract resolver should be registered</param>
        /// <returns>The original settings with modified contract resolver.</returns>
        public static JsonSerializerSettings ConfigureContractResolver(this JsonSerializerSettings settings)
        {
            settings.ContractResolver = new GrainReferenceAwareContractResolver();
            return settings;
        }
    }
}