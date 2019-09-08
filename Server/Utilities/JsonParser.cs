using System;
using System.Dynamic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Server.Utilities
{
    internal class JsonParser
    {
        private readonly DataContractJsonSerializer _serializer = new DataContractJsonSerializer(typeof(ExpandoObject));

        // Deserialize given string to valid ExpandoObject instance
        public ExpandoObject Deserialize(string data)
        {
            ExpandoObject obj;

            using (var memoryStream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(data)))
            {
                obj = (ExpandoObject)_serializer.ReadObject(memoryStream);
            }

            return obj;
        }

        // Serialize given object to string.
        public string Serialize(ExpandoObject data)
        {
            string result;
            
            using (var memoryStream = new MemoryStream())
            {
                _serializer.WriteObject(memoryStream, data);
                memoryStream.Position = 0;
                
                using (var streamReader = new StreamReader(memoryStream))
                {
                    result = streamReader.ReadToEnd();
                }
            }

            return result;
        }
    }
}