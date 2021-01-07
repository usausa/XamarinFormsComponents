namespace XamarinFormsComponents
{
    using System;
    using System.Text.Json;

    using XamarinFormsComponents.Serializers;

    public static class ResolverAdapterExtensions
    {
        private static JsonSerializerOptions CreateDefaultOption()
        {
            return new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public static IResolverAdapter AddJsonSerializer(this IResolverAdapter adapter)
        {
            adapter.AddComponent<ISerializer>(new XamarinFormsComponents.Serializers.JsonSerializer());
            return adapter;
        }

        public static IResolverAdapter AddJsonSerializer(this IResolverAdapter adapter, Action<JsonSerializerOptions> action)
        {
            var option = CreateDefaultOption();
            action(option);
            adapter.AddComponent<ISerializer>(new XamarinFormsComponents.Serializers.JsonSerializer(option));
            return adapter;
        }
    }
}
