using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
            _avolaApiClient = new AvolaApiClient(_appSettings.BaseUri, _appSettings.Client, _appSettings.Secret,
                _appSettings);
        }

        [HttpPost]
        [Route("checkpolicycoverage")]
        public async Task<string> CheckPolicyCoverage([FromBody] CheckPolicyCoverage checkPolicyCoverage)
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
                    Value = "Not Fixed Seasonal Stand"
                },
                new ExecutionRequestData()
                {
                    Key = 36,
                    Value = checkPolicyCoverage.BusinessTravel
                },
                new ExecutionRequestData()
                {
                    Key = 39,
                    Value = checkPolicyCoverage.TravelEndDate
                },
                new ExecutionRequestData()
                {
                    Key = 41,
                    Value = checkPolicyCoverage.TravelStartDate
                },
                new ExecutionRequestData()
                {
                    Key = 42,
                    Value = checkPolicyCoverage.WinterSportsTravel
                },
                new ExecutionRequestData()
                {
                    Key = 45,
                    Value = checkPolicyCoverage.CrimeorAttempttoCrime
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
                    Value = "Not Covered"
                },
                new ExecutionRequestData()
                {
                    Key = 70,
                    Value = "Present"
                },
                new ExecutionRequestData()
                {
                    Key = 72,
                    Value = "Short Term"
                },
                new ExecutionRequestData()
                {
                    Key = 75,
                    Value = "Found"
                },
                new ExecutionRequestData()
                {
                    Key = 77,
                    Value = "World Coverage"
                },
                new ExecutionRequestData()
                {
                    Key = 78,
                    Value = "Not Covered"
                },
                new ExecutionRequestData()
                {
                    Key = 83,
                    Value = checkPolicyCoverage.TravelClaimEventDate
                },
                new ExecutionRequestData()
                {
                    Key = 86,
                    Value = "Netherlands"
                },
                new ExecutionRequestData()
                {
                    Key = 87,
                    Value = checkPolicyCoverage.TravelDestinationCountry
                }
            };

            if (checkPolicyCoverage.SportPracticed != null && checkPolicyCoverage.SportPracticed != "-")
            {
                execdata.Add(new ExecutionRequestData()
                {
                    Key = 52,
                    Value = checkPolicyCoverage.SportPracticed
                });
            }

            var metadata = new List<ExecutionRequestData>()
            {
                new ExecutionRequestData()
                {
                    Key = 90,
                    Value = checkPolicyCoverage.PolicyNumber
                }
            };

            var result =
                await _avolaApiClient.ExecuteDecisionNoTrace(
                    new ApiExecutionRequest()
                    {
                        DecisionServiceId = 5,
                        VersionNumber = 1,
                        ExecutionRequestData = execdata,
                        ExecutionRequestMetaData = metadata,
                        Reference = $"policycoverage--{checkPolicyCoverage.PolicyNumber}"
                    });

            var hitconclusion = result.HitConclusions[0];


            return JsonConvert.SerializeObject(hitconclusion.Value);
        }

        [HttpPost]
        [Route("checkobjectcoverage")]
        public async Task<string> CheckLuggageObjectCoverage([FromBody] LuggageClaimObjectCoverage luggageobject)
        {
            var execdata = new List<ExecutionRequestData>()
            {
                new ExecutionRequestData()
                {
                    Key = 1,
                    Value = "No Careless Use"
                },
                new ExecutionRequestData()
                {
                    Key = 2,
                    Value = "No Damage to Lodging"
                },
                new ExecutionRequestData()
                {
                    Key = 3,
                    Value = "Liable"
                },
                new ExecutionRequestData()
                {
                    Key = 5,
                    Value = luggageobject.LuggageClaimCause
                },
                new ExecutionRequestData()
                {
                    Key = 9,
                    Value = luggageobject.LuggageClaimObjectinHandLuggage
                },
                new ExecutionRequestData()
                {
                    Key = 10,
                    Value = luggageobject.LuggageClaimObjectLocation
                },
                new ExecutionRequestData()
                {
                    Key = 11,
                    Value = "Direct Supervision"
                },
                new ExecutionRequestData()
                {
                    Key = 14,
                    Value = luggageobject.LuggageClaimObject
                }


            };
            var metadata = new List<ExecutionRequestData>()
            {
                new ExecutionRequestData()
                {
                    Key = 90,
                    Value = luggageobject.PolicyNumber
                }
            };

            var result =
                await _avolaApiClient.ExecuteDecisionNoTrace(
                    new ApiExecutionRequest()
                    {
                        DecisionServiceId = 2,
                        VersionNumber = 1,
                        ExecutionRequestData = execdata,
                        ExecutionRequestMetaData = metadata,
                        Reference = $"policyobjectcoverage--{luggageobject.PolicyNumber}"
                    });

            var hitconclusion = result.HitConclusions[0];


            return JsonConvert.SerializeObject(hitconclusion.Value);
        }

        [HttpPost]
        [Route("checkclaimmandate")]
        public async Task<string> CheckClaimSettleMandate([FromBody] TravelClaimSettlementMandate mandate)
        {
            var execdata = new List<ExecutionRequestData>()
            {
                new ExecutionRequestData()
                {
                    Key = 49,
                    Value = mandate.FrissScore
                },
                new ExecutionRequestData()
                {
                    Key = 50,
                    Value = mandate.NumberOfClaimObjectsWithoutFlexibleMandate
                },
                new ExecutionRequestData()
                {
                    Key = 51,
                    Value = mandate.NumberOfClaimsInPast3Years
                },
                new ExecutionRequestData()
                {
                    Key = 58,
                    Value = mandate.TotalCalculatedComensationAmount
                }
            };

            var metadata = new List<ExecutionRequestData>()
            {
                new ExecutionRequestData()
                {
                    Key = 90,
                    Value = mandate.PolicyNumber
                }
            };

            var result =
                await _avolaApiClient.ExecuteDecisionNoTrace(
                    new ApiExecutionRequest()
                    {
                        DecisionServiceId = 4,
                        VersionNumber = 1,
                        ExecutionRequestData = execdata,
                        ExecutionRequestMetaData = metadata,
                        Reference = $"ClaimMandate--{mandate.PolicyNumber}"
                    });

            if (result.HitConclusions.Any())
            {
                var hitconclusion = result.HitConclusions[0];
                return JsonConvert.SerializeObject(hitconclusion.Value);
            }

            return JsonConvert.SerializeObject(result.ConclusionValueType.ToString());
        }

        [HttpPost]
        [Route("checkclaimobjectmandate")]
        public async Task<string> CheckObjectClaimSettleMandate([FromBody] LuggageClaimObjectCalculatedCompensationAmount compensationAmount)
        {
            var execdata = new List<ExecutionRequestData>()
            {
                new ExecutionRequestData()
                {
                    Key = 14,
                    Value = compensationAmount.LuggageClaimObject
                },
                new ExecutionRequestData()
                {
                    Key = 17,
                    Value = compensationAmount.LuggageClaimObjectCurrentSalesValue
                },
                new ExecutionRequestData()
                {
                    Key = 21,
                    Value = compensationAmount.LuggageClaimObjectPurchaseDate
                },
                new ExecutionRequestData()
                {
                    Key = 22,
                    Value = compensationAmount.LuggageClaimObjectPurchaseValue
                },
                new ExecutionRequestData()
                {
                    Key = 83,
                    Value = compensationAmount.TravelClaimEventDate
                },
            };
            if (compensationAmount.LuggageClaimObject == "Laptop Computer")
            {
                execdata.Add(
                    new ExecutionRequestData()
                    {
                        Key = 23,
                        Value = compensationAmount.LuggageClaimObjectRepair
                    });

                if (compensationAmount.LuggageClaimObjectRepair == "Repair")
                {
                    execdata.Add(
                        new ExecutionRequestData()
                        {
                            Key = 24,
                            Value = compensationAmount.LuggageClaimObjectRepairValue
                        });
                }
            }
            else
            {
                if (compensationAmount.TravelNumberofInsuredPersons == null)
                    compensationAmount.TravelNumberofInsuredPersons = "1";
                execdata.Add(
                    new ExecutionRequestData()
                    {
                        Key = 40,
                        Value = compensationAmount.TravelNumberofInsuredPersons
                    });
            }
            if (compensationAmount.LuggageClaimObject == "Cosmetics")
            {
                execdata.Add(
                    new ExecutionRequestData()
                    {
                        Key = 20,
                        Value = compensationAmount.LuggageClaimObjectOpened
                    });
            }

            var metadata = new List<ExecutionRequestData>()
            {
                new ExecutionRequestData()
                {
                    Key = 90,
                    Value = compensationAmount.PolicyNumber
                }
            };

            var result =
                await _avolaApiClient.ExecuteDecisionNoTrace(
                    new ApiExecutionRequest()
                    {
                        DecisionServiceId = 3,
                        VersionNumber = 1,
                        ExecutionRequestData = execdata,
                        ExecutionRequestMetaData = metadata,
                        Reference = $"ObjectSettlementMandate--{compensationAmount.PolicyNumber}"
                    });

            if (result.HitConclusions.Any())
            {
                var hitconclusion = result.HitConclusions[0];
                return JsonConvert.SerializeObject(hitconclusion.Value);
            }

            return JsonConvert.SerializeObject(result.ConclusionValueType.ToString());
        }

        [HttpPost]
        [Route("checkobjectcompensationamount")]
        public async Task<string> CheckObjectCompensation(
            [FromBody] LuggageClaimObjectCalculatedCompensationAmount compensationAmount)
        {
            var execdata = new List<ExecutionRequestData>()
            {
                new ExecutionRequestData()
                {
                    Key = 14,
                    Value = compensationAmount.LuggageClaimObject
                },
                new ExecutionRequestData()
                {
                    Key = 17,
                    Value = compensationAmount.LuggageClaimObjectCurrentSalesValue
                },
                new ExecutionRequestData()
                {
                    Key = 21,
                    Value = compensationAmount.LuggageClaimObjectPurchaseDate
                },
                new ExecutionRequestData()
                {
                    Key = 22,
                    Value = compensationAmount.LuggageClaimObjectPurchaseValue
                },
                new ExecutionRequestData()
                {
                    Key = 83,
                    Value = compensationAmount.TravelClaimEventDate
                },
            };
            if (compensationAmount.LuggageClaimObject == "Laptop Computer")
            {
                execdata.Add(
                    new ExecutionRequestData()
                    {
                        Key = 23,
                        Value = compensationAmount.LuggageClaimObjectRepair
                    });

                if (compensationAmount.LuggageClaimObjectRepair == "Repair")
                {
                    execdata.Add(
                        new ExecutionRequestData()
                        {
                            Key = 24,
                            Value = compensationAmount.LuggageClaimObjectRepairValue
                        });
                }
            }
            else
            {
                if (compensationAmount.TravelNumberofInsuredPersons == null)
                    compensationAmount.TravelNumberofInsuredPersons = "1";
                execdata.Add(
                    new ExecutionRequestData()
                    {
                        Key = 40,
                        Value = compensationAmount.TravelNumberofInsuredPersons
                    });
            }
            if (compensationAmount.LuggageClaimObject == "Cosmetics")
            {
                execdata.Add(
                    new ExecutionRequestData()
                    {
                        Key = 20,
                        Value = compensationAmount.LuggageClaimObjectOpened
                    });
            }

            var metadata = new List<ExecutionRequestData>()
            {
                new ExecutionRequestData()
                {
                    Key = 90,
                    Value = compensationAmount.PolicyNumber
                }
            };

            var result =
                await _avolaApiClient.ExecuteDecisionNoTrace(
                    new ApiExecutionRequest()
                    {
                        DecisionServiceId = 1,
                        VersionNumber = 2,
                        ExecutionRequestData = execdata,
                        ExecutionRequestMetaData = metadata,
                        Reference = $"ObjectCompensationAmount--{compensationAmount.PolicyNumber}"
                    });

            if (result.HitConclusions.Any())
            {
                var hitconclusion = result.HitConclusions[0];
                return JsonConvert.SerializeObject(hitconclusion.Value);
            }

            return JsonConvert.SerializeObject(result.ConclusionValueType.ToString());
        }

        [HttpGet]
        [Route("getdecisionserviceversiondescription/{decisionserviceid}/{version}")]
        public async Task<string> GetDecisionserviceVersionDescription(int decisionserviceid, int version)
        {
            var versionDescription = await _avolaApiClient.GetDecisionserviceVersionDescription(decisionserviceid, version);
            return JsonConvert.SerializeObject(versionDescription);
        }

        [HttpGet]
        [Route("listavailabledecisionservices/{decisionserviceid}")]
        public async Task<string> ListAvailableDecisionServices(int decisionserviceid)
        {
            var list = await _avolaApiClient.ListAvailableDecisionServices(decisionserviceid);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return JsonConvert.SerializeObject(list[0].Versions);
            }
        }

    }
}



