USE [master]
GO
/****** Object:  Database [TaskDb]    Script Date: 5/21/2023 2:05:21 AM ******/
CREATE DATABASE [TaskDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaskDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TaskDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TaskDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TaskDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TaskDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TaskDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TaskDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TaskDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TaskDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TaskDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TaskDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [TaskDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TaskDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TaskDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TaskDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TaskDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TaskDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TaskDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TaskDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TaskDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TaskDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TaskDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TaskDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TaskDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TaskDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TaskDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TaskDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TaskDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TaskDb] SET RECOVERY FULL 
GO
ALTER DATABASE [TaskDb] SET  MULTI_USER 
GO
ALTER DATABASE [TaskDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TaskDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TaskDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TaskDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TaskDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TaskDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TaskDb', N'ON'
GO
ALTER DATABASE [TaskDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [TaskDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TaskDb]
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 5/21/2023 2:05:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_Drivers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[ADDNewDriver]    Script Date: 5/21/2023 2:05:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ADDNewDriver] 
	-- Add the parameters for the stored procedure here
	(
@FName NVARCHAR(MAX),
@LName NVARCHAR(MAX),
@Mobile NVARCHAR(20),
@EmailId NVARCHAR(MAX),
@ReturnCode INT OUTPUT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

     SET @ReturnCode = 200

    IF EXISTS (SELECT 1 FROM Drivers WHERE Email = @EmailId)
    BEGIN
        SET @ReturnCode = 201
        RETURN
    END

    INSERT INTO Drivers(FirstName,LastName,Email,PhoneNumber)
    VALUES (@FName,@LName,@EmailId,@Mobile)

    SET @ReturnCode = 200
    
	
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteDriver]    Script Date: 5/21/2023 2:05:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteDriver] 
	-- Add the parameters for the stored procedure here
(
@Id INT,
@ReturnCode INT OUTPUT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SET @ReturnCode = 200
    IF NOT EXISTS (SELECT 1 FROM Drivers WHERE Id = @Id)
    BEGIN
        SET @ReturnCode =203
        RETURN
    END
    ELSE
    BEGIN
        DELETE FROM Drivers WHERE Id = @Id
        SET @ReturnCode = 200
        RETURN
    END
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllDrivers]    Script Date: 5/21/2023 2:05:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllDrivers]
(@page INT,
 @size INT,
 @totalrow INT  OUTPUT
 )
AS
BEGIN

DECLARE @offset INT

 SET @offset = (@page-1)*@size;
   

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT Id, FirstName, LastName, Email, PhoneNumber 
	FROM Drivers
    ORDER BY Id DESC
    OFFSET @offset ROWS FETCH NEXT @size ROWS ONLY
	
	 SELECT @totalrow = COUNT(Id) FROM Drivers
END
GO
/****** Object:  StoredProcedure [dbo].[GetDriverbyId]    Script Date: 5/21/2023 2:05:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDriverbyId] (@Id INT,
@ReturnCode INT OUTPUT)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT Id, FirstName, LastName, Email, PhoneNumber 
	FROM Drivers where Id = @Id
	SET @ReturnCode = 200
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateDriver]    Script Date: 5/21/2023 2:05:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateDriver]
@Id INT,
@FName NVARCHAR(MAX),
@LName NVARCHAR(MAX),
@Mobile NVARCHAR(20),
@EmailId NVARCHAR(MAX),
@ReturnCode INT OUTPUT
AS
BEGIN
SET @ReturnCode = 200
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF(@Id <> 0)
    BEGIN
       IF NOT EXISTS (SELECT 1 FROM Drivers WHERE Id = @Id)
        BEGIN
            SET @ReturnCode = 203
            RETURN
        END

        UPDATE Drivers SET
        FirstName = @FName,
        LastName = @LName,
        PhoneNumber = @Mobile,
        Email = @EmailId
        WHERE Id = @Id

        SET @ReturnCode = 200

    END
END
GO
USE [master]
GO
ALTER DATABASE [TaskDb] SET  READ_WRITE 
GO
