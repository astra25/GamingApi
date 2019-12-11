using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamingApi
{
    public static class Helpers
    {
        private static async Task LoopEnumeratorAsync<T>(IEnumerator<T> enumerator, Func<T, Task> body)
        {
            using (enumerator)
                while (enumerator.MoveNext())
                    await body(enumerator.Current);
        }

        public static Task ForEachAsync<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
            var tasks = Partitioner.Create(source).GetPartitions(dop).Select(partition => LoopEnumeratorAsync(partition, body));
            return Task.WhenAll(tasks);
        }
    }
}
