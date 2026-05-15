using Microsoft.EntityFrameworkCore;
using CampusServicePortal.Data;

namespace CampusServicePortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<TransportRoute> TransportRoutes { get; set; }
        public DbSet<TransportBooking> TransportBookings { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<CampusEvent> Events { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<ExamSchedule> ExamSchedules { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<HostelRoom> HostelRooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the inheritance mapping for the Users table
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType") // This matches a column in your SQL table
                .HasValue<User>("User")               // Rows with 'User' become User objects
                .HasValue<Student>("Student");         // Rows with 'Student' become Student objects

            // Also ensure you have no duplicate definitions to avoid earlier errors
        }
    }
}