using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampusServicePortal.Data
{
    // Base class for the UML requirement
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Full Name is required")] // Form validation requirement
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Composition: User has a collection (List) of complaints
        public List<Complaint> Complaints { get; set; } = new();

        // Base class virtual method for Polymorphism requirement
        public virtual string GetUserRole() => "General User";
    }

    // Derived class specialization for UML requirement
    public class Student : User
    {
        public string RollNumber { get; set; } = string.Empty;

        // Polymorphic override implementation
        public override string GetUserRole() => "Student";
    }

    public class Complaint
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")] // Server-side validation
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int StudentId { get; set; }
        public User? Student { get; set; }
    }

    public class TransportBooking
    {
        [Key]
        public int BookingId { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public int UserId { get; set; }
    }
}