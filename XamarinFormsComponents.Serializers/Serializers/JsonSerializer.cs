namespace XamarinFormsComponents.Serializers
{
    using System.IO;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public sealed class JsonSerializer : ISerializer
    {
        private readonly Newtonsoft.Json.JsonSerializer serializer;

        public JsonSerializer()
            : this(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            })
        {
        }

        public JsonSerializer(JsonSerializerSettings settings)
        {
            serializer = Newtonsoft.Json.JsonSerializer.Create(settings);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Ignore")]
        public void Serialize(Stream stream, object obj)
        {
            var sw = new StreamWriter(stream);
            var jtw = new JsonTextWriter(sw);
            serializer.Serialize(jtw, obj);
            jtw.Flush();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Ignore")]
        public T Deserialize<T>(Stream stream)
        {
            var sr = new StreamReader(stream);
            var jtr = new JsonTextReader(sr);
            return serializer.Deserialize<T>(jtr);
        }
    }
}
