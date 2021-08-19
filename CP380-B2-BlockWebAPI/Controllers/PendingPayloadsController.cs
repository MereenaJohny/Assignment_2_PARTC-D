using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;
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
    public class PendingPayloadsController : ControllerBase
    {
        private PendingPayloads awaitingPayloads;
        public PendingPayloadsController(PendingPayloads pendingPayloads)
        {
            awaitingPayloads = pendingPayloads;
        }

        [HttpGet("/latestPayload")]
        public string GetLatestBlocks()
        {
            return JsonConvert.SerializeObject(awaitingPayloads.Payloads);
        }

        [HttpPost("/addPayload")]
        public string AddPayload (Payload payload)
        {
            awaitingPayloads.Payloads.Add(payload);
            return JsonConvert.SerializeObject("Payload Added Successfully..!!");
        }
    }
}
