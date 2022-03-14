using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BlockChain
{
    internal class AutoCachedLinkedList<T> : IEnumerable<T>
    {
        private LinkedList<T> list = new();
        private int minAmountOfElementsInMemory = 1024;
        private int maxAmountOfElementsInMemory = 2048;
        private int totalElementsInCache = 0;
        private string localCacheFileName = Guid.NewGuid().ToString();
        public AutoCachedLinkedList(int minAmountOfElementsInMemory=1024, int maxAmountOfElementsInMemory=2048)
        {
            this.minAmountOfElementsInMemory = minAmountOfElementsInMemory;
            this.maxAmountOfElementsInMemory = maxAmountOfElementsInMemory;
        }

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

        static JsonSerializerOptions options = new JsonSerializerOptions
        {
            IncludeFields = true,
            WriteIndented = true,
        };

        private IEnumerable<T> Import(string fileName)
        {
            var data = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<LinkedList<T>>(data, options);
        }

        private void Export(IEnumerable<T> list, string fileName)
        {
            var jsonString = JsonSerializer.Serialize(list.ToList(), options);
            File.WriteAllText(fileName, jsonString);
        }

        public IEnumerator<T> GetEnumerator()
        {
            if(totalElementsInCache!=default)
                foreach(var item in Import(localCacheFileName))
                    yield return item;
            foreach (var item in list)
                yield return item;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
