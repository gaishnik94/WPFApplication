# WPFApplication
This is my WPF Apptication. 
I translated all labels and dialog messages into English.

It used for heating systems. With its help, calculation of heating losses is performed and generating reports on heat loss.

## Required
* MS SQL Server
* MS Excel (for reports)

## Used
* WPF
* EntityFramework
* mahapps metro

## Script for database

```sql
USE [master]
GO
/****** Object:  Database [Teploseti]    Script Date: 27.06.2018 17:49:04 ******/
CREATE DATABASE [Teploseti]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Teploseti', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Teploseti.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Teploseti_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Teploseti_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Teploseti] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Teploseti].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Teploseti] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Teploseti] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Teploseti] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Teploseti] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Teploseti] SET ARITHABORT OFF 
GO
ALTER DATABASE [Teploseti] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Teploseti] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Teploseti] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Teploseti] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Teploseti] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Teploseti] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Teploseti] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Teploseti] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Teploseti] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Teploseti] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Teploseti] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Teploseti] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Teploseti] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Teploseti] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Teploseti] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Teploseti] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Teploseti] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Teploseti] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Teploseti] SET RECOVERY FULL 
GO
ALTER DATABASE [Teploseti] SET  MULTI_USER 
GO
ALTER DATABASE [Teploseti] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Teploseti] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Teploseti] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Teploseti] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Teploseti', N'ON'
GO
USE [Teploseti]
GO
/****** Object:  Table [dbo].[Аккаунты]    Script Date: 27.06.2018 17:49:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Аккаунты](
	[id_аккаунта] [int] IDENTITY(1,1) NOT NULL,
	[Логин] [varchar](50) NOT NULL,
	[Хэш_пароль] [varchar](max) NOT NULL,
	[Администратор] [bit] NOT NULL,
 CONSTRAINT [PK_Аккаунты] PRIMARY KEY CLUSTERED 
(
	[id_аккаунта] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Коэффициенты теплоотдачи]    Script Date: 27.06.2018 17:49:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Коэффициенты теплоотдачи](
	[id_коэф_тепл_отдачи] [int] IDENTITY(1,1) NOT NULL,
	[Номер_СНиП] [varchar](max) NULL,
	[t_до] [float] NULL,
	[t_после] [float] NULL,
	[Тип_поверхности] [varchar](max) NULL,
	[Коэфф_помещения_с_малым_к_изл] [float] NULL,
	[Коэфф_помещения_с_высоким_к_изл] [float] NULL,
	[Коэфф_откр_с_малым_к_изл] [float] NULL,
	[Коэфф_откр_с_высоким_к_изл] [float] NULL,
 CONSTRAINT [PK_Коэффициенты учитыв. тепловые потери] PRIMARY KEY CLUSTERED 
(
	[id_коэф_тепл_отдачи] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Коэффициенты учитыв. тепловые потери]    Script Date: 27.06.2018 17:49:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Коэффициенты учитыв. тепловые потери](
	[id_коэфф_тепл_потери] [int] IDENTITY(1,1) NOT NULL,
	[Номер_СНиП] [varchar](max) NOT NULL,
	[id_материала] [int] NOT NULL,
	[Нач_диаметр] [float] NOT NULL,
	[Кон_диаметр] [float] NOT NULL,
	[Коэффициент] [float] NOT NULL,
 CONSTRAINT [PK_Коэффициенты теплоотдачи] PRIMARY KEY CLUSTERED 
(
	[id_коэфф_тепл_потери] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Материал]    Script Date: 27.06.2018 17:49:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Материал](
	[id_материала] [int] IDENTITY(1,1) NOT NULL,
	[Наименование_материала] [varchar](max) NOT NULL,
	[Коэффициент_теплопроводности_материал] [float] NOT NULL,
	[Наименование_изоляции] [varchar](max) NULL,
	[Коэффициент_теплопроводности_изоляция] [float] NOT NULL,
 CONSTRAINT [PK_Студ] PRIMARY KEY CLUSTERED 
(
	[id_материала] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Обслуживание]    Script Date: 27.06.2018 17:49:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Обслуживание](
	[id_обслуживания] [int] IDENTITY(1,1) NOT NULL,
	[Код_трубы] [nvarchar](20) NOT NULL,
	[Номер_бригады] [varchar](max) NOT NULL,
	[Результат] [varchar](max) NOT NULL,
	[Дата] [date] NOT NULL,
	[id_сотрудника] [int] NOT NULL,
 CONSTRAINT [PK_Обслуживание] PRIMARY KEY CLUSTERED 
(
	[id_обслуживания] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Расчетные потери]    Script Date: 27.06.2018 17:49:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Расчетные потери](
	[id_расчетных_потерь] [int] IDENTITY(1,1) NOT NULL,
	[t_воды] [float] NOT NULL,
	[t_среды] [float] NOT NULL,
	[id_коэфф_тепл_отдачи] [int] NOT NULL,
	[Номер_коэфф] [int] NOT NULL,
	[id_коэф_тепл_потери] [int] NOT NULL,
	[Дата] [date] NULL,
	[Код_трубы] [nvarchar](20) NOT NULL,
	[id_сотрудника] [int] NOT NULL,
	[Результат] [float] NOT NULL,
	[Дата_расчета] [datetime] NULL,
 CONSTRAINT [PK_Расчетные потери] PRIMARY KEY CLUSTERED 
(
	[id_расчетных_потерь] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Сотрудники]    Script Date: 27.06.2018 17:49:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Сотрудники](
	[id_сотрудника] [int] IDENTITY(1,1) NOT NULL,
	[Фамилия] [varchar](50) NOT NULL,
	[Имя] [varchar](50) NOT NULL,
	[Отчество] [varchar](50) NULL,
	[Должность] [varchar](50) NOT NULL,
	[Паспортные_данные] [nvarchar](50) NOT NULL,
	[id_аккаунта] [int] NOT NULL,
 CONSTRAINT [PK_Сотрудники] PRIMARY KEY CLUSTERED 
(
	[id_сотрудника] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Труба]    Script Date: 27.06.2018 17:49:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Труба](
	[Код_трубы] [nvarchar](20) NOT NULL,
	[Длина] [int] NOT NULL,
	[d_внутр_материал] [float] NOT NULL,
	[d_наруж_материал] [float] NOT NULL,
	[d_внутр_изол] [float] NOT NULL,
	[d_наруж_изол] [float] NOT NULL,
	[id_материала] [int] NOT NULL,
	[id_участка] [int] NOT NULL,
 CONSTRAINT [PK_Труба_1] PRIMARY KEY CLUSTERED 
(
	[Код_трубы] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Участок]    Script Date: 27.06.2018 17:49:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Участок](
	[id_участка] [int] IDENTITY(1,1) NOT NULL,
	[Наименование] [varchar](max) NOT NULL,
	[Населенный_пункт] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Участок] PRIMARY KEY CLUSTERED 
(
	[id_участка] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Фактические потери]    Script Date: 27.06.2018 17:49:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Фактические потери](
	[id_факт_потерь] [int] IDENTITY(1,1) NOT NULL,
	[Значение] [float] NOT NULL,
	[Дата] [date] NOT NULL,
	[Код_трубы] [nvarchar](20) NOT NULL,
	[id_сотрудника] [int] NOT NULL,
 CONSTRAINT [PK_Фактические потери] PRIMARY KEY CLUSTERED 
(
	[id_факт_потерь] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Аккаунты] ON 

GO
INSERT [dbo].[Аккаунты] ([id_аккаунта], [Логин], [Хэш_пароль], [Администратор]) VALUES (1, N'admin', N'21-23-2F-29-7A-57-A5-A7-43-89-4A-0E-4A-80-1F-C3', 1)
GO
INSERT [dbo].[Аккаунты] ([id_аккаунта], [Логин], [Хэш_пароль], [Администратор]) VALUES (2, N'savko', N'21-23-2F-29-7A-57-A5-A7-43-89-4A-0E-4A-80-1F-C3', 0)
GO
INSERT [dbo].[Аккаунты] ([id_аккаунта], [Логин], [Хэш_пароль], [Администратор]) VALUES (3, N'gg', N'B4-F5-69-CB-F3-3D-D8-FF-A2-06-BA-A0-3F-68-17-CE', 0)
GO
SET IDENTITY_INSERT [dbo].[Аккаунты] OFF
GO
SET IDENTITY_INSERT [dbo].[Коэффициенты теплоотдачи] ON 

GO
INSERT [dbo].[Коэффициенты теплоотдачи] ([id_коэф_тепл_отдачи], [Номер_СНиП], [t_до], [t_после], [Тип_поверхности], [Коэфф_помещения_с_малым_к_изл], [Коэфф_помещения_с_высоким_к_изл], [Коэфф_откр_с_малым_к_изл], [Коэфф_откр_с_высоким_к_изл]) VALUES (1, N'2.04.014', NULL, 20, N'Плоская поверхность, оборудование, вертикальные трубопроводы', 6, 11, 6, 11)
GO
INSERT [dbo].[Коэффициенты теплоотдачи] ([id_коэф_тепл_отдачи], [Номер_СНиП], [t_до], [t_после], [Тип_поверхности], [Коэфф_помещения_с_малым_к_изл], [Коэфф_помещения_с_высоким_к_изл], [Коэфф_откр_с_малым_к_изл], [Коэфф_откр_с_высоким_к_изл]) VALUES (2, N'2.04.014', NULL, 20, N'Горизонтальные трубопроводы', 6, 10, 6, 10)
GO
INSERT [dbo].[Коэффициенты теплоотдачи] ([id_коэф_тепл_отдачи], [Номер_СНиП], [t_до], [t_после], [Тип_поверхности], [Коэфф_помещения_с_малым_к_изл], [Коэфф_помещения_с_высоким_к_изл], [Коэфф_откр_с_малым_к_изл], [Коэфф_откр_с_высоким_к_изл]) VALUES (3, N'2.04.014', 19, NULL, N'Все виды', 5, 7, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Коэффициенты теплоотдачи] OFF
GO
SET IDENTITY_INSERT [dbo].[Коэффициенты учитыв. тепловые потери] ON 

GO
INSERT [dbo].[Коэффициенты учитыв. тепловые потери] ([id_коэфф_тепл_потери], [Номер_СНиП], [id_материала], [Нач_диаметр], [Кон_диаметр], [Коэффициент]) VALUES (1, N'2.04.14', 1, 0, 150, 1.2)
GO
INSERT [dbo].[Коэффициенты учитыв. тепловые потери] ([id_коэфф_тепл_потери], [Номер_СНиП], [id_материала], [Нач_диаметр], [Кон_диаметр], [Коэффициент]) VALUES (2, N'2.04.14', 1, 150, 500, 1.15)
GO
SET IDENTITY_INSERT [dbo].[Коэффициенты учитыв. тепловые потери] OFF
GO
SET IDENTITY_INSERT [dbo].[Материал] ON 

GO
INSERT [dbo].[Материал] ([id_материала], [Наименование_материала], [Коэффициент_теплопроводности_материал], [Наименование_изоляции], [Коэффициент_теплопроводности_изоляция]) VALUES (1, N'Сталь', 58, N'Полиуретан', 0.01)
GO
INSERT [dbo].[Материал] ([id_материала], [Наименование_материала], [Коэффициент_теплопроводности_материал], [Наименование_изоляции], [Коэффициент_теплопроводности_изоляция]) VALUES (2, N'Медь', 382, N'Полиуретан', 0.01)
GO
SET IDENTITY_INSERT [dbo].[Материал] OFF
GO
SET IDENTITY_INSERT [dbo].[Обслуживание] ON 

GO
INSERT [dbo].[Обслуживание] ([id_обслуживания], [Код_трубы], [Номер_бригады], [Результат], [Дата], [id_сотрудника]) VALUES (1, N'R11-5', N'15', N'Выполнен ремонт изоляции', CAST(0x413E0B00 AS Date), 1)
GO
SET IDENTITY_INSERT [dbo].[Обслуживание] OFF
GO
SET IDENTITY_INSERT [dbo].[Расчетные потери] ON 

GO
INSERT [dbo].[Расчетные потери] ([id_расчетных_потерь], [t_воды], [t_среды], [id_коэфф_тепл_отдачи], [Номер_коэфф], [id_коэф_тепл_потери], [Дата], [Код_трубы], [id_сотрудника], [Результат], [Дата_расчета]) VALUES (1, 50, 15, 3, 0, 1, CAST(0x413E0B00 AS Date), N'R11-5', 1, 940, CAST(0x0000A8E700D54D46 AS DateTime))
GO
INSERT [dbo].[Расчетные потери] ([id_расчетных_потерь], [t_воды], [t_среды], [id_коэфф_тепл_отдачи], [Номер_коэфф], [id_коэф_тепл_потери], [Дата], [Код_трубы], [id_сотрудника], [Результат], [Дата_расчета]) VALUES (2, 45, 19, 3, 0, 1, CAST(0x4B3E0B00 AS Date), N'R11-5', 1, 698, CAST(0x0000A8F20174AD8C AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Расчетные потери] OFF
GO
SET IDENTITY_INSERT [dbo].[Сотрудники] ON 

GO
INSERT [dbo].[Сотрудники] ([id_сотрудника], [Фамилия], [Имя], [Отчество], [Должность], [Паспортные_данные], [id_аккаунта]) VALUES (1, N'Иванов', N'Иван', N'Иванович', N'Главный инженер', N'AB34920820', 1)
GO
INSERT [dbo].[Сотрудники] ([id_сотрудника], [Фамилия], [Имя], [Отчество], [Должность], [Паспортные_данные], [id_аккаунта]) VALUES (2, N'Савченко', N'Игорь', N'Викторович', N'Контроллер', N'AB3924891', 2)
GO
INSERT [dbo].[Сотрудники] ([id_сотрудника], [Фамилия], [Имя], [Отчество], [Должность], [Паспортные_данные], [id_аккаунта]) VALUES (3, N'Гури', N'грург', N'вагрг', N'выгр', N'MC2183135', 3)
GO
SET IDENTITY_INSERT [dbo].[Сотрудники] OFF
GO
INSERT [dbo].[Труба] ([Код_трубы], [Длина], [d_внутр_материал], [d_наруж_материал], [d_внутр_изол], [d_наруж_изол], [id_материала], [id_участка]) VALUES (N'L01', 12, 100, 105, 105, 110, 1, 2)
GO
INSERT [dbo].[Труба] ([Код_трубы], [Длина], [d_внутр_материал], [d_наруж_материал], [d_внутр_изол], [d_наруж_изол], [id_материала], [id_участка]) VALUES (N'R11-5', 11, 0.214, 0.219, 0.219, 0.229, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Участок] ON 

GO
INSERT [dbo].[Участок] ([id_участка], [Наименование], [Населенный_пункт]) VALUES (1, N'Северный район', N'Смоленск')
GO
INSERT [dbo].[Участок] ([id_участка], [Наименование], [Населенный_пункт]) VALUES (2, N'Южный', N'Смоленск')
GO
SET IDENTITY_INSERT [dbo].[Участок] OFF
GO
SET IDENTITY_INSERT [dbo].[Фактические потери] ON 

GO
INSERT [dbo].[Фактические потери] ([id_факт_потерь], [Значение], [Дата], [Код_трубы], [id_сотрудника]) VALUES (1, 1000, CAST(0x413E0B00 AS Date), N'R11-5', 1)
GO
INSERT [dbo].[Фактические потери] ([id_факт_потерь], [Значение], [Дата], [Код_трубы], [id_сотрудника]) VALUES (2, 900, CAST(0x4B3E0B00 AS Date), N'R11-5', 1)
GO
SET IDENTITY_INSERT [dbo].[Фактические потери] OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Аккаунты__BC2217D3131CCE9C]    Script Date: 27.06.2018 17:49:04 ******/
ALTER TABLE [dbo].[Аккаунты] ADD  CONSTRAINT [UQ__Аккаунты__BC2217D3131CCE9C] UNIQUE NONCLUSTERED 
(
	[Логин] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Аккаунты__BC2217D31628A7DF]    Script Date: 27.06.2018 17:49:04 ******/
ALTER TABLE [dbo].[Аккаунты] ADD  CONSTRAINT [UQ__Аккаунты__BC2217D31628A7DF] UNIQUE NONCLUSTERED 
(
	[Логин] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Расчетны__838AAF472CDF761E]    Script Date: 27.06.2018 17:49:04 ******/
ALTER TABLE [dbo].[Расчетные потери] ADD  CONSTRAINT [UQ__Расчетны__838AAF472CDF761E] UNIQUE NONCLUSTERED 
(
	[Дата] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Сотрудни__FAC702D5D325E0DD]    Script Date: 27.06.2018 17:49:04 ******/
ALTER TABLE [dbo].[Сотрудники] ADD  CONSTRAINT [UQ__Сотрудни__FAC702D5D325E0DD] UNIQUE NONCLUSTERED 
(
	[Паспортные_данные] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Фактичес__838AAF476404E1E8]    Script Date: 27.06.2018 17:49:04 ******/
ALTER TABLE [dbo].[Фактические потери] ADD UNIQUE NONCLUSTERED 
(
	[Дата] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Коэффициенты учитыв. тепловые потери]  WITH CHECK ADD  CONSTRAINT [FK_Коэффициенты учитыв. тепловые потери_Материал] FOREIGN KEY([id_материала])
REFERENCES [dbo].[Материал] ([id_материала])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Коэффициенты учитыв. тепловые потери] CHECK CONSTRAINT [FK_Коэффициенты учитыв. тепловые потери_Материал]
GO
ALTER TABLE [dbo].[Обслуживание]  WITH CHECK ADD  CONSTRAINT [FK_Обслуживание_Сотрудники] FOREIGN KEY([id_сотрудника])
REFERENCES [dbo].[Сотрудники] ([id_сотрудника])
GO
ALTER TABLE [dbo].[Обслуживание] CHECK CONSTRAINT [FK_Обслуживание_Сотрудники]
GO
ALTER TABLE [dbo].[Обслуживание]  WITH CHECK ADD  CONSTRAINT [FK_Обслуживание_Труба] FOREIGN KEY([Код_трубы])
REFERENCES [dbo].[Труба] ([Код_трубы])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Обслуживание] CHECK CONSTRAINT [FK_Обслуживание_Труба]
GO
ALTER TABLE [dbo].[Расчетные потери]  WITH CHECK ADD  CONSTRAINT [FK_Расчетные потери_Коэффициенты теплоотдачи1] FOREIGN KEY([id_коэфф_тепл_отдачи])
REFERENCES [dbo].[Коэффициенты теплоотдачи] ([id_коэф_тепл_отдачи])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Расчетные потери] CHECK CONSTRAINT [FK_Расчетные потери_Коэффициенты теплоотдачи1]
GO
ALTER TABLE [dbo].[Расчетные потери]  WITH CHECK ADD  CONSTRAINT [FK_Расчетные потери_Коэффициенты учитыв. тепловые потери1] FOREIGN KEY([id_коэф_тепл_потери])
REFERENCES [dbo].[Коэффициенты учитыв. тепловые потери] ([id_коэфф_тепл_потери])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Расчетные потери] CHECK CONSTRAINT [FK_Расчетные потери_Коэффициенты учитыв. тепловые потери1]
GO
ALTER TABLE [dbo].[Расчетные потери]  WITH CHECK ADD  CONSTRAINT [FK_Расчетные потери_Сотрудники] FOREIGN KEY([id_сотрудника])
REFERENCES [dbo].[Сотрудники] ([id_сотрудника])
GO
ALTER TABLE [dbo].[Расчетные потери] CHECK CONSTRAINT [FK_Расчетные потери_Сотрудники]
GO
ALTER TABLE [dbo].[Расчетные потери]  WITH CHECK ADD  CONSTRAINT [FK_Расчетные потери_Труба] FOREIGN KEY([Код_трубы])
REFERENCES [dbo].[Труба] ([Код_трубы])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Расчетные потери] CHECK CONSTRAINT [FK_Расчетные потери_Труба]
GO
ALTER TABLE [dbo].[Сотрудники]  WITH CHECK ADD  CONSTRAINT [FK_Сотрудники_Аккаунты] FOREIGN KEY([id_аккаунта])
REFERENCES [dbo].[Аккаунты] ([id_аккаунта])
GO
ALTER TABLE [dbo].[Сотрудники] CHECK CONSTRAINT [FK_Сотрудники_Аккаунты]
GO
ALTER TABLE [dbo].[Труба]  WITH CHECK ADD  CONSTRAINT [FK_Труба_Материал] FOREIGN KEY([id_материала])
REFERENCES [dbo].[Материал] ([id_материала])
GO
ALTER TABLE [dbo].[Труба] CHECK CONSTRAINT [FK_Труба_Материал]
GO
ALTER TABLE [dbo].[Труба]  WITH CHECK ADD  CONSTRAINT [FK_Труба_Участок] FOREIGN KEY([id_участка])
REFERENCES [dbo].[Участок] ([id_участка])
GO
ALTER TABLE [dbo].[Труба] CHECK CONSTRAINT [FK_Труба_Участок]
GO
ALTER TABLE [dbo].[Фактические потери]  WITH CHECK ADD  CONSTRAINT [FK_Фактические потери_Сотрудники] FOREIGN KEY([id_сотрудника])
REFERENCES [dbo].[Сотрудники] ([id_сотрудника])
GO
ALTER TABLE [dbo].[Фактические потери] CHECK CONSTRAINT [FK_Фактические потери_Сотрудники]
GO
ALTER TABLE [dbo].[Фактические потери]  WITH CHECK ADD  CONSTRAINT [FK_Фактические потери_Труба1] FOREIGN KEY([Код_трубы])
REFERENCES [dbo].[Труба] ([Код_трубы])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Фактические потери] CHECK CONSTRAINT [FK_Фактические потери_Труба1]
GO
USE [master]
GO
ALTER DATABASE [Teploseti] SET  READ_WRITE 
GO

```
