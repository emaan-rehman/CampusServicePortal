using Microsoft.EntityFrameworkCore;

namespace CampusServicePortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<TransportBooking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Setting up Relationship for the Data Access Layer
            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.Student)
                .WithMany(u => u.Complaints)
                .HasForeignKey(c => c.StudentId);
        }
    }
}