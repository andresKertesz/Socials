using System.Collections.Specialized;

namespace Socials.Client.Client.ClientService
{
    public interface IApiClientService
    {
        Task<HttpResponseMessage> GetAsync(string endpoint);
        Task<HttpResponseMessage> GetAsync(string endpoint,NameValueCollection nameValueCollection);
        Task<HttpResponseMessage> PostAsync(string endpoint, HttpContent content);
        void SetBearerToken(string token);



    }
}
