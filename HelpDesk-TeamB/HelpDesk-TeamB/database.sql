-- IT Help Desk | SQL Server | İstəyə bağlı: tətbiq EnsureCreated ilə eyni sxemi özü yaradır.
-- Əl ilə yaratsanız: bu skripti işlədin, sonra tətbiqi açın (seed "CHANGE_ME" parolları real hash-lə əvəz edir).
IF DB_ID('HelpDeskDb') IS NULL CREATE DATABASE HelpDeskDb;
GO
USE HelpDeskDb;
GO
CREATE TABLE Users (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  FullName NVARCHAR(100) NOT NULL,
  Email NVARCHAR(150) NOT NULL,
  PasswordHash NVARCHAR(MAX) NOT NULL,
  Role NVARCHAR(MAX) NOT NULL
);
CREATE UNIQUE INDEX IX_Users_Email ON Users(Email);
CREATE TABLE Tickets (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Title NVARCHAR(120) NOT NULL,
  Description NVARCHAR(2000) NOT NULL,
  Category NVARCHAR(20) NOT NULL CHECK (Category IN ('Hardware','Software','Network','Account','Other')),
  Priority NVARCHAR(20) NOT NULL CHECK (Priority IN ('Low','Medium','High','Critical')),
  Status NVARCHAR(20) NOT NULL CHECK (Status IN ('New','InProgress','Resolved','Closed')),
  CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
  UpdatedAt DATETIME2 NULL,
  CreatedById INT NOT NULL REFERENCES Users(Id),
  AssignedToId INT NULL REFERENCES Users(Id)
);
CREATE INDEX IX_Tickets_CreatedById ON Tickets(CreatedById);
CREATE INDEX IX_Tickets_AssignedToId ON Tickets(AssignedToId);
GO
INSERT INTO Users(FullName,Email,PasswordHash,Role) VALUES
 (N'IT Mütəxəssis','it@uni.az','CHANGE_ME','ITSpecialist'),
 (N'Test Tələbə','student@uni.az','CHANGE_ME','Student');
INSERT INTO Tickets(Title,Description,Category,Priority,Status,CreatedById) VALUES
 (N'Wi-Fi qoşulmur',N'Kitabxanada Wi-Fi şəbəkəsinə qoşula bilmirəm.','Network','High','New',2),
 (N'Parol sıfırlama',N'Tələbə portalının parolunu unutmuşam.','Account','Medium','Resolved',2),
 (N'Printer işləmir',N'204-cü otaqdakı printer kağızı tutur.','Hardware','Low','Closed',2);
GO
-- Statistika sorğuları
SELECT COUNT(*) AS Total FROM Tickets;
SELECT Status, COUNT(*) AS Cnt FROM Tickets GROUP BY Status;
SELECT Category, Status, COUNT(*) AS Cnt FROM Tickets GROUP BY Category, Status;
SELECT t.Id, t.Title, u.FullName FROM Tickets t JOIN Users u ON u.Id = t.CreatedById WHERE t.Status = 'New';
