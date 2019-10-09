namespace XamarinFormsComponents.Serializers
{
    using System.IO;

    public interface ISerializer
    {
        void Serialize(Stream stream, object obj);

        T Deserialize<T>(Stream stream);
    }
}
