using CampusServicePortal.Data;
using Microsoft.EntityFrameworkCore;

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
    }

    public class CampusRepository : ICampusRepository
    {

        private readonly AppDbContext _db;
        public CampusRepository(AppDbContext db) => _db = db;

        public async Task<List<Complaint>> GetComplaintsAsync() =>
            await _db.Complaints.Include(c => c.Student).AsNoTracking().ToListAsync();

        public async Task CreateComplaintAsync(Complaint complaint)
        {
            _db.Complaints.Add(complaint);
            await _db.SaveChangesAsync();
        }

        public async Task<List<TransportRoute>> GetTransportRoutesAsync() => await _db.TransportRoutes.AsNoTracking().ToListAsync();
        public async Task<List<Book>> GetBooksAsync() => await _db.Books.AsNoTracking().ToListAsync();
        public async Task<List<CampusEvent>> GetEventsAsync() => await _db.Events.AsNoTracking().ToListAsync();
        public async Task<List<MenuItem>> GetCafeteriaMenuAsync() => await _db.MenuItems.AsNoTracking().ToListAsync();
        public async Task<List<ExamSchedule>> GetExamsAsync() => await _db.ExamSchedules.AsNoTracking().ToListAsync();
    }
}