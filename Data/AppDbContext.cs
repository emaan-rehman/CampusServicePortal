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
            // 1. Force User to be a standard table with NO inheritance/discriminators
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasNoDiscriminator(); // This is the critical line to stop the crash
            });

            // 2. If you have a Student class, you MUST either delete it 
            // OR map it to a completely different table name to avoid the conflict
            modelBuilder.Ignore<Student>();

            base.OnModelCreating(modelBuilder);
        }
    }
}