using Newtonsoft.Json;
using Socials.Client.Client.Model;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
namespace Socials.Client.Client.Controllers
{
    public class EventController
    {
        public HttpClient Client { get; set; }
        public EventController()
        {
            Client = new HttpClient();
        }
        public async Task<List<Event>?> GetEvents()
        {
            List<Event>? events = await Client.GetFromJsonAsync<List<Event>>("/sample-data/SampleEvents.Json");
            return events;
        }
    }
}
