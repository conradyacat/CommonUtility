using Newtonsoft.Json;
using System;
using System.Data;

namespace Common.Utility.Converter.Json
{
    /// <summary>
    /// A custom JsonConverter for DataTable. Best used for large DataTables since 
    /// the column names will not be repeated for each row. 
    /// </summary>
    public class DataTableJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DataTable);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!reader.Read())
                return null;

            var dt = new DataTable();

            if (reader.TokenType == JsonToken.PropertyName && reader.Value.ToString() == "name")
            {
                dt.TableName = reader.ReadAsString();
                reader.Read();
            }

            // read the columns
            if (reader.TokenType == JsonToken.PropertyName && reader.Value.ToString() == "columns")
            {
                while (reader.TokenType != JsonToken.StartArray)
                    reader.Read();

                while (reader.Read() && reader.TokenType != JsonToken.EndArray)
                {
                    reader.Read();
                    string columnName = reader.ReadAsString();
                    reader.Read();
                    string typeName = reader.ReadAsString();
                    dt.Columns.Add(columnName, Type.GetType(typeName));
                    reader.Read();
                }

                reader.Read();
            }

            // read the rows
            if (reader.TokenType == JsonToken.PropertyName && reader.Value.ToString() == "rows")
            {
                while (reader.TokenType != JsonToken.StartArray)
                    reader.Read();

                while (reader.Read() && reader.TokenType != JsonToken.EndArray)
                {
                    DataRow newRow = dt.NewRow();
                    foreach (DataColumn column in dt.Columns)
                    {
                        reader.Read();
                        if (reader.Value == null)
                            continue;

                        newRow[column.ColumnName] = reader.Value;
                    }

                    dt.Rows.Add(newRow);
                    reader.Read();
                }

                reader.Read();
            }

            return dt;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dt = value as DataTable;

            if (dt == null)
                throw new ArgumentException("The object is not a DataTable type", "value");

            writer.WriteStartObject();

            writer.WritePropertyName("name");
            writer.WriteValue(dt.TableName);

            // write the columns
            writer.WritePropertyName("columns");
            writer.WriteStartArray();

            foreach (DataColumn column in dt.Columns)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("name");
                writer.WriteValue(column.ColumnName);
                writer.WritePropertyName("type");
                writer.WriteValue(column.DataType.FullName);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();

            // write the rows
            writer.WritePropertyName("rows");
            writer.WriteStartArray();

            foreach (DataRow row in dt.Rows)
            {
                writer.WriteStartArray();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    writer.WriteValue(row[i]);
                }

                writer.WriteEndArray();
            }

            writer.WriteEndArray();

            writer.WriteEndObject();
        }

        // TODO: add options: datatype normalization, etc.
    }
}
