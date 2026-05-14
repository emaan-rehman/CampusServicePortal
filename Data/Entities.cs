using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusServicePortal.Data
{
    public class User
    {
        [Key] public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public virtual string GetUserRole() => "User";
    }

    public class Student : User
    {
        public string RollNumber { get; set; } = string.Empty;
        public override string GetUserRole() => "Student";
    }

    public class Complaint
    {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";

        // Add this field - your Razor code calls it!
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int StudentId { get; set; }

        // Use Student here instead of User to access Student-specific properties like RollNumber
        [ForeignKey("StudentId")]
        public virtual Student? Student { get; set; }
    }

    // New Entities to fix CS0246 errors
    public class TransportRoute
    {
        [Key] public int Id { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
    }

    public class Book
    {
        [Key] public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
    }

    public class CampusEvent
    {
        [Key] public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }

    public class MenuItem
    {
        [Key] public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")] // Fixes the Price truncation warning
        public decimal Price { get; set; }
    }

    public class ExamSchedule
    {
        [Key] public int Id { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
    }
}