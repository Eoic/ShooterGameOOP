using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Server.Utilities
{
    public static class JsonParser
    {
        public static string Serialize<T>(T value)
        {
            using (var stream = new MemoryStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var dataSerializer = new DataContractJsonSerializer(typeof(T));
                    dataSerializer.WriteObject(stream, value);
                    stream.Position = 0;
                    var stringValue = reader.ReadToEnd();
                    return stringValue;
                }
            }
        }

        public static T Deserialize<T>(string valueString)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(valueString)))
            {
                var dataSerializer = new DataContractJsonSerializer(typeof(T));
                return (T)dataSerializer.ReadObject(stream);
            }
        }
    }
}