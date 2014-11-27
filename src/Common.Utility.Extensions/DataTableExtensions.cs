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
            foreach (DataRow row in self.Rows)
            {
                if (predicate(row))
                    yield return row;
            }
        }

        public static DataRow[] Select(this DataTable self, Predicate<DataRow> predicate)
        {
            return SelectAsEnumerable(self, predicate).ToArray();
        }
    }
}
