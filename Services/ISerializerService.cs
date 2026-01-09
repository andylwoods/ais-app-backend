namespace myapp.Services
{
    public interface ISerializerService
    {
        string SerializeToXml<T>(T obj);
    }
}
