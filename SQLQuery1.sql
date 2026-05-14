-- Core Table for Authentication & Roles [cite: 11, 17]
CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY,
    RoleName NVARCHAR(50) NOT NULL
);

CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(MAX),
    RoleId INT FOREIGN KEY REFERENCES Roles(RoleId)
);

-- Domain Specific Tables [cite: 13, 17]
CREATE TABLE Complaints (
    Id INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(200),
    Description NVARCHAR(MAX),
    Status NVARCHAR(50), -- Pending, Resolved
    StudentId INT FOREIGN KEY REFERENCES Users(UserId),
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE TransportBookings (
    BookingId INT PRIMARY KEY IDENTITY,
    RouteName NVARCHAR(100),
    BookingDate DATETIME,
    UserId INT FOREIGN KEY REFERENCES Users(UserId)
);