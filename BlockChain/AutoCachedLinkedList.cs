using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BlockChain
{
    internal class AutoCachedLinkedList<T>
    {
        private LinkedList<T> list = new ();
        private static int minAmountOfElementsInMemory = 1024;
        private static int maxAmountOfElementsInMemory = minAmountOfElementsInMemory * 2;
        private int totalElementsInCache = 0;
        private string localCacheFileName = Guid.NewGuid().ToString();

        internal void AddLast(T item)
        {
            list.AddLast(item);
            if (list.Count > maxAmountOfElementsInMemory)
            {
                if (totalElementsInCache > 0)
                {
                    var cache = Import(localCacheFileName);
                    var newCache = cache.Concat(list.Take(maxAmountOfElementsInMemory - minAmountOfElementsInMemory));

                    Export(newCache, localCacheFileName);
                }
                else
                {
                    var newCache = list.Take(maxAmountOfElementsInMemory - minAmountOfElementsInMemory);
                    Export(newCache, localCacheFileName);
                }
                for (var a = 0; a < maxAmountOfElementsInMemory - minAmountOfElementsInMemory; a++)
                    list.RemoveFirst();
                totalElementsInCache += maxAmountOfElementsInMemory - minAmountOfElementsInMemory;
            }
        }

        internal T Last()
        {
            return list.Last.Value;
        }

        internal IEnumerable<T> Take(int amount)
        {
            if (amount <= totalElementsInCache)
                return Import(localCacheFileName).Take(amount);
            return Import(localCacheFileName).Concat(list).Take(amount);
        }

        internal T First()
        {
            if (totalElementsInCache != 0)
                return Import(localCacheFileName).First();
            return list.First.Value;
        }

        internal IEnumerable<T> Skip(int amount)
        {
            if (amount >= totalElementsInCache)
            {
                return list.Skip(totalElementsInCache-amount);
            }
            return list.Concat(list).Skip(amount);
        }

        static JsonSerializerOptions options = new JsonSerializerOptions
        {
            IncludeFields = true,
        };

        private IEnumerable<T> Import(string fileName)
        {
            return JsonSerializer.Deserialize<LinkedList<T>>(File.ReadAllText(fileName), options);
        }

        private void Export(IEnumerable<T> list, string fileName)
        {
            var jsonString = JsonSerializer.Serialize(list.ToList(), options);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
