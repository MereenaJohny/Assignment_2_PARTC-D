using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;
using CP380_B2_BlockWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP380_B2_BlockWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BlocksController : ControllerBase
    {
        // TODO

        private BlockList BList;
        

        public BlocksController(BlockList blockList)
        {
            BList = blockList;
        }

        [HttpGet("/blocks")]
        public string GetAllBlocks()
        {
            return JsonConvert.SerializeObject(BList.Chain.Select(block => new BlockSummary()
            {

                TimeStamp = block.TimeStamp,
                PreviousHash = block.PreviousHash,
                Hash = block.Hash

            }));
        }

        [HttpGet("/blocks/{hash?}")]
        public string GetAllBlocks(string hash)
        {
            var block = BList.Chain
                .Where(block => block.Hash.Equals(hash));

            if (block != null && block.Count() > 0)
            {
                return JsonConvert.SerializeObject(block
                    .Select(block => new BlockSummary()
                    {
                        Hash = block.Hash,
                        PreviousHash = block.PreviousHash,
                        TimeStamp = block.TimeStamp
                    }
                    )
                    .First());
            }

            return JsonConvert.SerializeObject(NotFound());
        }

        [HttpGet("{hash}/Payloads")]
        public string GetAllPayloads(string hash)
        {
            var block = BList.Chain
                        .Where(block => block.Hash.Equals(hash));

            if (block != null && block.Count() > 0)
            {
                return JsonConvert.SerializeObject(block
                    .Select(block => block.Data
                    )
                    .First());
            }

            return JsonConvert.SerializeObject(NotFound());
        }

        [HttpPost("/blocks")]
        public string PostBlock(Block block)
        {
            BlockListService BListService = new BlockListService();
            Block blockResponse = BListService.SubmitNewBlock(block.Hash, block.Nonce, block.TimeStamp);
            if(blockResponse != null)
            {
                return JsonConvert.SerializeObject(blockResponse);
            }
            else
            {
                return "400 Bad request";
            }
        }
    }
}
