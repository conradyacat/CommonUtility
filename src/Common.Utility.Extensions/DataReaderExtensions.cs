using System.Data;

namespace Common.Utility.Extensions
{
    public static class DataReaderExtension
    {
        public static T GetValue<T>(this IDataReader self, string fieldName)
        {
            var ordinal = self.GetOrdinal(fieldName);

            return ordinal > -1 ? (T)self.GetValue(ordinal) : default(T);
        }

        public static bool HasField(this IDataReader self, string fieldName)
        {
            return self.GetOrdinal(fieldName) > -1;
        }
    }
}
