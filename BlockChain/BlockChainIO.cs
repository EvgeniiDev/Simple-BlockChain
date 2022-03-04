using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlockChain
{
    internal class BlockChainIO
    {

        public static void Import()
        {
            
        }

        public static void Export(LinkedList<Block> blockChain)
        {
            var jsonString = JsonSerializer.Serialize(blockChain.ToList());
            File.WriteAllText("fileName", jsonString);
        }
    }
}
