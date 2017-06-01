using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelClaimClient
{
    public class AppSettings
    {
        public string AuthUrl { get; set; }
        public string AuthScope { get; set; }
        public string IdsrvrBaseUri { get; set; }
        public string IdsrvrClient { get; set; }
        public string IdsrvrSecret { get; set; }
    }
}
