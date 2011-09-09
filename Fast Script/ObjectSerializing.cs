using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Fast_Script
{
    class ObjectSerializing
    {
        public static void SerializeObject(string filename, object objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public static object DeSerializeObject(string filename)
        {
            object objectToSerialize;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (object)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }

        public static object DeSerializeObject(string filename, object backend)
        {
            object state = backend; // your object to pass in

            BinaryFormatter bFormatter = new BinaryFormatter(null, new StreamingContext(
                StreamingContextStates.All,state)); // pass it in

            object objectToSerialize;
            Stream stream = File.Open(filename, FileMode.Open);
            //BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (object)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }
}
