using Microsoft.EntityFrameworkCore;
using CampusServicePortal.Data;

namespace CampusServicePortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<TransportRoute> TransportRoutes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<CampusEvent> Events { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<ExamSchedule> ExamSchedules { get; set; }

    }
}