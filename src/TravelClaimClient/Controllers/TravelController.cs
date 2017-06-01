using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TravelClaimClient.Models;

namespace TravelClaimClient.Controllers
{
    [Route("api/[controller]")]
    public class TravelController : Controller
    {
        private AvolaApiClient _avolaApiClient;
        private AppSettings _appSettings;

        public TravelController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _avolaApiClient = new AvolaApiClient(_appSettings.BaseUri, _appSettings.Client, _appSettings.Secret, _appSettings);
        }

        [HttpPost]
        [Route("checkpolicycoverage")]
        public async Task<string> CheckPolicyCoverage([FromBody] object person, string policynumber)
        {
            var execdata = new List<ExecutionRequestData>()
            {
                new ExecutionRequestData()
                {
                    Key = 34,
                    Value = "Paid"
                },
                new ExecutionRequestData()
                {
                    Key = 35,
                    Value = "FRONTseasonal"
                },
                new ExecutionRequestData()
                {
                    Key = 36,
                    Value = "FRONTtravelvorbusiness"
                },
                new ExecutionRequestData()
                {
                    Key = 39,
                    Value = "FRONTeinddatumreis"
                },
                new ExecutionRequestData()
                {
                    Key = 41,
                    Value = "FRONTstartdatumreis"
                },
                new ExecutionRequestData()
                {
                    Key = 42,
                    Value = "FONTwintersporttijdensreis"
                },
                new ExecutionRequestData()
                {
                    Key = 45,
                    Value = "FRONTcrimeofgeencrime"
                },
                new ExecutionRequestData()
                {
                    Key = 47,
                    Value = "No Driving Without Drivers License"
                },
                new ExecutionRequestData()
                {
                    Key = 48,
                    Value = "Not Foreseeable"
                },
                new ExecutionRequestData()
                {
                    Key = 65,
                    Value = "Premium Payment Done"
                },
                new ExecutionRequestData()
                {
                    Key = 67,
                    Value = "No War, No Riots nor Molest"
                },
                new ExecutionRequestData()
                {
                    Key = 68,
                    Value = "Covered"
                },
                new ExecutionRequestData()
                {
                    Key = 70,
                    Value = "Present"
                },
                new ExecutionRequestData()
                {
                    Key = 72,
                    Value = "FRONTdurationtype"
                },
                new ExecutionRequestData()
                {
                    Key = 75,
                    Value = "FRONTPOLICYSTATUS"
                },
                new ExecutionRequestData()
                {
                    Key = 77,
                    Value = "FRONTworldocverage"
                },
                new ExecutionRequestData()
                {
                    Key = 78,
                    Value = "FRONTwintersportscoverage"
                },
                new ExecutionRequestData()
                {
                    Key = 83,
                    Value = "FRONTwanneer"
                },
                new ExecutionRequestData()
                {
                    Key = 86,
                    Value = "FRONTresidencecountry"
                },
                new ExecutionRequestData()
                {
                    Key = 87,
                    Value = "FRONTtowhichcountry"
                }
            };
            var metadata = new List<ExecutionRequestData>();

            var result =
                await _avolaApiClient.ExecuteDecisionNoTrace(
                    new ApiExecutionRequest() {DecisionServiceId = 5, VersionNumber = 1, ExecutionRequestData = execdata, ExecutionRequestMetaData = metadata, Reference = $"policycoverage--{policynumber}"});

            var hitconclusion = result.HitConclusions[0];


            return hitconclusion.Value;
        }

        [HttpPost]
        [Route("checkobcjectcoverage")]
        public async Task<string> CheckLuggageObjectCoverage([FromBody] object luggageobject)
        {
            var result =
                await _avolaApiClient.ExecuteDecisionNoTrace(
                    new ApiExecutionRequest() { DecisionServiceId = 2, VersionNumber = 1 });

            var hitconclusion = result.HitConclusions[0];


            return hitconclusion.Value;
        }

        [HttpPost]
        [Route("checkclaimmandate")]
        public async Task<string> CheckClaimSettleMandate([FromBody] object claim)
        {
            var result =
                await _avolaApiClient.ExecuteDecisionNoTrace(
                    new ApiExecutionRequest() { DecisionServiceId = 4, VersionNumber = 1 });

            return "Flexible Mandate/No Flexible Mandate";
        }

        [HttpPost]
        [Route("checkclaimobjectmandate")]
        public async Task<string> CheckObjectClaimSettleMandate([FromBody] object objectclaim)
        {
            var result =
                await _avolaApiClient.ExecuteDecisionNoTrace(
                    new ApiExecutionRequest() { DecisionServiceId = 3, VersionNumber = 1 });

            return "Flexible Mandate/No Flexible Mandate";
        }

        [HttpPost]
        [Route("checkobjectcompensationamount")]
        public async Task<string> CheckObjectCompensation([FromBody] object claim)
        {
            var result =
                await _avolaApiClient.ExecuteDecisionNoTrace(
                    new ApiExecutionRequest() { DecisionServiceId = 1, VersionNumber = 2 });

            return "0EUR";
        }

        [HttpGet]
        [Route("listallservices")]
        public async Task<IList<DecisionServiceDescription>> Listall()
        {
            var list = await _avolaApiClient.ListAvailableDecisionServices();
            return list;
        }

    }
}
