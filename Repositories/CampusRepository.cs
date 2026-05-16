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
        Task CreateBookingAsync(TransportBooking booking);
        Task<List<Book>> GetBooksAsync();
        Task<bool> ReserveBookAsync(int bookId);
        Task<List<CampusEvent>> GetEventsAsync();

        // Strictly matched declaration signature for Admin Events
        Task AddEventAsync(CampusEvent campusEvent);

        Task<List<MenuItem>> GetCafeteriaMenuAsync();
        Task<List<Course>> GetAllCoursesAsync();
        Task EnrollInCourseAsync(Enrollment enrollment);

        Task<List<ExamSchedule>> GetExamSchedulesAsync();
        Task AddExamScheduleAsync(ExamSchedule schedule);
        Task<User?> AuthenticateUserAsync(string email, string password);
        Task<List<User>> GetUsersAsync();
        Task<List<Role>> GetRolesAsync();
        Task UpdateUserRoleAsync(int userId, int newRoleId);
        Task<List<Faculty>> GetAllFacultyAsync();
        Task AddFacultyAsync(Faculty faculty);
        Task DeleteFacultyAsync(int id);
        Task<List<Fee>> GetStudentFeesAsync(int studentId);
        Task<List<HostelRoom>> GetAllRoomsAsync();
        Task AddRoomAsync(HostelRoom room);
        Task<bool> BookHostelRoomAsync(int roomId);
        Task<List<MenuItem>> GetCafeMenuAsync();
        Task AddMenuItemAsync(MenuItem item);
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

        public async Task CreateComplaintAsync(Complaint complaint)
        {
            // Uses a fresh, private connection for the save operation
            using var db = await _dbFactory.CreateDbContextAsync();
            db.Complaints.Add(complaint);
            await db.SaveChangesAsync();
        }

        public async Task<List<Complaint>> GetComplaintsAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Complaints
                .OrderByDescending(c => c.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<TransportRoute>> GetTransportRoutesAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.TransportRoutes.AsNoTracking().ToListAsync();
        }

        public async Task CreateBookingAsync(TransportBooking booking)
        {
            // Use the factory to ensure a fresh, private connection for this save
            using var db = await _dbFactory.CreateDbContextAsync();
            db.TransportBookings.Add(booking);
            await db.SaveChangesAsync();
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Books.AsNoTracking().ToListAsync();
        }

        public async Task<bool> ReserveBookAsync(int bookId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var book = await db.Books.FindAsync(bookId);

            if (book != null && book.IsAvailable)
            {
                book.IsAvailable = false; // Mark as checked out
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<CampusEvent>> GetEventsAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            // AsNoTracking() improves performance for read-only lists
            return await db.Events.AsNoTracking().OrderBy(e => e.Date).ToListAsync();
        }

        // Fresh factory isolated configuration for AddEvent operation
        public async Task AddEventAsync(CampusEvent campusEvent)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            db.Events.Add(campusEvent);
            await db.SaveChangesAsync();
        }

        public async Task<List<MenuItem>> GetCafeteriaMenuAsync() => await _db.MenuItems.AsNoTracking().ToListAsync();

        public async Task<List<ExamSchedule>> GetExamSchedulesAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.ExamSchedules
                .OrderBy(e => e.ExamDate)
                .ToListAsync();
        }

        public async Task AddExamScheduleAsync(ExamSchedule schedule)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            db.ExamSchedules.Add(schedule);
            await db.SaveChangesAsync();
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Users
                .Include(u => u.Role) // CRITICAL: This fills the 'Role' property
                .AsNoTracking()      // Helps prevent the 'Materialization' error
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Roles.ToListAsync();
        }

        public async Task UpdateUserRoleAsync(int userId, int newRoleId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var user = await db.Users.FindAsync(userId);
            if (user != null)
            {
                user.RoleId = newRoleId;
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Courses.ToListAsync();
        }

        public async Task EnrollInCourseAsync(Enrollment enrollment)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            db.Enrollments.Add(enrollment);
            await db.SaveChangesAsync();
        }

        public async Task<List<Faculty>> GetAllFacultyAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Faculty.OrderBy(f => f.Name).ToListAsync();
        }

        public async Task AddFacultyAsync(Faculty faculty)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            db.Faculty.Add(faculty);
            await db.SaveChangesAsync();
        }

        public async Task<List<Fee>> GetStudentFeesAsync(int studentId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Fees
                .Where(f => f.StudentId == studentId)
                .OrderByDescending(f => f.DueDate)
                .ToListAsync();
        }

        public async Task<List<HostelRoom>> GetAllRoomsAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.HostelRooms.ToListAsync();
        }

        public async Task AddRoomAsync(HostelRoom room)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            db.HostelRooms.Add(room);
            await db.SaveChangesAsync();
        }

        public async Task<List<MenuItem>> GetCafeMenuAsync()
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.MenuItems.OrderBy(m => m.Category).ToListAsync();
        }

        public async Task AddMenuItemAsync(MenuItem item)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            db.MenuItems.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task DeleteFacultyAsync(int id)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var faculty = await db.Faculty.FindAsync(id);
            if (faculty != null)
            {
                db.Faculty.Remove(faculty);
                await db.SaveChangesAsync();
            }
        }

        public async Task<bool> BookHostelRoomAsync(int roomId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var room = await db.HostelRooms.FindAsync(roomId);

            if (room != null && (room.IsOccupied == false || room.IsOccupied == null))
            {
                room.IsOccupied = true;
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}