using Microsoft.EntityFrameworkCore;
using CampusServicePortal.Data;

namespace CampusServicePortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Core Entities
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        // Feature Entities
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<TransportRoute> TransportRoutes { get; set; }
        public DbSet<TransportBooking> TransportBookings { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<CampusEvent> Events { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        // Other Entities (Placeholders for future use)
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<HostelRoom> HostelRooms { get; set; }
        public DbSet<ExamSchedule> ExamSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. FIX LOGIN CRASH: Disable inheritance for the Users table
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasNoDiscriminator();
            });

            // 2. PREVENT CONFLICT: Ignore the Student class to keep the model simple
            modelBuilder.Ignore<Student>();

            // 3. MAP NEW TABLES: Explicitly link C# classes to SQL Tables
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollments");
            modelBuilder.Entity<CampusEvent>().ToTable("Events");
            modelBuilder.Entity<TransportBooking>().ToTable("TransportBookings");
            modelBuilder.Entity<Complaint>().ToTable("Complaints");
            modelBuilder.Entity<ExamSchedule>(entity =>
            {
                entity.ToTable("ExamSchedules");
                entity.HasKey(e => e.Id);
                // Explicitly define columns if they still show red squiggles
                entity.Property(e => e.SubjectCode);
                entity.Property(e => e.SubjectName);
            });
            modelBuilder.Entity<HostelRoom>(entity =>
            {
                entity.ToTable("HostelRooms");
                entity.HasKey(e => e.RoomId); // This resolves the 'requires a primary key' error
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}