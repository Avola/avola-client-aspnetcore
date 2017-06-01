using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<string> CheckPolicyCoverage([FromBody] object person)
        {
            var result =
                await _avolaApiClient.ExecuteDecisionNoTrace(
                    new ApiExecutionRequest() {DecisionServiceId = 5, VersionNumber = 1});

            return "Covered/Not Covered";
        }

        [HttpPost]
        [Route("checkobcjectcoverage")]
        public async Task<string> CheckLuggageObjectCoverage([FromBody] object luggageobject)
        {
            var result =
                await _avolaApiClient.ExecuteDecisionNoTrace(
                    new ApiExecutionRequest() { DecisionServiceId = 2, VersionNumber = 1 });

            return "Covered/Not Covered";
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
