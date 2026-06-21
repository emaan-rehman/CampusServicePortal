# Smart Campus Service Portal

## Overview

Smart Campus Service Portal is a comprehensive campus management system developed using Blazor, ASP.NET Core, Entity Framework Core, and SQL Server. The platform centralizes various university services into a single digital ecosystem, enabling students and administrators to manage academic and campus-related activities efficiently.

The system digitizes traditional campus operations such as course management, complaint handling, library services, transport management, hostel management, fee management, examination schedules, faculty management, and event management.

---

## Key Features

### Authentication & Security

* Secure User Authentication
* Role-Based Access Control
* Session Management
* Authorization & Access Restrictions

### Student Portal

* Personalized Dashboard
* Course Enrollment
* Library Services
* Transport Services
* Examination Schedules
* Fee Management
* Hostel Management
* Campus Events
* Complaint Submission & Tracking

### Administrative Portal

* Dashboard Analytics
* Faculty Management
* Course Management
* Library Administration
* Event Management
* Examination Management
* Hostel Management
* Café Management
* Role Management
* Complaint Monitoring

### Technical Features

* Responsive User Interface
* Repository Pattern Implementation
* Entity Framework Core Integration
* Modular Architecture
* Database Relationship Management
* Error Handling & Validation

---

## Technologies Used

| Technology            | Purpose                 |
| --------------------- | ----------------------- |
| Blazor                | Frontend Framework      |
| ASP.NET Core          | Backend Framework       |
| C#                    | Programming Language    |
| Entity Framework Core | ORM                     |
| SQL Server / LocalDB  | Database                |
| Bootstrap             | Responsive UI           |
| Razor Components      | Dynamic Pages           |
| Visual Studio         | Development Environment |

---

## System Architecture

The application follows a layered architecture:

Presentation Layer (Blazor UI)

↓

Service Layer

↓

Repository Layer

↓

Database Layer

This architecture improves maintainability, scalability, and separation of concerns.

---

## Project Structure

```text
CampusServicePortal
│
├── Pages
├── Data
│   ├── AppDbContext.cs
│   └── Entities.cs
│
├── Repositories
│   └── CampusRepository.cs
│
├── Services
│   └── UserSession.cs
│
└── wwwroot
```

---

## Main Modules

### Student Modules

* Dashboard
* Courses
* Library
* Transport
* Exams
* Fee Management
* Hostel
* Events
* Complaint Portal

### Administrative Modules

* Dashboard
* Faculty Management
* Course Management
* Library Management
* Event Management
* Exams Management
* Hostel Management
* Café Management
* Role Management

---

## Database Features

* User Management
* Role Management
* Course Enrollment
* Complaint Records
* Fee Records
* Faculty Records
* Event Records
* Library Records
* Transport Records
* Hostel Records

---

## Screenshots

Screenshots and demonstrations can be provided upon request. The project contains multiple modules including authentication, course management, complaint handling, library services, transport management, hostel management, fee management, and event management.
---

## Installation

### Clone Repository

```bash
git clone https://github.com/emaan-rehman/CampusServicePortal.git
```

### Navigate to Project

```bash
cd CampusServicePortal
```

### Restore Packages

```bash
dotnet restore
```

### Configure Database

Update the SQL Server connection string inside:

```text
appsettings.json
```

### Apply Migrations

```bash
Update-Database
```

### Run Application

```bash
dotnet run
```

---

## Future Enhancements

* Online Fee Payment Integration
* Notification System
* Email Verification
* Attendance Management
* Mobile Application Support
* Real-Time Chat System
* AI-Based Complaint Analysis

---

## Learning Outcomes

This project demonstrates:

* Blazor Development
* ASP.NET Core Development
* Entity Framework Core
* SQL Server Integration
* Repository Pattern
* Role-Based Authentication
* Session Management
* Responsive Web Design
* Software Architecture
* Database Design

---

## Team Members

* Emaan Rehman
* Waduha Afzal
* Eman Kashif
* Maarij Nadeem

---

## Author

Emaan Rehman

BS Computer Science Student

GitHub: https://github.com/emaan-rehman
LinkedIn: https://www.linkedin.com/in/emaan-rehman
