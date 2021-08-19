using CP380_B1_BlockList.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP380_B2_BlockWebAPI.Models
{
    public class PendingPayloads
    {
        public PendingPayloads()
        {
            Payloads = new List<Payload>();
        }

        public List<Payload> Payloads;
    }
}
