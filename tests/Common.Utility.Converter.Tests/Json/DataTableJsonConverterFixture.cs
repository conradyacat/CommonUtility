using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;
using Common.Utility.Converter.Json;

namespace Common.Utility.Converter.Tests
{
    [TestClass]
    public class DataTableJsonConverterFixture
    {
        [TestMethod]
        public void ShouldSerialize()
        {
            DataTable dt = GetTestData();
            var settings = new JsonSerializerSettings { Converters = new List<JsonConverter> { new DataTableJsonConverter() } };
            string json = JsonConvert.SerializeObject(dt, Formatting.None, settings);

            string expected = "{\"name\":\"\",\"columns\":[{\"name\":\"Int32\",\"type\":\"System.Int32\"},{\"name\":\"Int16\",\"type\":\"System.Int16\"},"
                + "{\"name\":\"Int64\",\"type\":\"System.Int64\"},{\"name\":\"Decimal\",\"type\":\"System.Decimal\"},{\"name\":\"Float\",\"type\":\"System.Single\"},"
                + "{\"name\":\"Double\",\"type\":\"System.Double\"},{\"name\":\"String\",\"type\":\"System.String\"},{\"name\":\"DateTime\",\"type\":\"System.DateTime\"}],"
                + "\"rows\":[[12,2,3,123.4,543.21,321.98,\"Hello!\",\"2014-11-25T00:00:00\"],[null,null,null,null,null,null,null,null]]}";

            Assert.AreEqual(expected, json);
        }

        [TestMethod]
        public void ShouldDeserialize()
        {
            string json = "{\"name\":\"Sample\",\"columns\":[{\"name\":\"Int32\",\"type\":\"System.Int32\"},{\"name\":\"Int16\",\"type\":\"System.Int16\"},"
                + "{\"name\":\"Int64\",\"type\":\"System.Int64\"},{\"name\":\"Decimal\",\"type\":\"System.Decimal\"},{\"name\":\"Float\",\"type\":\"System.Single\"},"
                + "{\"name\":\"Double\",\"type\":\"System.Double\"},{\"name\":\"String\",\"type\":\"System.String\"},{\"name\":\"DateTime\",\"type\":\"System.DateTime\"}],"
                + "\"rows\":[[12,2,3,123.4,543.21,321.98,\"Hello!\",\"2014-11-25T00:00:00\"],[null,null,null,null,null,null,null,null]]}";

            var settings = new JsonSerializerSettings { Converters = new List<JsonConverter> { new DataTableJsonConverter() }, NullValueHandling = NullValueHandling.Include };
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json, settings);

            DataTable expected = GetTestData();
            foreach (DataColumn column in expected.Columns)
            {
                Assert.IsTrue(dt.Columns.Contains(column.ColumnName));
                Assert.AreEqual(column.DataType, dt.Columns[column.ColumnName].DataType);
            }

            for (int i = 0; i < expected.Rows.Count; i++)
            {
                var row1 = expected.Rows[i];
                var row2 = dt.Rows[i];

                for (int j = 0; j < row1.ItemArray.Length; j++)
                {
                    Assert.AreEqual(row1[j], row2[j]);
                }
            }
        }

        private static DataTable GetTestData()
        {
            var dt = new DataTable();
            dt.Columns.Add("Int32", typeof(int));
            dt.Columns.Add("Int16", typeof(short));
            dt.Columns.Add("Int64", typeof(long));
            dt.Columns.Add("Decimal", typeof(decimal));
            dt.Columns.Add("Float", typeof(float));
            dt.Columns.Add("Double", typeof(double));
            dt.Columns.Add("String", typeof(string));
            dt.Columns.Add("DateTime", typeof(DateTime));

            dt.Rows.Add(12, (short)2, 3L, 123.4m, 543.21, 321.98, "Hello!", new DateTime(2014, 11, 25));
            dt.Rows.Add(DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);

            return dt;
        }
    }
}
