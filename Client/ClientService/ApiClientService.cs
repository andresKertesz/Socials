using Microsoft.AspNetCore.Http;
using System.Collections.Specialized;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Socials.Client.Client.ClientService
{
    public class ApiClientService : IApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string BaseUrl;
        public ApiClientService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseUrl = _configuration["ApiUrl"];
        }

        public void SetBearerToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {

            return await _httpClient.GetAsync(BaseUrl + endpoint);

        }
        
        public async Task<HttpResponseMessage> GetAsync(string endpoint,NameValueCollection query)
        {
            UriBuilder uri = new UriBuilder(BaseUrl+endpoint);
            uri.Port = -1;
            uri.Query = query.ToString();
            try
            {
               var data =  await _httpClient.GetAsync(uri.ToString());
            } catch (Exception e) { 

                Console.WriteLine(e.ToString());
            }
            return await _httpClient.GetAsync(uri.ToString());

        }

        [ValidateAntiForgeryToken]
        public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
        {
            var response = await _httpClient.PostAsync(BaseUrl + endpoint, content);
            return response;
        }
    }
}
