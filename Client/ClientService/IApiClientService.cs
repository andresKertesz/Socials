namespace Socials.Client.Client.ClientService
{
    public interface IApiClientService
    {
        Task<HttpResponseMessage> GetAsync(string endpoint);
        Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content);


    }
}
