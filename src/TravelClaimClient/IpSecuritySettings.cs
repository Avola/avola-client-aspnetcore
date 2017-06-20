using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelClaimClient
{
    public class IpSecuritySettings
    {
        public string AllowedIPs { get; set; }
        public List<string> AllowedIPsList => !string.IsNullOrEmpty(AllowedIPs) ? AllowedIPs.Split(',').ToList() : new List<string>();
    }
}
