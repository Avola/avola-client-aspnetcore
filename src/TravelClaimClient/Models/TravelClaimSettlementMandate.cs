using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelClaimClient.Models
{
    public class TravelClaimSettlementMandate
    {
        public string FrissScore { get; set; }
        public string NumberOfClaimObjectsWithoutFlexibleMandate { get; set; }
        public string NumberOfClaimsInPast3Years { get; set; }
        public string TotalCalculatedComensationAmount { get; set; }
        public string PolicyNumber { get; set; }
    }
}
