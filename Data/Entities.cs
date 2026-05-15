using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusServicePortal.Data
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }

    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Foreign Key for Role
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }

        // Marked virtual to allow override in Student class (Fixes CS0115)
        public virtual string GetUserRole() => "User";
    }

    public class Student : User
    {
        public string RollNumber { get; set; } = string.Empty;

        // Successfully overrides base method
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

        // Linking to User (matches your DB Users table)
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? Student { get; set; }
    }

    public class TransportRoute
    {
        [Key]
        public int Id { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
    }

    public class TransportBooking
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RouteId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
    }

    [Table("Books")] // Forces EF Core to look for plural "Books" in SQL
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
    }

    public class CampusEvent
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
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
        public int StudentId { get; set; }

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