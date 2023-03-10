USE [master]
GO
/****** Object:  Database [FPTBook]    Script Date: 12/24/2022 2:33:56 PM ******/
CREATE DATABASE [FPTBook]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FPTBook', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQL\MSSQL\DATA\FPTBook.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FPTBook_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQL\MSSQL\DATA\FPTBook_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FPTBook] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FPTBook].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FPTBook] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FPTBook] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FPTBook] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FPTBook] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FPTBook] SET ARITHABORT OFF 
GO
ALTER DATABASE [FPTBook] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FPTBook] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FPTBook] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FPTBook] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FPTBook] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FPTBook] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FPTBook] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FPTBook] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FPTBook] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FPTBook] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FPTBook] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FPTBook] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FPTBook] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FPTBook] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FPTBook] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FPTBook] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FPTBook] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FPTBook] SET RECOVERY FULL 
GO
ALTER DATABASE [FPTBook] SET  MULTI_USER 
GO
ALTER DATABASE [FPTBook] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FPTBook] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FPTBook] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FPTBook] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FPTBook] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FPTBook] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'FPTBook', N'ON'
GO
ALTER DATABASE [FPTBook] SET QUERY_STORE = OFF
GO
USE [FPTBook]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/24/2022 2:33:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/24/2022 2:33:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Email] [nvarchar](450) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 12/24/2022 2:33:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[PublicDate] [datetime2](7) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[PictureName] [nvarchar](max) NULL,
	[PicturePath] [nvarchar](max) NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12/24/2022 2:33:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 12/24/2022 2:33:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CusId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[CustomerPicture] [nvarchar](max) NULL,
	[AccountEmail] [nvarchar](450) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221224020014_InitialCreate', N'6.0.7')
GO
INSERT [dbo].[Accounts] ([Email], [Password], [Role]) VALUES (N'Admin@gmail.com', N'202cb962ac59075b964b07152d234b70', N'ADMIN')
INSERT [dbo].[Accounts] ([Email], [Password], [Role]) VALUES (N'duydlgch200068@fpt.edu.vn', N'202cb962ac59075b964b07152d234b70', N'OWNER')
INSERT [dbo].[Accounts] ([Email], [Password], [Role]) VALUES (N'teatardhl@gmail.com', N'202cb962ac59075b964b07152d234b70', N'CUSTOMER')
GO
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([Id], [Title], [Author], [PublicDate], [CategoryID], [PictureName], [PicturePath], [Price]) VALUES (3, N'Life skills for teen', N'Karen Harris', CAST(N'2022-12-14T13:24:00.0000000' AS DateTime2), 1, N'363a4ab5-1e7b-4135-9903-3e767b289e0a_Book1.jpg', N'~/wwwroot/images', 100)
INSERT [dbo].[Book] ([Id], [Title], [Author], [PublicDate], [CategoryID], [PictureName], [PicturePath], [Price]) VALUES (4, N'Life skills for adult children', N'Karen Harris', CAST(N'2022-12-29T13:27:00.0000000' AS DateTime2), 1, N'd3f56318-9697-41d2-baa6-67d708b7a206_Book2.jpg', N'~/wwwroot/images', 150)
INSERT [dbo].[Book] ([Id], [Title], [Author], [PublicDate], [CategoryID], [PictureName], [PicturePath], [Price]) VALUES (5, N'Value Education', N'Kiruba Charles', CAST(N'2022-12-15T13:28:00.0000000' AS DateTime2), 4, N'467c5660-c939-4444-b274-263ddf6e27dd_Edu1.jpg', N'~/wwwroot/images', 125)
INSERT [dbo].[Book] ([Id], [Title], [Author], [PublicDate], [CategoryID], [PictureName], [PicturePath], [Price]) VALUES (6, N'Research in Education', N'John W.Best', CAST(N'2022-12-15T13:28:00.0000000' AS DateTime2), 4, N'a4f2f1b4-a04c-4859-bac7-3d223024efd6_Edu2.jpg', N'~/wwwroot/images', 70)
INSERT [dbo].[Book] ([Id], [Title], [Author], [PublicDate], [CategoryID], [PictureName], [PicturePath], [Price]) VALUES (7, N'Aru Shah', N'Pandawa A.', CAST(N'2022-12-13T13:29:00.0000000' AS DateTime2), 2, N'36f48841-fbfa-45fb-889a-e97bc6aab9f8_novel2.jpg', N'~/wwwroot/images', 80)
SET IDENTITY_INSERT [dbo].[Book] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (1, N'Life Skills')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (2, N'Novel')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (4, N'Education')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CusId], [Name], [DateOfBirth], [Gender], [Email], [Address], [CustomerPicture], [AccountEmail]) VALUES (1, N'CustomerName', CAST(N'2022-12-24T10:01:14.8024671' AS DateTime2), N'Unknown', N'teatardhl@gmail.com', N'CustomerAddress', N'', N'teatardhl@gmail.com')
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
/****** Object:  Index [IX_Book_CategoryID]    Script Date: 12/24/2022 2:33:57 PM ******/
CREATE NONCLUSTERED INDEX [IX_Book_CategoryID] ON [dbo].[Book]
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Customers_AccountEmail]    Script Date: 12/24/2022 2:33:57 PM ******/
CREATE NONCLUSTERED INDEX [IX_Customers_AccountEmail] ON [dbo].[Customers]
(
	[AccountEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Categories_CategoryID] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Categories_CategoryID]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Accounts_AccountEmail] FOREIGN KEY([AccountEmail])
REFERENCES [dbo].[Accounts] ([Email])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Accounts_AccountEmail]
GO
USE [master]
GO
ALTER DATABASE [FPTBook] SET  READ_WRITE 
GO
