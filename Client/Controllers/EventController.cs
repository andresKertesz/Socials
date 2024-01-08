using Newtonsoft.Json;
using Socials.Client.Client.Model;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Socials.Client.Client.ClientService;
using Newtonsoft.Json.Linq;
using System.Web;

namespace Socials.Client.Client.Controllers
{
    public class EventController
    {
        private enum Options { distance, top}
        private const string BASE_EVENT_ENDPOINT = "event/";
        private const string BASE_PUBLIC_EVENT_ENDPOINT = "public/event/";
        private readonly IApiClientService ApiClient;

        public EventController(IApiClientService ApiClient)
        {
            this.ApiClient = ApiClient;
        }

        public async Task<List<Event>> GetEventsFromDistanceAsync(int max_distance, double latitude, double longitude)
        {
            List<Event> events = new List<Event>();
            var data = HttpUtility.ParseQueryString(string.Empty);

            data["option"] = Options.distance.ToString();
            data["max_distance"] = max_distance.ToString();
            data["latitude"] = latitude.ToString();
            data["longitude"] = longitude.ToString();

            var response = await ApiClient.GetAsync(BASE_EVENT_ENDPOINT + "?" +data.ToString()); 
            if (response.IsSuccessStatusCode)
            {
                string rawData = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(rawData);
                if (json.ContainsKey("data"))
                {
                    string eventsFound = json["data"].ToString();
                    Console.WriteLine(eventsFound);
                    var eventList = JsonConvert.DeserializeObject<List<Event>>(eventsFound);
                    if(eventList != null)
                    {
                        events = eventList;
                    }
                }
            }
            return events;
        }
        
        public async Task<List<Event>> GetPublicEventsFromDistanceAsync(int max_distance, double latitude, double longitude)
        {
            List<Event> events = new List<Event>();
            var data = HttpUtility.ParseQueryString(string.Empty);

            data["option"] = Options.distance.ToString();
            data["max_distance"] = max_distance.ToString();
            data["latitude"] = latitude.ToString();
            data["longitude"] = longitude.ToString();

            var response = await ApiClient.GetAsync(BASE_PUBLIC_EVENT_ENDPOINT + "?" +data.ToString()); 
            if (response.IsSuccessStatusCode)
            {
                string rawData = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(rawData);
                if (json.ContainsKey("data"))
                {
                    string eventsFound = json["data"].ToString();
                    Console.WriteLine(eventsFound);
                    var eventList = JsonConvert.DeserializeObject<List<Event>>(eventsFound);
                    if(eventList != null)
                    {
                        events = eventList;
                    }
                }
            }
            return events;
        }
    }
}
