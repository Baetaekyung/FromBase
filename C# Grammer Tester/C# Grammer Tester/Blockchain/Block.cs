using System.Security.Cryptography;
using System.Text;

namespace C__Grammer_Tester.Blockchain
{
    public class Block
    {
        public int Index { get; }
        public DateTime Timestamp { get; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public int Nonce { get; private set; }
        public List<Transaction> Transactions { get; set; }

        public Block(int index, DateTime timestamp, string previousHash = "")
        {
            Index = index;
            Timestamp = timestamp;
            PreviousHash = previousHash;
            Transactions = new List<Transaction>();
            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string rawData = Index + Timestamp.ToString() + PreviousHash + Nonce;
                foreach (var transaction in Transactions)
                {
                    rawData += transaction.TransactionHash;
                }
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return Convert.ToHexString(bytes);
            }
        }

        public void MineBlock(int difficulty)
        {
            string target = new string('0', difficulty);
            while (Hash.Substring(0, difficulty) != target)
            {
                Nonce++;
                Hash = CalculateHash();
            }
            Console.WriteLine($"Block mined: {Hash}");
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }
    }
}
