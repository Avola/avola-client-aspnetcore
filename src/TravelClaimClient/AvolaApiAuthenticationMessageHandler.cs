using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using TravelClaimClient.Models;

namespace TravelClaimClient
{
    internal class AvolaApiAuthenticationMessageHandler : DelegatingHandler
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly X509Certificate2 _certificate;
        private AppSettings _appSettings;

        private readonly bool _useCertificate = false;

        public AvolaApiAuthenticationMessageHandler(string clientId, string clientSecret, AppSettings appSettings)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _appSettings = appSettings;
            InnerHandler = new HttpClientHandler();
        }

        public AvolaApiAuthenticationMessageHandler(string clientId, X509Certificate2 certificate, AppSettings appSettings)
        {
            _clientId = clientId;
            _certificate = certificate;
            _useCertificate = true;
            _appSettings = appSettings;
            InnerHandler = new HttpClientHandler();
        }

        private TokenResponse _token = null;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_token == null)
            {
                _token = _useCertificate ? await GetTokenWithCertificateAsync() : await GetTokenWithSecretAsync();
            }

            request.Headers.Authorization = new AuthenticationHeaderValue(_token.TokenType, _token.AccessToken);
            var response = await base.SendAsync(request, cancellationToken);

            // if we get a 401, get token again and retry
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _token = _useCertificate ? await GetTokenWithCertificateAsync() : await GetTokenWithSecretAsync();
                    request.Headers.Authorization = new AuthenticationHeaderValue(_token.TokenType, _token.AccessToken);
                    response = await base.SendAsync(request, cancellationToken);
                }
            }

            return response;
        }

        private async Task<TokenResponse> GetTokenWithSecretAsync()
        {
            var client = new TokenClient(_appSettings.AuthUrl, _clientId, _clientSecret);
            return await client.RequestClientCredentialsAsync(_appSettings.AuthScope);
        }

        private async Task<TokenResponse> GetTokenWithCertificateAsync()
        {
            var handler = new HttpClientHandler();
            //if (AppSettings.Authentication.ValidateAllServerCertificates) handler.ServerCertificateValidationCallback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
            handler.ClientCertificates.Add(_certificate);

            var client = new TokenClient(
                _appSettings.AuthUrl,
                _clientId,
                handler);


            return await client.RequestClientCredentialsAsync(_appSettings.AuthScope);
        }

    }
}
