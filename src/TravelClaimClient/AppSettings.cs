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
        public string BaseUri { get; set; }
        public string Client { get; set; }
        public string Secret { get; set; }
    }
}
