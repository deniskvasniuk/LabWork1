using Microsoft.EntityFrameworkCore;



namespace LabWork1.Models
{
    public class BookingContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }
    }
}
