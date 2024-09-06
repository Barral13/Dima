USE [master]
GO

ALTER DATABASE [dima-dev] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE [dima-dev]
GO

"Server=localhost,1433;Database=dima-dev;User ID=sa;Password=Barral#13;Trusted_Connection=False; TrustServerCertificate=True;"