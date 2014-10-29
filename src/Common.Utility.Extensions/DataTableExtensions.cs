using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Common.Utility.Extensions
{
    public static class DataTableExtension
    {
        public static IEnumerable<DataRow> SelectAsEnumerable(this DataTable self, Predicate<DataRow> predicate)
        {
            return self.Rows.Cast<DataRow>().Where(row => predicate(row));
        }

        public static DataRow[] Select(this DataTable self, Predicate<DataRow> predicate)
        {
            return SelectAsEnumerable(self, predicate).ToArray();
        }
    }
}
