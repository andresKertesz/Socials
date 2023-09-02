namespace Socials.Client.Client.ClientService
{
    public class ApiClientService : IApiClientService
    {
        private readonly HttpClient _httpClient;
        public ApiClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void SetBearerToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {

            return await _httpClient.GetAsync(endpoint);

        }

        public async Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content)
        {

            var response = await _httpClient.PostAsync(endpoint, content);
            return response;
        }
    }
}
