using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusServicePortal.Data
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }

    public class User
    {
        [Key]
        public int UserId { get; set; } // Matches the 'UserId' column in your DB
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }

        public virtual string GetUserRole() => "User";
    }

    public class Student : User
    {
        public string RollNumber { get; set; } = string.Empty; // Matches DB column

        public override string GetUserRole() => "Student";
    }

    public class Complaint
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // FIXED: Using 'UserId' because your DB doesn't recognize 'StudentId' in this table
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? Student { get; set; }
    }

    [Table("Books")] // Forces EF to plural name, resolving image_177f01 error
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
    }

    // --- Supporting Classes Kept for Consistency ---
    public class TransportBooking
    {
        [Key] // This resolves the InvalidOperationException
        public int BookingId { get; set; }

        // Add these to fix the CS0117/CS1061 missing definition errors
        public string RouteName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
    }

    public class TransportRoute
    {
        [Key]
        public int Id { get; set; }
        public string RouteName { get; set; } = string.Empty;

        // Add this to match your SQL 'BusNumber' column
        public string BusNumber { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
    }

    public class CampusEvent
    {
        [Key] // Prevents the "requires a primary key" exception
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Location { get; set; } // Matches nvarchar(100) in SQL

        public DateTime Date { get; set; } = DateTime.Now;
    }

    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }

    public class ExamSchedule
    {
        [Key]
        public int Id { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
    }

    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
    }

    public class Faculty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }

    public class Fee
    {
        [Key]
        public int Id { get; set; }

        // FIXED: Using 'UserId' here as well to maintain inheritance consistency
        public int UserId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
    }

    public class HostelRoom
    {
        [Key]
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string Block { get; set; } = string.Empty;
        public bool IsOccupied { get; set; }
    }
}