using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Testing.Infrastructure
{
    static public class FileService
    {
        static public List<T> LoadJson<T>(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<T> objects = JsonConvert.DeserializeObject<List<T>>(json);
                return objects;
            }

        }


        static public void SaveJson<T>(List<T> list, string path)
        {

            string json = JsonConvert.SerializeObject(list.ToArray());

            System.IO.File.WriteAllText(path, json);

        }
    }
}
