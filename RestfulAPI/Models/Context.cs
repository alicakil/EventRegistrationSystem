using Microsoft.EntityFrameworkCore;

namespace RestfulAPI.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=ALI\SQLEXPRESS15;Initial Catalog=ERS; Integrated Security=true;");
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<EventSubscriber> EventSubscribers { get; set; }
    }
}
