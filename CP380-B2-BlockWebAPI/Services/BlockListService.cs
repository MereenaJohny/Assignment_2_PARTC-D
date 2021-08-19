using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP380_B2_BlockWebAPI.Services
{
    public class BlockListService
    {
        BlockList blockLists = new BlockList();
        PendingPayloads payloadsList = new PendingPayloads();
        public BlockListService (){}
        public BlockListService(IConfiguration configuration, BlockList blockList, PendingPayloads pendingPayloads)
        {

            blockLists = blockList;
            payloadsList = pendingPayloads;
        }
        public Block SubmitNewBlock(string hash, int nonce, DateTime timestamp)
        {
            Block block = new Block(timestamp, hash, payloadsList.Payloads);
            block.CalculateHash();
            if (block.Hash.Equals(hash))
            {
                blockLists.Chain.Add(block);
                block.Data.Clear();
                return block;
            }
            else
            {
                return null;
            }
        }
    }
}
