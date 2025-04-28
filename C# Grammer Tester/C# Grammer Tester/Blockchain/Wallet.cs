using System.Text;
using System.Security.Cryptography;

namespace C__Grammer_Tester.Blockchain
{
    public class Wallet
    {
        public string Address { get; private set; }
        public decimal Balance { get; private set; }

        public Wallet()
        {
            Address = GenerateAddress();
            Balance = 0;  // 초기 잔액은 0
        }

        // 공개 키 생성 (주소 역할)
        private string GenerateAddress()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var key = Guid.NewGuid().ToString(); // 임시로 GUID를 주소로 사용
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                return Convert.ToHexString(bytes);
            }
        }

        // 잔액 업데이트
        public void UpdateBalance(decimal amount)
        {
            Balance += amount;
        }

        // 트랜잭션 보내기
        public void SendCoins(string toAddress, decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"Sent {amount} coins to {toAddress}. New balance: {Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }
    }
}
