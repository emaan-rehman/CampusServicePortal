using CampusServicePortal.Data;
using Microsoft.EntityFrameworkCore;

namespace CampusServicePortal.Repositories
{
    public interface ICampusRepository
    {
        Task<List<Complaint>> GetComplaintsAsync();
        Task CreateComplaintAsync(Complaint complaint);
    }

    public class CampusRepository : ICampusRepository
    {
        private readonly AppDbContext _db;
        public CampusRepository(AppDbContext db) => _db = db;

        public async Task<List<Complaint>> GetComplaintsAsync()
        {
            try
            {
                return await _db.Complaints.Include(c => c.Student).AsNoTracking().ToListAsync();
            }
            catch { return new List<Complaint>(); }
        }

        public async Task CreateComplaintAsync(Complaint complaint)
        {
            _db.Complaints.Add(complaint);
            await _db.SaveChangesAsync();
        }
    }
}