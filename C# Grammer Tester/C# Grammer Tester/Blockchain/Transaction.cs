using System.Security.Cryptography;
using System.Text;

namespace C__Grammer_Tester.Blockchain
{
    public class Transaction
    {
        public string FromAddress { get; }
        public string ToAddress { get; }
        public decimal Amount { get; }
        public string TransactionHash { get; private set; }

        public Transaction(string fromAddress, string toAddress, decimal amount)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
            TransactionHash = CalculateTransactionHash();
        }

        private string CalculateTransactionHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string transactionData = FromAddress + ToAddress + Amount.ToString();
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(transactionData));
                return Convert.ToHexString(bytes);
            }
        }
    }
}
