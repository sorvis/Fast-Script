using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fast_Script.data_index
{
    class indexReaderWriter
    {
        public indexReaderWriter()
        {

        }
        //public void Serialize(Stream stream)
        //{
        //    BinaryWriter writer = new BinaryWriter(stream);
        //    writer.Write(_dictionary.Count);
        //    foreach (var kvp in _dictionary)
        //    {
        //        writer.Write(kvp.Key);
        //        writer.Write(kvp.Value);
        //    }
        //    writer.Flush();
        //}

        //public Dictionary<string, int> Deserialize(Stream stream)
        //{
        //    BinaryReader reader = new BinaryReader(stream);
        //    int count = reader.ReadInt32();
        //    var dictionary = new Dictionary<string, int>(count);
        //    for (int n = 0; n < count; n++)
        //    {
        //        var key = reader.ReadString();
        //        var value = reader.ReadInt32();
        //        dictionary.Add(key, value);
        //    }
        //    return dictionary;
        //}
        public void SerializeObject(string filename, indexBuilder objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public indexBuilder DeSerializeObject(string filename, bible_data.bible Bible)
        {
            indexBuilder objectToSerialize;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (indexBuilder)bFormatter.Deserialize(stream);
            stream.Close();
            objectToSerialize.Bible = Bible;
            return objectToSerialize;
        }
    }
}
