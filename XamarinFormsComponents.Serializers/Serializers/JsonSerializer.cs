namespace XamarinFormsComponents.Serializers
{
    using Newtonsoft.Json;

    public sealed class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings settings;

        public JsonSerializer(JsonSerializerSettings settings)
        {
            this.settings = settings;
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }

        public T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text, settings);
        }
    }
}
