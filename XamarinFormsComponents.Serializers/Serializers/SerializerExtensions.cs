namespace XamarinFormsComponents.Serializers
{
    using System.IO;
    using System.Text;

    public static class SerializerExtensions
    {
        public static string SerializeUtf8(this ISerializer serializer, object obj)
        {
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public static T DeserializeUtf8<T>(this ISerializer serializer, string text)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(text)))
            {
                return serializer.Deserialize<T>(stream);
            }
        }
    }
}
