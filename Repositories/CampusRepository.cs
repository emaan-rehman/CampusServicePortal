using CampusServicePortal.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CampusServicePortal.Repositories
{
    public interface ICampusRepository
    {
        Task<List<Complaint>> GetComplaintsAsync();
        Task CreateComplaintAsync(Complaint complaint);
        Task<List<TransportRoute>> GetTransportRoutesAsync();
        Task<List<Book>> GetBooksAsync();
        Task<List<CampusEvent>> GetEventsAsync();
        Task<List<MenuItem>> GetCafeteriaMenuAsync();
        Task<List<ExamSchedule>> GetExamsAsync();
        Task<List<Role>> GetRolesAsync();
        Task<List<User>> GetUsersAsync();
        Task UpdateUserRoleAsync(int userId, int roleId);
    }

    public class CampusRepository : ICampusRepository
    {

        private readonly AppDbContext _db;
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public CampusRepository(AppDbContext db, IDbContextFactory<AppDbContext> dbFactory)
        {
            _db = db;
            _dbFactory = dbFactory;
        }

        public async Task<List<Complaint>> GetComplaintsAsync()
        {
            // Use the factory to prevent "Second operation started on this context"
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Complaints
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CreateComplaintAsync(Complaint complaint)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            db.Complaints.Add(complaint);
            await db.SaveChangesAsync();
        }

        public async Task<List<TransportRoute>> GetTransportRoutesAsync() => await _db.TransportRoutes.AsNoTracking().ToListAsync();
        public async Task<List<Book>> GetBooksAsync()
        {
            // This ensures a fresh connection is opened specifically for this request
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Books.AsNoTracking().ToListAsync();
        }
        public async Task<List<CampusEvent>> GetEventsAsync() => await _db.Events.AsNoTracking().ToListAsync();
        public async Task<List<MenuItem>> GetCafeteriaMenuAsync() => await _db.MenuItems.AsNoTracking().ToListAsync();
        public async Task<List<ExamSchedule>> GetExamsAsync() => await _db.ExamSchedules.AsNoTracking().ToListAsync();
        public async Task<List<Role>> GetRolesAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Roles.AsNoTracking().ToListAsync();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Users.Include(u => u.Role).AsNoTracking().ToListAsync();
        }

        public async Task UpdateUserRoleAsync(int userId, int roleId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var user = await db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                user.RoleId = roleId; // This now exists because of Step 1
                await db.SaveChangesAsync();
            }
        }
    }
}