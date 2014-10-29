using System.IO;
using System.Xml.Serialization;

namespace Common.Utility.Extensions
{
    public static class XmlSerializerExtension
    {
        public static T Deserialize<T>(this XmlSerializer self, string serializedObject)
        {
            using (var sr = new StringReader(serializedObject))
            {
                return (T)self.Deserialize(sr);
            }
        }
    }
}
