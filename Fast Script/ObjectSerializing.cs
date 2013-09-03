using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace Fast_Script
{
    class ObjectSerializing
    {
        public static void SerializeObjectToFile(string filename, object objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public static object DeSerializeObjectFromFile(string filename)
        {
            object objectToSerialize;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (object)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }

        public static object DeSerializeObjectFromFile(string filename, object backend)
        {
            object state = backend; // your object to pass in

            BinaryFormatter bFormatter = new BinaryFormatter(null, new StreamingContext(
                StreamingContextStates.All,state)); // pass it in

            object objectToSerialize = new object();
            Stream stream = File.Open(filename, FileMode.Open);
            try
            {
                objectToSerialize = (object)bFormatter.Deserialize(stream);
            }
            catch (Exception)
            {
                
            }
            stream.Close();
            return objectToSerialize;
        }

        public static void deepCopyToFileFromObject(object item, string fileName)
        {
            MemoryStream ms = null;
            Byte[] byteArray = null;
            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                ms = new MemoryStream();
                serializer.Serialize(ms, item);
                byteArray = ms.ToArray();
            }
            catch (Exception unexpected)
            {
                Trace.Fail(unexpected.Message);
                throw;
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                }
            }

            //write file
            File.WriteAllBytes(fileName, byteArray);
        }

        public static object deepCopyFromFileToObject(string fileName)
        {
            FileStream inStream = File.OpenRead(fileName);
            MemoryStream ms = new MemoryStream();

            ms.SetLength(inStream.Length);
            inStream.Read(ms.GetBuffer(), 0, (int)inStream.Length);

            ms.Flush();
            ms.Position = 0;
            inStream.Close();

            object deserializedObject = null;

            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                deserializedObject = serializer.Deserialize(ms);
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }
            return deserializedObject;
        }
    }
}
