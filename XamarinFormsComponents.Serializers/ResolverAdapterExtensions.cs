namespace XamarinFormsComponents
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using XamarinFormsComponents.Serializers;

    public static class ResolverAdapterExtensions
    {
        private static JsonSerializerSettings CreateDefaultSetting()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
        }

        public static IResolverAdapter AddJsonSerializer(this IResolverAdapter adapter)
        {
            adapter.AddComponent<ISerializer>(new XamarinFormsComponents.Serializers.JsonSerializer());
            return adapter;
        }

        public static IResolverAdapter AddJsonSerializer(this IResolverAdapter adapter, Action<JsonSerializerSettings> action)
        {
            var settings = CreateDefaultSetting();
            action(settings);
            adapter.AddComponent<ISerializer>(new XamarinFormsComponents.Serializers.JsonSerializer(settings));
            return adapter;
        }
    }
}
