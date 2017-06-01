using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelClaimClient.Models;

namespace TravelClaimClient
{
    public class AvolaApiClient
    {
        private readonly string _baseUrl;
        private readonly string _clientId;
        private readonly AvolaApiAuthenticationMessageHandler _handler;
        private AppSettings _appSettings;

        private readonly HttpClient _client;

        private AvolaApiClient(string baseUrl, string clientId)
        {
            _baseUrl = baseUrl;
            _clientId = clientId;
        }

        public AvolaApiClient(string baseUrl, string clientId, string clientSecret, AppSettings appSettings)
            : this(baseUrl, clientId)
        {
            _appSettings = appSettings;
            _handler = new AvolaApiAuthenticationMessageHandler(clientId, clientSecret, _appSettings);
            _client = CreateHttpClient();
        }

        public AvolaApiClient(string baseUrl, string clientId, X509Certificate2 clientCertificate, AppSettings appSettings)
            : this(baseUrl, clientId)
        {
            _appSettings = appSettings;
            _handler = new AvolaApiAuthenticationMessageHandler(clientId, clientCertificate, _appSettings);
            _client = CreateHttpClient();
        }

        private string CreateUrl(string url)
        {
            //var finalUrl = new Uri(_client.BaseAddress, url).AbsolutePath;
            var finalUrl = _baseUrl.TrimEnd(new[] { '/' }) + "/" + url.TrimStart(new[] { '/' });
            finalUrl = finalUrl.TrimEnd(new[] { '/' });

            return finalUrl;
        }

        private HttpClient CreateHttpClient()
        {
            var client =new HttpClient(_handler);

            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public async Task<string> DoSimpleRequest(string url, HttpMethod httpMethod)
        {
            var finalUrl = CreateUrl(url);

            var request = new HttpRequestMessage(httpMethod, finalUrl);
            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode) HandleErrorResponse(response.StatusCode, response.Content.ReadAsStringAsync().Result);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<T> DoSimpleRequest<T>(string url, HttpMethod httpMethod)
        {
            var finalUrl = CreateUrl(url);

            var request = new HttpRequestMessage(httpMethod, finalUrl);
            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode) HandleErrorResponse(response.StatusCode, response.Content.ReadAsStringAsync().Result);

            var jsonContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }

        private void HandleErrorResponse(HttpStatusCode httpStatusCode, string responseContent)
        {
            if (httpStatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedAccessException();
            throw new Exception(responseContent);
        }

        public async Task<T> DoPostObjectRequest<T, U>(string url, U objectToPost)
        {
            var finalUrl = CreateUrl(url);

            var response = await _client.PostAsync(finalUrl, new StringContent(JsonConvert.SerializeObject(objectToPost), Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode) HandleErrorResponse(response.StatusCode, response.Content.ReadAsStringAsync().Result);

            var jsonContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }

        public async Task<ExecutionResult> ExecuteDecision(ApiExecutionRequest executionRequest)
        {
            var url = "ApiExecution/execute";
            return await DoPostObjectRequest<ExecutionResult, ApiExecutionRequest>(url, executionRequest);
        }

        public async Task<ExecutionResult> ExecuteDecisionNoTrace(ApiExecutionRequest executionRequest)
        {
            var url = "ApiExecution/execute/notrace";
            return await DoPostObjectRequest<ExecutionResult, ApiExecutionRequest>(url, executionRequest);
        }

        public async Task<IList<DecisionServiceDescription>> ListAvailableDecisionServices(int? decisionServiceId = null)
        {
            var url = "ApiExecution/decisions/list";
            if (decisionServiceId != null)
            {
                url += $"/{decisionServiceId}";
            }
            return await DoSimpleRequest<IList<DecisionServiceDescription>>(url, HttpMethod.Get);
        }

        public async Task<IList<DecisionServiceDescription>> ListAvailableVersions(int? decisionServiceId = null)
        {
            var url = "ApiExecution/decisions";
            if (decisionServiceId != null)
            {
                url += $"/{decisionServiceId}";
            }
            return await DoSimpleRequest<IList<DecisionServiceDescription>>(url, HttpMethod.Get);
        }


        public async Task<DecisionServiceVersionDescription> GetDecisionserviceVersionDescription(int decisionServiceId, int version)
        {
            var url = $"ApiExecution/decisions/{decisionServiceId}/{version}";
            return await DoSimpleRequest<DecisionServiceVersionDescription>(url, HttpMethod.Get);
        }

        public async Task<ApiDescription> GetApiDescription()
        {
            var url = "Settings";
            return await DoSimpleRequest<ApiDescription>(url, HttpMethod.Get);
        }
    }
}
