using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using RestfulAPI.Models;

namespace RestfulAPI
{
    public class alicakil : IDisposable
    {
        public void CreateDemoDatabase()
        {
            Context c = new Context();

            // Create a d atabase with Dummy data (if not there)...
            if (!(c.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            { 
                c.Database.EnsureCreated();

                // Some Events...
                c.Events.Add(new Event() { EventName = "Meeting1", Description = "Official Meeting1", Location = "Bangkok", EventTime = DateTime.Now.AddDays(3) });
                c.Events.Add(new Event() { EventName = "Meeting2", Description = "Official Meeting2", Location = "Kyiv", EventTime = DateTime.Now.AddDays(4) });
                c.Events.Add(new Event() { EventName = "Meeting3", Description = "Official Meeting3", Location = "Istanbul", EventTime = DateTime.Now.AddDays(5) });
                c.SaveChanges();


                // 50000 people...
                for (int i = 1; i < 50000; i++) c.Subscribers.Add(new Subscriber() { Name = "Subscriber" + i, Email = i + "@mail.com" });
                c.SaveChanges();

                // Event Registirations...
                for (int i = 1;     i < 20000; i++) c.EventSubscribers.Add(new EventSubscriber() { Eventid = 1, Subscriberid = i });
                for (int i = 20000; i < 40000; i++) c.EventSubscribers.Add(new EventSubscriber() { Eventid = 2, Subscriberid = i });
                for (int i = 50000; i < 50000; i++) c.EventSubscribers.Add(new EventSubscriber() { Eventid = 3, Subscriberid = i });
                c.SaveChanges();
            }            
        }

        public void Dispose()
        {
            
        }
    }
}
