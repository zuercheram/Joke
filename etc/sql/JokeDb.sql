CREATE DATABASE JokesApp;
GO

USE JokesApp;
GO

DROP TABLE IF EXISTS Jokes
GO

CREATE TABLE Jokes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    SourceId NVARCHAR(68) not null,
    Value NVARCHAR(MAX) not null,
    Url NVARCHAR(255),    
    IconUrl NVARCHAR(255),
    CreatedAt DATETIME,
    UpdatedAt DATETIME,
)

