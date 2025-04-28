namespace C__Grammer_Tester.Blockchain
{
    public class Blockchain
    {
        public List<Block> Chain { get; }
        public int Difficulty { get; } = 2;

        public Blockchain()
        {
            Chain = new List<Block> { CreateGenesisBlock() };
        }

        private Block CreateGenesisBlock()
        {
            return new Block(0, DateTime.Now, "0");
        }

        public Block GetLatestBlock()
        {
            return Chain[^1];
        }

        public void AddTransaction(Transaction transaction)
        {
            Block latestBlock = GetLatestBlock();
            latestBlock.AddTransaction(transaction);
        }

        public void AddBlock(Block newBlock, Wallet minerWallet)
        {
            newBlock.MineBlock(Difficulty);
            newBlock.PreviousHash = GetLatestBlock().Hash;
            newBlock.Hash = newBlock.CalculateHash();
            Chain.Add(newBlock);

            // 보상 추가
            newBlock.AddTransaction(new Transaction("System", minerWallet.Address, 50));
            minerWallet.UpdateBalance(50); // 보상 지급
        }

        public bool IsChainValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                var currentBlock = Chain[i];
                var previousBlock = Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                    return false;

                if (currentBlock.PreviousHash != previousBlock.Hash)
                    return false;
            }
            return true;
        }
    }

}
