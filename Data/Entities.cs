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
        public int UserId { get; set; } 
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }

        public virtual string GetUserRole() => "User";
    }

    public class Student : User
    {
        public string RollNumber { get; set; } = string.Empty; 
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
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? Student { get; set; }
    }

    [Table("Books")] 
    public class Book
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Author { get; set; } 

        public bool IsAvailable { get; set; } = true; 
    }
    public class TransportBooking
    {
        [Key]
        public int BookingId { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
    }

    public class TransportRoute
    {
        [Key]
        public int Id { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public string BusNumber { get; set; } = string.Empty;
        public string Status { get; set; } = "Active";
    }

    public class CampusEvent
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Location { get; set; } 

        public DateTime Date { get; set; } = DateTime.Now;
    }

    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public string? Category { get; set; }
    }


    public class Course
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public int Credits { get; set; }
    }

    public class Enrollment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }
    public class ExamSchedule
    {
        public int Id { get; set; }
        public string SubjectCode { get; set; } = string.Empty; 
        public string SubjectName { get; set; } = string.Empty;
        public DateTime? ExamDate { get; set; }
        public string? Room { get; set; } 
    }
    public class Faculty
    {
        public int FacultyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Department { get; set; }
        public string? Email { get; set; }
    }

    public class Fee
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsPaid { get; set; }
    }

    public class HostelRoom
    {
        [Key] 
        public int RoomId { get; set; }

        public string? RoomNumber { get; set; }
        public int? Capacity { get; set; }
        public bool? IsOccupied { get; set; }
    }
}