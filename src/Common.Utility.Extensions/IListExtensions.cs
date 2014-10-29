using System.Collections.Generic;

namespace Common.Utility.Extensions
{
    public static class IListExtensions
    {
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IList<T> self, int partitionSize)
        {
            var start = 0;
            var totalCount = self.Count;

            while (start < totalCount)
            {
                var diff = totalCount - start;
                var capacity = diff < partitionSize ? diff : partitionSize;
                var list = new List<T>(capacity);
                for (var i = 0; i < capacity; i++)
                {
                    list.Add(self[start++]);
                }

                yield return list;
            }
        }
    }
}
