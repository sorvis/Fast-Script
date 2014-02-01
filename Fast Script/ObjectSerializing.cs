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

        public static object DeSerializeObjectFromFile(string filename, object state)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(
                StreamingContextStates.All,state)); // pass it in

            object objectToSerialize = new object();
            Stream stream = File.Open(filename, FileMode.Open);
            try
            {
                objectToSerialize = (object)binaryFormatter.Deserialize(stream);
            }
            catch (Exception)
            {
                
            }
            stream.Close();
            return objectToSerialize;
        }

        public static void deepCopyToFileFromObject(object item, string fileName)
        {
            MemoryStream memoryStream = null;
            Byte[] byteArray = null;
            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                memoryStream = new MemoryStream();
                serializer.Serialize(memoryStream, item);
                byteArray = memoryStream.ToArray();
            }
            catch (Exception unexpected)
            {
                Trace.Fail(unexpected.Message);
                throw;
            }
            finally
            {
                if (memoryStream != null)
                {
                    memoryStream.Close();
                }
            }

            //write file
            File.WriteAllBytes(fileName, byteArray);
        }

        public static object deepCopyFromFileToObject(string fileName)
        {
            FileStream inStream = File.OpenRead(fileName);
            MemoryStream memoryStream = new MemoryStream();

            memoryStream.SetLength(inStream.Length);
            inStream.Read(memoryStream.GetBuffer(), 0, (int)inStream.Length);

            memoryStream.Flush();
            memoryStream.Position = 0;
            inStream.Close();

            object deserializedObject = null;

            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                deserializedObject = serializer.Deserialize(memoryStream);
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Close();
            }
            return deserializedObject;
        }
    }
}
