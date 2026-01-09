using System.Xml.Serialization;

namespace myapp.Services
{
    public class SerializerService : ISerializerService
    {
        public string SerializeToXml<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }
}
