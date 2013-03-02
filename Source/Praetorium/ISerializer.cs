using System.IO;

namespace Praetorium
{
    public interface ISerializer
    {

        T Clone<T>(T instance);

        T Deserialize<T>(byte[] data);

        T Deserialize<T>(Stream stream);

        void Serialize<T>(Stream stream, T instance);

        byte[] Serialize<T>(T instance);

    }
}
