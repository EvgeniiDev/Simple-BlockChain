using System;

namespace BlockChain
{
    public class Transaction
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public double Amount { get; set; }

        public Transaction(string sender, string recipient, double amount)
        {
            Sender = sender;
            Recipient = recipient;
            Amount = amount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Sender.GetHashCode(), Recipient.GetHashCode(), Amount);
        }
    }
}
