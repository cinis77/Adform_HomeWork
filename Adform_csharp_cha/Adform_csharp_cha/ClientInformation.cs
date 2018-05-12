using System;
using IdentityModel.Client;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adform_csharp_cha
{
    class ClientInformation
    {
        string Scope { get; }
        string ClientID { get; }
        string ClientSecret { get; }
        DiscoveryClient DiscoverClientInformation { get; }
        HttpClient HttpClientForRequest;


        /// <summary>
        /// According to object forms HttpClient
        /// </summary>
        public async Task FormHttpClientData()
        {
            Console.WriteLine("Creating client information");
            var clientDiscovery = await DiscoverClientInformation.GetAsync();
            TokenClient newClient = new TokenClient(clientDiscovery.TokenEndpoint, ClientID, clientSecret: ClientSecret, style: AuthenticationStyle.PostValues);
            var tokenClientResponse = await newClient.RequestClientCredentialsAsync(Scope);
            HttpClientForRequest = new HttpClient();
            HttpClientForRequest.SetBearerToken(tokenClientResponse.AccessToken);
            
        }

        public HttpClient GetHttpClientData()
        {
            if (HttpClientForRequest == null)
                throw new ArgumentException(message:"HttpClient is not set");
            return HttpClientForRequest;
        }

        public ClientInformation(string accessConfigurationAddress, string Scope, string ClientId, string ClientSecret)
        {
            DiscoverClientInformation = new DiscoveryClient(accessConfigurationAddress);
            this.Scope = Scope;
            this.ClientID = ClientId;
            this.ClientSecret = ClientSecret;
        }

       
    }
}
