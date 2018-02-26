using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SerializationTesting
{
    internal static class Helper
    {

        public static void SaveSettings<T>(object d, string filePath)
        {
            // export
            var writerSettings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };

            var serializer = new XmlSerializer(typeof(T));
            //var serializer = new XmlSerializer(typeof(List<Location>), new Type[] { typeof(Location) });

            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create)))
            {
                var writer = XmlWriter.Create(sw, writerSettings);
                serializer.Serialize(writer, d);
            }
        }

        public static void SaveSettings_Old<T>(object d, string filePath)
        {
            // export
            var writerSettings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };

            var serializer = new XmlSerializer(typeof(T));

            //// Create an XmlTextWriter using a FileStream.
            //Stream fs = new FileStream(filePath, FileMode.Create);
            //var writer = XmlWriter.Create(fs, writerSettings);
            //serializer.Serialize(writer, d);
            //writer.Close();

            using (StreamWriter sw = new StreamWriter(File.Open(filePath, FileMode.Create)))
            {
                var writer = XmlWriter.Create(sw, writerSettings);
                serializer.Serialize(writer, d);
            }
        }

        public static T LoadSettings<T>(string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(filePath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
