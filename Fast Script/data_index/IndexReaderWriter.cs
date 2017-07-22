using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fast_Script.data_index
{
    class IndexReaderWriter
    {
        public IndexReaderWriter()
        {

        }
        
        public void SerializeObject(string filename, IndexBuilder objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public IndexBuilder DeSerializeObject(string filename, bible_data.Bible Bible)
        {
            IndexBuilder objectToSerialize;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (IndexBuilder)bFormatter.Deserialize(stream);
            stream.Close();
            objectToSerialize.Bible = Bible;
            return objectToSerialize;
        }
    }
}
