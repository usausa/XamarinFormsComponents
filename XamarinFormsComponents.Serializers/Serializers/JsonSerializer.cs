namespace XamarinFormsComponents.Serializers;

using System.Text.Json;

public sealed class JsonSerializer : ISerializer
{
    private readonly JsonSerializerOptions options;

    public JsonSerializer()
        : this(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        })
    {
    }

    public JsonSerializer(JsonSerializerOptions options)
    {
        this.options = options;
    }

    public async ValueTask SerializeAsync(Stream stream, object obj, CancellationToken cancel = default)
    {
        await System.Text.Json.JsonSerializer.SerializeAsync(stream, obj, obj.GetType(), options, cancel).ConfigureAwait(false);
    }

    public string Serialize(object obj)
    {
        return System.Text.Json.JsonSerializer.Serialize(obj, obj.GetType(), options);
    }

    public ValueTask<T?> DeserializeAsync<T>(Stream stream, CancellationToken cancel = default)
    {
        return System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream, options, cancel);
    }

    public T? Deserialize<T>(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<T>(json, options);
    }
}
