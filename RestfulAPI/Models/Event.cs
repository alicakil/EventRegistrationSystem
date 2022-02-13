using System.ComponentModel.DataAnnotations;

namespace RestfulAPI.Models
{
    public class Event
    {
        [Key]
        public int id { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? Description { get; set; }
        public DateTime EventTime { get; set; } = DateTime.Now;
        public string? GeoInformation { get; set; }

        public IList<EventSubscriber> EventSubscribers { get; set; } = new List<EventSubscriber>();
    }
}