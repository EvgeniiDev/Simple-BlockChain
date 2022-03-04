using System;
using System.Collections.Generic;

namespace BlockChain
{
    internal class Block
    {
        public int PreviousHash { get; }
        public BlockType BlockType { get; }
        public List<Transaction> transactions;

        public Block(BlockType blockType, int previousHash)
        {
            BlockType = blockType;
            PreviousHash = previousHash;
            transactions = new();
        }

        public void ChangeTransaction(int id, Transaction transaction)
        {
            if(transactions.Count<=id)
                throw new IndexOutOfRangeException();
            transactions[id] = transaction;
        }

        public int Size { get => transactions.Count;}

        internal void Add(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetHashOfTransactions(),PreviousHash);
        }

        private int GetHashOfTransactions()
        {
            int a = 0;
            foreach (var transaction in transactions)
                a = HashCode.Combine(a, transaction.GetHashCode());
            return a;
        }
    }

    public enum BlockType
    {
        Genesis,
        Common,
    }


}
