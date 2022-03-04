using System;

namespace BlockChain
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var chain = new BlockChain();

            chain.AddTransaction(new Transaction("ivan", "senya", 10));
            chain.AddTransaction(new Transaction("1ivan", "1senya", 10));
            chain.AddTransaction(new Transaction("2ivan", "2senya", 10));
            chain.AddTransaction(new Transaction("3ivan", "3senya", 10));

            chain.AddTransaction(new Transaction("ivan", "senya", 20));
            chain.AddTransaction(new Transaction("1ivan", "1senya", 20));
            chain.AddTransaction(new Transaction("2ivan", "2senya", 20));
            chain.AddTransaction(new Transaction("3ivan", "3senya", 20));
            for(var a =0; a < 100000000; a++)
            {
                chain.AddTransaction(new Transaction(new Random().Next().ToString(), new Random().Next().ToString(), new Random().Next()));
            }
            chain.AddTransaction(new Transaction("4ivan", "4senya", 2000));

            var num = chain.Validate();
            Console.WriteLine(num==-1? $"Всё хорошо!": $"Этот блок похекали -> {num}");
            var block = chain.GetBlock(2);
            block.ChangeTransaction(2, new Transaction("2ivan", "2senya", 200000000));

            num = chain.Validate();
            Console.WriteLine(num == -1 ? $"Всё хорошо!" : $"Этот блок похекали -> {num}");
        }
    }
}
