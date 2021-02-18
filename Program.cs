using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;


namespace Lab3_BlockChain_McKennaTrussel
{
    class Program
    {
        static void Main(string[] args)
        {
            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();


            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();


            Blockchain mckennacoin = new Blockchain(3, 100);

            Console.WriteLine("Start the Miner.");
            mckennacoin.MinePendingTransactions(wallet1);
            Console.WriteLine("\nBalance of wallet1 is $" + mckennacoin.GetBalanceOfWallet(wallet1).ToString());


            //mckennacoin.AddBlock(new Block(1, DateTime.Now.ToString("yyyMMddHHmmssffff"), "amount: 50"));
            //mckennacoin.AddBlock(new Block(2, DateTime.Now.ToString("yyyMMddHHmmssffff"), "amount: 200"));

            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            mckennacoin.addPendingTransaction(tx1);
            Console.WriteLine("Start the Miner.");
            mckennacoin.MinePendingTransactions(wallet2);
            Console.WriteLine("\nBalance of wallet1 is $" + mckennacoin.GetBalanceOfWallet(wallet1).ToString());
            Console.WriteLine("\nBalance of wallet2 is $" + mckennacoin.GetBalanceOfWallet(wallet2).ToString());


            string blockJSON = JsonConvert.SerializeObject(mckennacoin, Formatting.Indented);
            Console.WriteLine(blockJSON);

            ////check and see if blockchain is able to catch changes to hash
            //mckennacoin.GetLatestBlock().PreviousHash = "12345";

            if (mckennacoin.IsChainValid())
            {
                Console.WriteLine("Blockchain is Valid!");
            }
            else
            {
                Console.WriteLine("Blockchain is NOT Valid!");
            }
        }
    }
}

////ALL BELOW IS OLD CODE MOVED INTO OTHER FILES!


    //class Blockchain
    //{
    //    public List<Block> Chain { get; set; }

    //    public Blockchain()
    //    {
    //        this.Chain = new List<Block>();
    //        this.Chain.Add(CreateGenesisBlock());
    //    }

    //    public Block CreateGenesisBlock()
    //    {
    //        return new Block(0, DateTime.Now.ToString("yyyMMddHHmmssffff"), "GENESIS BLOCK");
    //    }

    //    public Block GetLatestBlock()
    //    {
    //        return this.Chain.Last();
    //    }

    //    public void AddBlock(Block newBlock)
    //    {
    //        newBlock.PreviousHash = this.GetLatestBlock().Hash;
    //        newBlock.Hash = newBlock.CalculateHash();
    //        this.Chain.Add(newBlock);
    //    }

    //    public bool IsChainValid()
    //    {
    //        for (int i = 1; i < this.Chain.Count; i++)
    //        {
    //            Block currentBlock = this.Chain[i];
    //            Block previousBlock = this.Chain[i - 1];

    //            //Check if the current block hash is the same as calculated hash
    //            if (currentBlock.Hash != currentBlock.CalculateHash())
    //            {
    //                return false;
    //            }

    //            if (currentBlock.PreviousHash != previousBlock.Hash)
    //            {
    //                return false;
    //            }

    //        }
    //        return true;
    //    }

    //}

    //class Block
    //{
    //    public int Index { get; set; }
    //    public string PreviousHash { get; set; }
    //    public string Timestamp { get; set; }
    //    public string Data { get; set; }
    //    public string Hash { get; set; }


    //    public Block(int index, string timestamp, string data, string previousHash = "")
    //    {
    //        this.Index = index;
    //        this.Timestamp = timestamp;
    //        this.Data = data;
    //        this.PreviousHash = previousHash;
    //        this.Hash = CalculateHash();
    //    }

    //    public string CalculateHash()
    //    {
    //        string blockData = this.Index + this.PreviousHash + this.Timestamp + this.Data;
    //        byte[] blockBytes = Encoding.ASCII.GetBytes(blockData);
    //        byte[] hashBytes = SHA256.Create().ComputeHash(blockBytes);
    //        return BitConverter.ToString(hashBytes).Replace("-", "");
    //    }
    //}

