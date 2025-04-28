namespace C__Grammer_Tester.Blockchain
{
    public class Program
    {
        static void Main(string[] args)
        {
            Blockchain myCoin = new Blockchain();
            Wallet minerWallet = new Wallet(); // 채굴자 지갑 생성

            // 트랜잭션 추가 (예: 송금)
            var transaction1 = new Transaction(minerWallet.Address, "address2", 100);
            myCoin.AddTransaction(transaction1);

            Console.WriteLine("Mining block 1...");
            myCoin.AddBlock(new Block(1, DateTime.Now, myCoin.GetLatestBlock().Hash), minerWallet);

            Console.WriteLine($"Miner's wallet balance: {minerWallet.Balance}");
            Console.WriteLine($"Is blockchain valid? {myCoin.IsChainValid()}");

            // 트랜잭션 추가 (송금 후 채굴)
            var transaction2 = new Transaction(minerWallet.Address, "address3", 50);
            myCoin.AddTransaction(transaction2);

            Console.WriteLine("Mining block 2...");
            myCoin.AddBlock(new Block(2, DateTime.Now, myCoin.GetLatestBlock().Hash), minerWallet);

            Console.WriteLine($"Miner's wallet balance: {minerWallet.Balance}");
            Console.WriteLine($"Is blockchain valid? {myCoin.IsChainValid()}");
        }
    }
}
