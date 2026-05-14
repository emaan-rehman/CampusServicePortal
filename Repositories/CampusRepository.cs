using CampusServicePortal.Data;
using Microsoft.EntityFrameworkCore; // This enables the .Include() method

public interface ICampusRepository
{
    Task<List<Complaint>> GetComplaintsAsync();
    Task CreateComplaintAsync(Complaint complaint);
}

public class CampusRepository : ICampusRepository
{
    private readonly AppDbContext _db;
    public CampusRepository(AppDbContext db) => _db = db;

    public async Task<List<Complaint>> GetComplaintsAsync() =>
        await _db.Complaints.Include(c => c.Student).ToListAsync();

    public async Task CreateComplaintAsync(Complaint complaint)
    {
        _db.Complaints.Add(complaint);
        await _db.SaveChangesAsync();
    }
}