using System;

namespace BlockChain
{
    public class Transaction
    {
        public string sender { get; set; }
        public string recipient { get; set; }
        public double amount { get; set; }

        public Transaction(string sender, string recipient, double amount)
        {
            this.sender = sender;
            this.recipient = recipient;
            this.amount = amount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(sender.GetHashCode(), recipient.GetHashCode(), amount);
        }
    }
}
