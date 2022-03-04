using System;
using System.Collections.Generic;

namespace BlockChain
{
    internal class Block
    {
        public int PreviousHash { get; }
        public BlockType BlockType { get; }
        public List<Transaction> Transactions;
        public int Size { get => Transactions.Count; }

        public Block(BlockType blockType, int previousHash)
        {
            BlockType = blockType;
            PreviousHash = previousHash;
            Transactions = new();
        }

        public void ChangeTransaction(int id, Transaction transaction)
        {
            if(Transactions.Count<=id)
                throw new IndexOutOfRangeException();
            Transactions[id] = transaction;
        }

        internal void Add(Transaction transaction)
        {
            Transactions.Add(transaction);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetHashOfTransactions(),PreviousHash);
        }

        private int GetHashOfTransactions()
        {
            int a = 0;
            foreach (var transaction in Transactions)
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
