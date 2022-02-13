namespace RestfulAPI.Models
{
    public class Subscriber
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public IList<EventSubscriber> EventSubscribers { get; set; }
    }
}
