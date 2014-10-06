using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace VSOTeams.Common
{
    internal static class JsonHelper
    {
        internal static async Task TryWriteJsonToFileAsync(string filename, string json)
        {
            if (json != null)
            {
                try
                {
                    StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(file, json);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        internal static T Deserialize<T>(string json)
        {
            var bytes = Encoding.Unicode.GetBytes(json);
            using (var stream = new MemoryStream(bytes))
            {
                stream.Position = 0;
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        }

        internal static string Serialize(object instance)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(instance.GetType());
                serializer.WriteObject(stream, instance);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                { return reader.ReadToEnd(); }
            }
        }
    }
}
