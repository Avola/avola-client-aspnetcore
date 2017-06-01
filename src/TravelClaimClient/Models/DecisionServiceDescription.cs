using System.Collections.Generic;

namespace TravelClaimClient.Models
{
    public class DecisionServiceDescription
    {
        public int DecisionServiceId { get; set; }
        public string Name { get; set; }
        public List<DecisionServiceVersionDescription> Versions { get; set; }
    }
}