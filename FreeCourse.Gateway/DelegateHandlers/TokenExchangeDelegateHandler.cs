using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Gateway.DelegateHandlers
{
    public class TokenExchangeDelegateHandler : DelegatingHandler
    {
        public TokenExchangeDelegateHandler(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public HttpClient httpClient { get; set; }
        public IConfiguration configuration { get; set; }
        public string accessToken { get; set; }

        private async Task<string> GetToken(string requestToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                return accessToken;
            }
            var disco = await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = configuration["IdentityServerUrl"],
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError)
            {
                throw disco.Exception;
            }

            TokenExchangeTokenRequest tokenExchangeTokenRequest = new TokenExchangeTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = configuration["ClientId"],
                ClientSecret = configuration["ClientSecret"],
                GrantType = configuration["TokenGrant"],
                SubjectToken = requestToken,
                SubjectTokenType = "urn:ietf:params:oauth:grant-type:token-exchange:access-token",
                Scope = "openid discount_fullpermission payment_fullpermission"
            };
            var tokenResponse = await httpClient.RequestTokenExchangeTokenAsync(tokenExchangeTokenRequest);
            if (tokenResponse.IsError)
            {
                throw tokenResponse.Exception;
            }

            accessToken = tokenResponse.AccessToken;
            return accessToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestToken = request.Headers.Authorization.Parameter;
            var newToken = await GetToken(requestToken);

            request.SetBearerToken(newToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
