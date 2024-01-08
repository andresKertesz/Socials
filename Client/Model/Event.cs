namespace Socials.Client.Client.Model
{
    public class Event
    {
        public int? id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public int? user_id { get; set; }
        public int? event_visibility_id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public DateTime? date { get; set; }
        public string? longitude { get; set; }
        public string? latitude { get; set; }
        public int? likes { get; set; }
        public int? participants { get; set; }
        public double? dist { get; set; }
        public Visibility? visibility { get; set; }
    
    }

    public class Visibility
    {
        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int hidden { get; set; }
    }
}
