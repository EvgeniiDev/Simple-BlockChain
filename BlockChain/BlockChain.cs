﻿using System.Collections.Generic;
using System.Linq;

namespace BlockChain
{
    internal class BlockChain
    {
        LinkedList<Block> chain;
        private Block currentBlock;
        public BlockChain()
        {
            chain = new LinkedList<Block>();
            chain.AddLast(new Block(BlockType.Genesis, 0));
            var hash = chain.Last().GetHashCode();
            currentBlock = new(BlockType.Common, hash);
        }

        public void AddTransaction(Transaction transaction)
        {
            if(currentBlock.Size >= 1024)
            {
                chain.AddLast(currentBlock);
                var hash = currentBlock.GetHashCode();
                currentBlock = new(BlockType.Common, hash);
            }
            currentBlock.Add(transaction);
        }

        public Block GetBlock(int blockId)
        {
            return chain.Take(blockId).Last();
        }

        public int Validate()
        {
            int previousHash = chain.First().GetHashCode();
            var num = 0;
            foreach(var block in chain.Skip(1))
            {
                num++;
                if (previousHash != block.PreviousHash)
                    return num;
                previousHash = block.GetHashCode();
            }
            return -1;
        }
    }
}