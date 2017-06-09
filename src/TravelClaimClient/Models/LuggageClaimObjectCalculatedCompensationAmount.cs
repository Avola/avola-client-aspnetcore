using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelClaimClient.Models
{
    public class LuggageClaimObjectCalculatedCompensationAmount
    {
        public string LuggageClaimObject { get; set; }
        public string LuggageClaimObjectCurrentSalesValue { get; set; }
        public string LuggageClaimObjectOpened { get; set; }
        public string LuggageClaimObjectPurchaseDate { get; set; }
        public string LuggageClaimObjectPurchaseValue { get; set; }
        public string LuggageClaimObjectRepair { get; set; }
        public string LuggageClaimObjectRepairValue { get; set; }
        public string TravelNumberofInsuredPersons { get; set; }
        public string TravelClaimEventDate { get; set; }
    }
}
