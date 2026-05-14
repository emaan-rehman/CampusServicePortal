using CampusServicePortal.Data;
using Microsoft.EntityFrameworkCore;

namespace CampusServicePortal.Repositories
{
    // 1. Interface for Dependency Injection (Business Logic Layer)
    public interface ICampusRepository
    {
        Task<List<Complaint>> GetComplaintsAsync();
        Task CreateComplaintAsync(Complaint complaint);

        // You can add more methods here for your 15+ features
        // Example: Task<List<BusRoute>> GetTransportRoutesAsync();
    }

    // 2. Repository Implementation (Data Access Layer)
    public class CampusRepository : ICampusRepository
    {
        private readonly AppDbContext _db;

        public CampusRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Complaint>> GetComplaintsAsync()
        {
            try
            {
                // .AsNoTracking() is a performance optimization technical constraint
                // .Include() ensures the Student name shows up (Referential Integrity)
                return await _db.Complaints
                    .Include(c => c.Student)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Graceful error handling prevents application crashes
                Console.WriteLine($"Database Error: {ex.Message}");
                return new List<Complaint>();
            }
        }

        public async Task CreateComplaintAsync(Complaint complaint)
        {
            try
            {
                _db.Complaints.Add(complaint);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to database: {ex.Message}");
                throw; // Re-throw to allow UI validation to catch it
            }
        }
    }
}