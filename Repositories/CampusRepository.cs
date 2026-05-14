using CampusServicePortal.Data;
using Microsoft.EntityFrameworkCore;

namespace CampusServicePortal.Repositories
{
    public interface ICampusRepository
    {
        // 1. Complaints Management
        Task<List<Complaint>> GetComplaintsAsync();
        Task CreateComplaintAsync(Complaint complaint);

        // 2. Transport System
        Task<List<TransportRoute>> GetTransportRoutesAsync();
        Task BookSeatAsync(int routeId, string studentId);

        // 3. Library Management
        Task<List<Book>> GetBooksAsync();
        Task ReserveBookAsync(int bookId, string studentId);

        // 4. Campus Events
        Task<List<CampusEvent>> GetEventsAsync();

        // 5. Cafeteria Menu
        Task<List<MenuItem>> GetCafeteriaMenuAsync();

        // 6. Exam Schedules
        Task<List<ExamSchedule>> GetExamsAsync();
    }

    public class CampusRepository : ICampusRepository
    {
        private readonly AppDbContext _db;

        public CampusRepository(AppDbContext db)
        {
            _db = db;
        }

        // --- COMPLAINTS ---
        public async Task<List<Complaint>> GetComplaintsAsync()
        {
            try { return await _db.Complaints.Include(c => c.Student).AsNoTracking().ToListAsync(); }
            catch { return new List<Complaint>(); }
        }

        public async Task CreateComplaintAsync(Complaint complaint)
        {
            _db.Complaints.Add(complaint);
            await _db.SaveChangesAsync();
        }

        // --- TRANSPORT ---
        public async Task<List<TransportRoute>> GetTransportRoutesAsync() =>
            await _db.TransportRoutes.AsNoTracking().ToListAsync();

        public async Task BookSeatAsync(int routeId, string studentId)
        {
            // Implementation logic for booking
            await _db.SaveChangesAsync();
        }

        // --- LIBRARY ---
        public async Task<List<Book>> GetBooksAsync() =>
            await _db.Books.AsNoTracking().ToListAsync();

        public async Task ReserveBookAsync(int bookId, string studentId)
        {
            var book = await _db.Books.FindAsync(bookId);
            if (book != null) { book.IsAvailable = false; await _db.SaveChangesAsync(); }
        }

        // --- EVENTS ---
        public async Task<List<CampusEvent>> GetEventsAsync() =>
            await _db.Events.OrderBy(e => e.Date).AsNoTracking().ToListAsync();

        // --- CAFETERIA ---
        public async Task<List<MenuItem>> GetCafeteriaMenuAsync() =>
            await _db.MenuItems.AsNoTracking().ToListAsync();

        // --- EXAMS ---
        public async Task<List<ExamSchedule>> GetExamsAsync() =>
            await _db.ExamSchedules.AsNoTracking().ToListAsync();
    }
}