using System.ComponentModel.DataAnnotations;

namespace RestfulAPI.Models
{
    public class EventSubscriber
    {
        [Key]
        public int id { get; set; }
        public int Eventid { get; set; }
        public int Subscriberid { get; set; }

    }
}
