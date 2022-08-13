using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings serviceApiSettings;
        private readonly ClientSettings clientSettings;
        private readonly IClientAccessTokenCache clientAccessTokenCache;
        private readonly HttpClient _httpClient;

        public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings, IClientAccessTokenCache clientAccessTokenCache, HttpClient httpClient)
        {
            this.serviceApiSettings = serviceApiSettings.Value;
            this.clientSettings = clientSettings.Value;
            this.clientAccessTokenCache = clientAccessTokenCache;
            _httpClient = httpClient;
        }

        public async Task<string> GetToken()
        {
            var currentToken = await clientAccessTokenCache.GetAsync("WebClientToken");
            if(currentToken != null)
            {
                return currentToken.AccessToken;
            }
            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = serviceApiSettings.IdentityBaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError)
            {
                throw disco.Exception;
            }

            var clietCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = clientSettings.WebClient.ClientId,
                ClientSecret = clientSettings.WebClient.ClientSecret,
                Address = disco.TokenEndpoint
            };
            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clietCredentialTokenRequest);

            if (newToken.IsError)
            {
                throw newToken.Exception;
            }

            await clientAccessTokenCache.SetAsync("WebClientToken",newToken.AccessToken,newToken.ExpiresIn);
            return newToken.AccessToken;
        }
    }
}
