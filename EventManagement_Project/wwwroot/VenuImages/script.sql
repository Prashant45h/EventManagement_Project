USE [master]
GO
/****** Object:  Database [DB_EventManagment]    Script Date: 3/1/2024 3:16:05 PM ******/
CREATE DATABASE [DB_EventManagment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_EventManagment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DB_EventManagment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DB_EventManagment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DB_EventManagment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DB_EventManagment] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_EventManagment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_EventManagment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_EventManagment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_EventManagment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_EventManagment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_EventManagment] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_EventManagment] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DB_EventManagment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_EventManagment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_EventManagment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_EventManagment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_EventManagment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_EventManagment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_EventManagment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_EventManagment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_EventManagment] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DB_EventManagment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_EventManagment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_EventManagment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_EventManagment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_EventManagment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_EventManagment] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_EventManagment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_EventManagment] SET RECOVERY FULL 
GO
ALTER DATABASE [DB_EventManagment] SET  MULTI_USER 
GO
ALTER DATABASE [DB_EventManagment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_EventManagment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_EventManagment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_EventManagment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DB_EventManagment] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DB_EventManagment] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DB_EventManagment', N'ON'
GO
ALTER DATABASE [DB_EventManagment] SET QUERY_STORE = OFF
GO
USE [DB_EventManagment]
GO
/****** Object:  Table [dbo].[BookingDetails_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingDetails_Tbl](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[BookingNo] [nvarchar](50) NULL,
	[BookingDate] [date] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[BookingApprovel] [nvarchar](50) NULL,
	[BookingApprovelDate] [datetime] NULL,
	[BookingCompleteFlag] [nvarchar](50) NULL,
 CONSTRAINT [PK_BookingDetails_Tbl] PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingEquipment_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingEquipment_Tbl](
	[BookingEquipmentID] [int] IDENTITY(1,1) NOT NULL,
	[EquipmentID] [int] NULL,
	[Createdby] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[BookingID] [int] NULL,
 CONSTRAINT [PK_BookingEquipment_Tbl] PRIMARY KEY CLUSTERED 
(
	[BookingEquipmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingFlower_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingFlower_Tbl](
	[BookingFlowerID] [int] IDENTITY(1,1) NOT NULL,
	[FlowerID] [int] NULL,
	[Createdby] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[BookingID] [int] NULL,
 CONSTRAINT [PK_BookingFlower_Tbl] PRIMARY KEY CLUSTERED 
(
	[BookingFlowerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingFood_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingFood_Tbl](
	[BookingFoodID] [int] IDENTITY(1,1) NOT NULL,
	[FoodType] [nvarchar](50) NULL,
	[MealType] [nvarchar](50) NULL,
	[DishType] [nvarchar](50) NULL,
	[DishName] [int] NULL,
	[Createdby] [int] NULL,
	[CeatedDate] [datetime] NULL,
	[BookingID] [int] NULL,
 CONSTRAINT [PK_BookingFood_Tbl] PRIMARY KEY CLUSTERED 
(
	[BookingFoodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingLights_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingLights_Tbl](
	[BookingLightID] [int] IDENTITY(1,1) NOT NULL,
	[LightType] [nvarchar](50) NULL,
	[LightIDSelected] [int] NULL,
	[BookingID] [int] NULL,
	[Createdby] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_BookingLights_Tbl] PRIMARY KEY CLUSTERED 
(
	[BookingLightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingVenu_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingVenu_Tbl](
	[BookingVenuID] [int] IDENTITY(1,1) NOT NULL,
	[VenuID] [int] NULL,
	[EventTypeID] [int] NULL,
	[GuestCount] [int] NULL,
	[Createdby] [int] NULL,
	[Createdate] [datetime] NULL,
	[BookingID] [int] NOT NULL,
 CONSTRAINT [PK_BookingVenu_Tbl_1] PRIMARY KEY CLUSTERED 
(
	[BookingVenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City_Tbl](
	[City_ID] [int] IDENTITY(1,1) NOT NULL,
	[City_Name] [nvarchar](50) NULL,
	[State_ID] [int] NULL,
 CONSTRAINT [PK_City_Tbl] PRIMARY KEY CLUSTERED 
(
	[City_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country_Tbl](
	[Country_ID] [int] IDENTITY(1,1) NOT NULL,
	[Country_Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Country_Tbl] PRIMARY KEY CLUSTERED 
(
	[Country_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dishtypes]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dishtypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Dishtype] [varchar](50) NULL,
 CONSTRAINT [PK_Dishtypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipment_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipment_Tbl](
	[EquipmentID] [int] IDENTITY(1,1) NOT NULL,
	[EquipmentName] [nvarchar](50) NULL,
	[EquipmentFilename] [nvarchar](50) NULL,
	[EquipmentFilepath] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[Createdate] [datetime] NULL,
	[EquipmentCost] [int] NULL,
 CONSTRAINT [PK_Equipment_Tbl] PRIMARY KEY CLUSTERED 
(
	[EquipmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventType_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventType_Tbl](
	[EventID] [int] IDENTITY(1,1) NOT NULL,
	[EventType] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_EventType_Tbl] PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flower_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flower_Tbl](
	[FlowerID] [int] IDENTITY(1,1) NOT NULL,
	[FlowerName] [nvarchar](50) NULL,
	[FlowerFilename] [nvarchar](50) NULL,
	[FlowerFilepath] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[Createdate] [datetime] NULL,
	[FlowerCost] [int] NULL,
 CONSTRAINT [PK_Flower_Tbl] PRIMARY KEY CLUSTERED 
(
	[FlowerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food_Tbl](
	[FoodID] [int] IDENTITY(1,1) NOT NULL,
	[FoodType] [nvarchar](50) NULL,
	[MealType] [nvarchar](50) NULL,
	[DishType] [nvarchar](max) NULL,
	[FoodName] [nvarchar](50) NULL,
	[FoodFileName] [nvarchar](50) NULL,
	[FoodFilepath] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[Createdate] [datetime] NULL,
	[FoodCost] [int] NULL,
 CONSTRAINT [PK_Food_Tbl] PRIMARY KEY CLUSTERED 
(
	[FoodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Light_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Light_Tbl](
	[LightID] [int] IDENTITY(1,1) NOT NULL,
	[LightType] [nvarchar](50) NULL,
	[LightName] [nvarchar](50) NULL,
	[LightFilename] [nvarchar](50) NULL,
	[LightFilepath] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[Createdate] [datetime] NULL,
	[LightCost] [int] NULL,
 CONSTRAINT [PK_Light_Tbl] PRIMARY KEY CLUSTERED 
(
	[LightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registartion_Table]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registartion_Table](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[City] [int] NULL,
	[State] [int] NULL,
	[Country] [int] NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Email_ID] [nvarchar](50) NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[ConfirmPassword] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NULL,
	[BirthDate] [datetime] NULL,
	[RoleID] [int] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_Registartion_Table] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role_Tbl](
	[Role_ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role_Tbl] PRIMARY KEY CLUSTERED 
(
	[Role_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State_Tbl](
	[State_ID] [int] IDENTITY(1,1) NOT NULL,
	[State_Name] [nvarchar](50) NULL,
	[Country_ID] [int] NULL,
 CONSTRAINT [PK_State_Tbl] PRIMARY KEY CLUSTERED 
(
	[State_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venu_Tbl]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venu_Tbl](
	[VenuID] [int] IDENTITY(1,1) NOT NULL,
	[VenuName] [nvarchar](50) NULL,
	[VenuFilename] [nvarchar](max) NULL,
	[VenuFilepath] [nvarchar](max) NULL,
	[Cretatedby] [int] NULL,
	[Createdate] [datetime] NULL,
	[VenuCost] [int] NULL,
 CONSTRAINT [PK_Venu_Tbl] PRIMARY KEY CLUSTERED 
(
	[VenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[City_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_City_Tbl_State_Tbl] FOREIGN KEY([State_ID])
REFERENCES [dbo].[State_Tbl] ([State_ID])
GO
ALTER TABLE [dbo].[City_Tbl] CHECK CONSTRAINT [FK_City_Tbl_State_Tbl]
GO
ALTER TABLE [dbo].[Registartion_Table]  WITH CHECK ADD  CONSTRAINT [FK_Registartion_Table_Role_Tbl] FOREIGN KEY([City])
REFERENCES [dbo].[City_Tbl] ([City_ID])
GO
ALTER TABLE [dbo].[Registartion_Table] CHECK CONSTRAINT [FK_Registartion_Table_Role_Tbl]
GO
ALTER TABLE [dbo].[State_Tbl]  WITH CHECK ADD  CONSTRAINT [FK_State_Tbl_Country_Tbl] FOREIGN KEY([Country_ID])
REFERENCES [dbo].[Country_Tbl] ([Country_ID])
GO
ALTER TABLE [dbo].[State_Tbl] CHECK CONSTRAINT [FK_State_Tbl_Country_Tbl]
GO
/****** Object:  StoredProcedure [dbo].[approvedata]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[approvedata]
    @BookingNo NVARCHAR(50),
	@BookingDate Datetime,
    @BookingApprovel NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BookingDetails_Tbl 

    SET BookingDate = @BookingDate,
	    BookingApprovel = @BookingApprovel,
        BookingApprovelDate = GETDATE()
    WHERE BookingNo = @BookingNo;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_eqipmentbooking]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_eqipmentbooking]
    @EquipmentID INT,
    @CreatedBy INT,
    @BookingID INT OUTPUT 
AS
BEGIN
    SELECT TOP 1 @BookingID = BookingID 
    FROM BookingDetails_Tbl 
    ORDER BY BookingID DESC;

    INSERT INTO BookingEquipment_Tbl (EquipmentID, Createdby, CreatedDate, BookingID)
    VALUES (@EquipmentID, @CreatedBy, GETDATE(), @BookingID);
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_Flowerbooking]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_Flowerbooking]
    @FlowerID INT,
    @Createdby INT,
    @BookingID INT OUTPUT 
AS
BEGIN
    SELECT TOP 1 @BookingID = BookingID 
    FROM BookingDetails_Tbl 
    ORDER BY BookingID DESC;

    INSERT INTO BookingFlower_Tbl(FlowerID,CreatedBy,BookingID,CreatedDate)
    VALUES (@FlowerID,@CreatedBy,@BookingID,GETDATE());
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_Foodbooking]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Foodbooking]
    @FoodType nvarchar(50),
	@MealType nvarchar(50),
	@DishType nvarchar(50),
	@DishName INT,
    @CreatedBy INT,
    @BookingID INT OUTPUT 
AS
BEGIN
    SELECT TOP 1 @BookingID = BookingID 
    FROM BookingDetails_Tbl 
    ORDER BY BookingID DESC;

    INSERT INTO BookingFood_Tbl (FoodType,MealType,DishType,DishName,CreatedBy,CeatedDate,BookingID)
    VALUES (@FoodType,@MealType, @DishType,@DishName,@CreatedBy, GETDATE(), @BookingID);
END;
GO
/****** Object:  StoredProcedure [dbo].[Sp_Insertdataforsignup]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_Insertdataforsignup]
    @Name nvarchar(50),
    @Address nvarchar(50),
    @City int,
    @State int,
    @Country int,
    @Mobile_No nvarchar(50),
    @Email_ID nvarchar(50),
    @Username nvarchar(50),
    @Password nvarchar(50),
    @ConfirmPassword nvarchar(50),
    @Gender nvarchar(50),
    @BirthDate datetime,
    @RoleID int
AS
BEGIN
    INSERT INTO [dbo].[Registartion_Table] (
        Name,
        Address,
        City,
        State,
        Country,
        Mobile_No,
        Email_ID,
        Username,
        Password,
        ConfirmPassword,
        Gender,
        BirthDate,
        RoleID,
        CreatedOn
    )
    VALUES (
        @Name,
        @Address,
        @City,
        @State,
        @Country,
        @Mobile_No,
        @Email_ID,
        @Username,
        @Password,
        @ConfirmPassword,
        @Gender,
        @BirthDate,
        @RoleID,
		GETDATE()
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertEquipment]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_InsertEquipment]
(
    @EquipmentName NVARCHAR(50),
    @EquipmentFilename NVARCHAR(MAX),
    @EquipmentFilepath NVARCHAR(MAX),
    @CreatedBy INT,
    @EquipmentCost INT
)
AS
BEGIN
    INSERT INTO [dbo].[Equipment_Tbl]
    (
        EquipmentName,
        EquipmentFilename,
        EquipmentFilepath,
        CreatedBy,
        Createdate,
        EquipmentCost
    )
    VALUES
    (
        @EquipmentName,
        @EquipmentFilename,
        @EquipmentFilepath,
        @CreatedBy,
		GETDATE(),
        @EquipmentCost
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertFlower]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_InsertFlower]
    @FlowerName NVARCHAR(50),
    @FlowerFileName NVARCHAR(50),
    @FlowerFilepath NVARCHAR(50),
    @CreatedBy INT,
    @FlowerCost INT
AS
BEGIN

    INSERT INTO [dbo].[Flower_Tbl] (
        [FlowerName],
        [FlowerFileName],
        [FlowerFilepath],
        [CreatedBy],
        [Createdate],
        [FlowerCost]
    )
    VALUES (
        @FlowerName,
        @FlowerFileName,
        @FlowerFilepath,
        @CreatedBy,
        GETDATE(),
        @FlowerCost
    );
END
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertFood]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_InsertFood]
(
    @FoodType NVARCHAR(50),
    @MealType NVARCHAR(50),
    @DishType NVARCHAR(50),
    @FoodName NVARCHAR(50),
    @FoodFileName NVARCHAR(MAX),
    @FoodFilepath NVARCHAR(MAX),
    @CreatedBy INT,
    @FoodCost INT
)
AS
BEGIN

    INSERT INTO [dbo].[Food_Tbl]
    (
        [FoodType],
        [MealType],
        [DishType],
        [FoodName],
        [FoodFileName],
        [FoodFilepath],
        [CreatedBy],
        [Createdate],
        [FoodCost]
    )
    VALUES
    (
        @FoodType,
        @MealType,
        @DishType,
        @FoodName,
        @FoodFileName,
        @FoodFilepath,
        @CreatedBy,
		GETDATE(),
        @FoodCost
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertLight]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_InsertLight]
    @LightType NVARCHAR(50),
    @LightName NVARCHAR(50),
    @LightFilename NVARCHAR(50),
    @LightFilepath NVARCHAR(50),
    @CreatedBy INT,
    @LightCost INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Light_Tbl] ([LightType], [LightName], [LightFilename], [LightFilepath], [CreatedBy], [Createdate], [LightCost])
    VALUES (@LightType, @LightName, @LightFilename, @LightFilepath, @CreatedBy, GETDATE(), @LightCost);
END
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertVenu]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_InsertVenu]
    @VenuName NVARCHAR(50),
    @VenuFilename NVARCHAR(MAX),
    @VenuFilepath NVARCHAR(MAX),
	@Cretatedby int,
    @VenuCost INT
AS
BEGIN


    INSERT INTO Venu_Tbl (VenuName, VenuFilename, VenuFilepath,Cretatedby,Createdate, VenuCost)
    VALUES (@VenuName, @VenuFilename, @VenuFilepath,@Cretatedby, GETDATE(), @VenuCost);
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_Lightbooking]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Lightbooking]
    @LightType nvarchar(50),
	@LightIDSelected INT,
    @CreatedBy INT,
    @BookingID INT OUTPUT 
AS
BEGIN
    SELECT TOP 1 @BookingID = BookingID 
    FROM BookingDetails_Tbl 
    ORDER BY BookingID DESC;

    INSERT INTO BookingLights_Tbl(LightType,LightIDSelected,BookingID,CreatedBy,CreatedDate)
    VALUES (@LightType,@LightIDSelected,@BookingID,@CreatedBy,GETDATE());
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_SaveBookingVenue]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SaveBookingVenue]
    @VenuID INT,
    @EventTypeID INT,
    @GuestCount INT,
    @CreatedBy INT,
    @Createdate DATETIME,
    @BookingID INT OUTPUT
AS
BEGIN
    DECLARE @NewBookingID INT
    DECLARE @BookingNo NVARCHAR(50)

	    SET @BookingNo = 'BK-' + CAST(YEAR(GETDATE()) AS NVARCHAR(4)) + '-' + CAST((SELECT ISNULL(MAX(BookingID), 0) + 1 FROM BookingDetails_Tbl) AS NVARCHAR(10))


    INSERT INTO [dbo].[BookingDetails_Tbl] (BookingNo,[CreatedBy], [BookingDate],[CreatedDate],[BookingApprovel])
    VALUES (@BookingNo,@CreatedBy, CONVERT(date, @Createdate), GETDATE(), 'Pending') 

    SET @NewBookingID = SCOPE_IDENTITY()


    INSERT INTO [dbo].[BookingVenu_Tbl] (VenuID, EventTypeID, GuestCount, Createdby, Createdate, BookingID)
    VALUES (@VenuID, @EventTypeID, @GuestCount, @CreatedBy, @Createdate, @NewBookingID);
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateEquipment]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateEquipment]
(
    @EquipmentID INT,
    @EquipmentName NVARCHAR(50),
    @EquipmentFilename NVARCHAR(MAX),
    @EquipmentFilepath NVARCHAR(MAX),
    @CreatedBy INT,
    @EquipmentCost INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Equipment_Tbl]
    SET
        [EquipmentName] = @EquipmentName,
        [EquipmentFilename] = @EquipmentFilename,
        [EquipmentFilepath] = @EquipmentFilepath,
        [CreatedBy] = @CreatedBy,
        [Createdate] = GETDATE(),
        [EquipmentCost] = @EquipmentCost
    WHERE
        [EquipmentID] = @EquipmentID;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateFood]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateFood]
    @FoodID INT,
    @FoodType NVARCHAR(50),
    @MealType NVARCHAR(50),
    @DishType NVARCHAR(MAX),
    @FoodName NVARCHAR(50),
    @FoodFileName NVARCHAR(50),
    @FoodFilepath NVARCHAR(50),
    @CreatedBy INT,
    @FoodCost INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Food_Tbl]
    SET 
        [FoodType] = @FoodType,
        [MealType] = @MealType,
        [DishType] = @DishType,
        [FoodName] = @FoodName,
        [FoodFileName] = @FoodFileName,
        [FoodFilepath] = @FoodFilepath,
        [CreatedBy] = @CreatedBy,
        [Createdate] = GETDATE(),
        [FoodCost] = @FoodCost
    WHERE
        [FoodID] = @FoodID;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateVenu]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateVenu]
    @VenuID INT,
    @VenuName NVARCHAR(50),
    @VenuFilename NVARCHAR(50),
    @VenuFilepath NVARCHAR(50),
    @Cretatedby INT,
    @VenuCost INT
AS
BEGIN
    UPDATE [dbo].[Venu_Tbl]
    SET
        [VenuName] = @VenuName,
        [VenuFilename] = @VenuFilename,
        [VenuFilepath] = @VenuFilepath,
        [Cretatedby] = @Cretatedby,
        [Createdate] = GETDATE(),
        [VenuCost] = @VenuCost
    WHERE
        [VenuID] = @VenuID;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateFlower]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateFlower]
    @FlowerID INT,
    @FlowerName NVARCHAR(50),
    @FlowerFileName NVARCHAR(50),
    @FlowerFilepath NVARCHAR(50),
    @CreatedBy INT,
    @FlowerCost INT
AS
BEGIN

    UPDATE [dbo].[Flower_Tbl]
    SET 
        [FlowerName] = @FlowerName,
        [FlowerFileName] = @FlowerFileName,
        [FlowerFilepath] = @FlowerFilepath,
        [CreatedBy] = @CreatedBy,
        [Createdate] = GETDATE(),
        [FlowerCost] = @FlowerCost
    WHERE
        [FlowerID] = @FlowerID;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateLight]    Script Date: 3/1/2024 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateLight]
    @LightID INT,
    @LightType NVARCHAR(50),
    @LightName NVARCHAR(50),
    @LightFileName NVARCHAR(50),
    @LightFilepath NVARCHAR(50),
    @CreatedBy INT,
    @LightCost INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Light_Tbl]
    SET 
        [LightType] = @LightType,
        [LightName] = @LightName,
        [LightFileName] = @LightFileName,
        [LightFilepath] = @LightFilepath,
        [CreatedBy] = @CreatedBy,
        [Createdate] = GETDATE(),
        [LightCost] = @LightCost
    WHERE
        [LightID] = @LightID;
END
GO
USE [master]
GO
ALTER DATABASE [DB_EventManagment] SET  READ_WRITE 
GO
