using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TravelClaimClient.Controllers
{
    [Route("api/[controller]")]
    public class TravelController : Controller
    {

        public TravelController()
        {
            
        }
        [HttpPost]
        [Route("checkpolicycoverage")]
        public string CheckPolicyCoverage([FromBody] object person)
        {
            return "Covered/Not Covered";
        }

        [HttpPost]
        [Route("checkobcjectcoverage")]
        public string CheckLuggageObjectCoverage([FromBody] object luggageobject)
        {
            return "Covered/Not Covered";
        }

        [HttpPost]
        [Route("checkclaimmandate")]
        public string CheckClaimSettleMandate([FromBody] object claim)
        {
            return "Flexible Mandate/No Flexible Mandate";
        }

        [HttpPost]
        [Route("checkclaimobjectmandate")]
        public string CheckObjectClaimSettleMandate([FromBody] object objectclaim)
        {
            return "Flexible Mandate/No Flexible Mandate";
        }

        [HttpPost]
        [Route("checkobjectcompensationamount")]
        public string CheckObjectCompensation([FromBody] object claim)
        {
            return "0EUR";
        }

    }
}
