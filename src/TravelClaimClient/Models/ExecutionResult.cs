using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelClaimClient.Models
{
    public class ExecutionResult
    {
        public int DecisionTableId { get; set; }
        public int DecisionServiceId { get; set; }
        public string Reference { get; set; }
        public List<int> FinalConclusionBusinessDataIds { get; set; }
        public ConclusionValueType ConclusionValueType { get; set; }
        public List<HitConclusion> HitConclusions { get; set; } = new List<HitConclusion>();
        public List<ErrorMessage> Errors { get; set; } = new List<ErrorMessage>();
    }
}
