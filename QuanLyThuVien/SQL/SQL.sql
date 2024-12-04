﻿CREATE DATABASE [LIBRARY_MANAGER];
GO
USE [LIBRARY_MANAGER];
GO


-- ACCOUNT_USER Table
CREATE TABLE [dbo].[ACCOUNT_USER] (
    [ID] INT IDENTITY(1,1) PRIMARY KEY,
    [USER_ACCOUNT] NVARCHAR(255) NOT NULL,
    [USER_NAME_TEXT]  NVARCHAR(255) NOT NULL,
    [PASSWORD_TEXT] NVARCHAR(255) NOT NULL,
    [DOB] DATETIME NOT NULL,
    [ROLE] BIT NOT NULL,
    [PHONE_NUMBER] NVARCHAR(15) NULL,
    [ADDRESS] NVARCHAR(255) NULL

);
GO

-- BOOK Table
CREATE TABLE [dbo].[BOOK] (
    [ID] INT IDENTITY(1,1) PRIMARY KEY,
    [BOOK_NAME] NVARCHAR(255) NOT NULL,
    [AUTHOR] NVARCHAR(255) NOT NULL,
    [CATEGORY] NVARCHAR(255) NOT NULL,
    [PUBLISHER] NVARCHAR(255) NOT NULL,
    [DATE_PUBLISH] DATETIME NULL,
    [DATE_IMPORT] DATETIME NULL,
    [PRICE] DECIMAL(10, 2) NULL

);
GO

-- READER Table
CREATE TABLE [dbo].[READER] (
    [ID] INT IDENTITY(1,1) PRIMARY KEY,
    [USER_NAME_TEXT] NVARCHAR(255) NOT NULL,
    [DOB] DATETIME NOT NULL,
    [DATECREATED] DATETIME NOT NULL,
    [READERTYPE] NVARCHAR(255) NULL,
    [EMAIL] NVARCHAR(255) NOT NULL,
    [ADDRESS] NVARCHAR(255) NOT NULL
);
GO

-- LIST_BORROWED Table
CREATE TABLE [dbo].[LIST_BORROWED] (
    [ID] INT IDENTITY(1,1) PRIMARY KEY,
    [ID_READER] INT NOT NULL,
    [ID_BOOK] INT NOT NULL,
    [STATUS_BORROW] NVARCHAR(20) NULL DEFAULT N'Borrowed',
    [DATE_BORROWED] DATETIME NOT NULL,
    [DATE_EXPIRED] DATETIME NOT NULL,
    FOREIGN KEY (ID_READER) REFERENCES [dbo].[READER](ID),
    FOREIGN KEY (ID_BOOK) REFERENCES [dbo].[BOOK](ID)
);

SET IDENTITY_INSERT [dbo].[ACCOUNT_USER] ON 

INSERT [dbo].[ACCOUNT_USER] ([ID], [USER_ACCOUNT], [USER_NAME_TEXT], [PASSWORD_TEXT], [DOB], [ROLE], [PHONE_NUMBER], [ADDRESS]) VALUES (1, N'Ho Viet Tra', N'hotra123', N'123', CAST(N'2003-01-17T00:00:00.000' AS DateTime), 1, N'123456', N'BN')
INSERT [dbo].[ACCOUNT_USER] ([ID], [USER_ACCOUNT], [USER_NAME_TEXT], [PASSWORD_TEXT], [DOB], [ROLE], [PHONE_NUMBER], [ADDRESS]) VALUES (7, N'Linh Doan', N'linhdoan', N'123', CAST(N'2003-10-19T00:00:00.000' AS DateTime), 0, N'123', N'TB')
INSERT [dbo].[ACCOUNT_USER] ([ID], [USER_ACCOUNT], [USER_NAME_TEXT], [PASSWORD_TEXT], [DOB], [ROLE], [PHONE_NUMBER], [ADDRESS]) VALUES (8, N'Duong Manh', N'duongmanh', N'123', CAST(N'2003-01-02T00:00:00.000' AS DateTime), 0, N'123456', N'HD')
INSERT [dbo].[ACCOUNT_USER] ([ID], [USER_ACCOUNT], [USER_NAME_TEXT], [PASSWORD_TEXT], [DOB], [ROLE], [PHONE_NUMBER], [ADDRESS]) VALUES (11, N'h', N'h', N'123', CAST(N'2003-01-02T00:00:00.000' AS DateTime), 0, N'123', N'd')
SET IDENTITY_INSERT [dbo].[ACCOUNT_USER] OFF
GO
SET IDENTITY_INSERT [dbo].[BOOK] ON 

INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (3, N'Truy?n Cu?i', N'H? Vi?t Trà', N'Truy?n', N'Kim Ð?ng', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(22.50 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (4, N'loskim', N'hoviettra', N'truyentranh', N'hoviettra', CAST(N'2003-01-17T00:00:00.000' AS DateTime), CAST(N'2020-09-10T00:00:00.000' AS DateTime), CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (6, N'Doraemon', N'HoVietTra', N'truyen tranh', N'HoVietTra', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-10-30T00:00:00.000' AS DateTime), CAST(123.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (7, N'tham tu lung danh conan', N'hvt', N'truyen tranh', N'hvt', CAST(N'2003-01-17T00:00:00.000' AS DateTime), CAST(N'2003-01-20T00:00:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (9, N'Các thì trong ti?ng anh', N'Kim Ð?ng', N'H?c T?p', N'Kim Ð?ng', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(50.70 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (10, N'Cap nhat', N'Cap nhat', N'Cap nhat', N'Cap nhat', CAST(N'2024-10-03T00:00:00.000' AS DateTime), CAST(N'2024-10-10T00:00:00.000' AS DateTime), CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (11, N'hoviettra', N'hoviettra', N'hoviettra', N'hoviettra', CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(N'2024-11-20T00:00:00.000' AS DateTime), CAST(15.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (12, N'Than Dong Dat Viet', N'Ho Viet Tra', N'Truyen Tranh', N'Kim Ðong', CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(22.50 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (13, N'Võ Luy?n Ð?nh Phong', N'H? Vi?t Trà', N'Truy?n Tranh', N'H? Vi?t Trà', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-11-21T00:00:00.000' AS DateTime), CAST(18.50 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (14, N'Muoi van cau hoi vi sao', N'Ho Viet Tra', N'hoc tap', N'Kim Dong', CAST(N'2024-10-01T00:00:00.000' AS DateTime), CAST(N'2024-10-31T00:00:00.000' AS DateTime), CAST(12.70 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (15, N'Truyen cuoi1', N'Ho Viet Trà', N'Truyen', N'Kim Ðong', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(22.50 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (16, N'abcd', N'H? Vi?t Trà', N'Truyn', N'Kim Ð?ng', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(22.50 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (17, N'Truyn Cui', N'H? Vi?t Trà', N'Truyn', N'Kim Ð?ng', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(22.50 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (29, N'4', N'1', N'1', N'1', CAST(N'2024-11-02T00:00:00.000' AS DateTime), CAST(N'2024-11-09T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (30, N'1', N'1', N'1', N'1', CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (31, N'5', N'4', N'4', N'4', CAST(N'2024-11-16T00:00:00.000' AS DateTime), CAST(N'2024-11-24T00:00:00.000' AS DateTime), CAST(4.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (32, N'10', N'10', N'10', N'10', CAST(N'2024-11-02T00:00:00.000' AS DateTime), CAST(N'2024-11-09T00:00:00.000' AS DateTime), CAST(10.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (33, N'2', N'2', N'2', N'2', CAST(N'2024-11-02T00:00:00.000' AS DateTime), CAST(N'2024-11-08T00:00:00.000' AS DateTime), CAST(2.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (34, N'3', N'3', N'3', N'3', CAST(N'2024-11-02T00:00:00.000' AS DateTime), CAST(N'2024-11-03T00:00:00.000' AS DateTime), CAST(3.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (35, N'6', N'6', N'6', N'6', CAST(N'2024-11-02T00:00:00.000' AS DateTime), CAST(N'2024-11-09T00:00:00.000' AS DateTime), CAST(6.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (38, N'C Programming', N'Dennis Ritchie', N'Programming', N'Pearson', CAST(N'1978-03-22T00:00:00.000' AS DateTime), CAST(N'2023-01-10T00:00:00.000' AS DateTime), CAST(250000.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (39, N'Data Structures', N'Mark Allen Weiss', N'Data Science', N'Addison-Wesley', CAST(N'1993-08-15T00:00:00.000' AS DateTime), CAST(N'2023-02-20T00:00:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (40, N'Introduction to Algorithms', N'Thomas H. Cormen', N'Algorithms', N'MIT Press', CAST(N'1990-07-28T00:00:00.000' AS DateTime), CAST(N'2023-03-01T00:00:00.000' AS DateTime), CAST(500000.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (41, N'Artificial Intelligence', N'Stuart Russell', N'AI', N'Prentice Hall', CAST(N'1995-12-12T00:00:00.000' AS DateTime), CAST(N'2023-04-10T00:00:00.000' AS DateTime), CAST(400000.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (42, N'Clean Code', N'Robert C. Martin', N'Programming', N'Prentice Hall', CAST(N'2008-08-01T00:00:00.000' AS DateTime), CAST(N'2023-05-15T00:00:00.000' AS DateTime), CAST(350000.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (43, N'Design Patterns', N'Erich Gamma', N'Software Engineering', N'Addison-Wesley', CAST(N'1994-10-21T00:00:00.000' AS DateTime), CAST(N'2023-06-01T00:00:00.000' AS DateTime), CAST(450000.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (44, N'Database Systems', N'Ramez Elmasri', N'Databases', N'McGraw-Hill', CAST(N'1989-05-25T00:00:00.000' AS DateTime), CAST(N'2023-07-12T00:00:00.000' AS DateTime), CAST(550000.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (45, N'Java Programming', N'Herbert Schildt', N'Programming', N'McGraw-Hill', CAST(N'1998-03-27T00:00:00.000' AS DateTime), CAST(N'2023-08-20T00:00:00.000' AS DateTime), CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (46, N'The Pragmatic Programmer', N'Andrew Hunt', N'Software Development', N'Addison-Wesley', CAST(N'1999-10-20T00:00:00.000' AS DateTime), CAST(N'2023-09-15T00:00:00.000' AS DateTime), CAST(375000.00 AS Decimal(10, 2)))
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE]) VALUES (47, N'Python Crash Course', N'Eric Matthes', N'Programming', N'No Starch Press', CAST(N'2015-11-01T00:00:00.000' AS DateTime), CAST(N'2023-10-05T00:00:00.000' AS DateTime), CAST(280000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[BOOK] OFF

GO
SET IDENTITY_INSERT [dbo].[READER] ON 

INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (1, N'HoTra', CAST(N'2024-12-07T00:00:00.000' AS DateTime), CAST(N'2024-12-12T00:00:00.000' AS DateTime), N'HOC SINH', N'a@gmail.com', N'BN')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (2, N'a', CAST(N'2024-11-30T00:00:00.000' AS DateTime), CAST(N'2024-12-07T00:00:00.000' AS DateTime), N'SINH VIEN', N'a@gmail.com', N'a')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (3, N'user', CAST(N'1990-01-01T00:00:00.000' AS DateTime), CAST(N'2024-12-01T00:00:00.000' AS DateTime), N'Regular', N'user1@example.com', N'123 Main St, City')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (4, N'user2', CAST(N'1985-05-12T00:00:00.000' AS DateTime), CAST(N'2024-12-01T00:00:00.000' AS DateTime), N'Premium', N'user2@example.com', N'456 Elm St, City')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (5, N'user4', CAST(N'1995-03-22T00:00:00.000' AS DateTime), CAST(N'2024-12-01T00:00:00.000' AS DateTime), N'Regular', N'user4@example.com', N'321 Pine St, City')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (6, N'1', CAST(N'1992-11-01T00:00:00.000' AS DateTime), CAST(N'2024-12-06T00:00:00.000' AS DateTime), N'hs', N'1', N'1')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (9, N'Nguyen Van A', CAST(N'1990-01-01T00:00:00.000' AS DateTime), CAST(N'2024-12-03T17:54:04.610' AS DateTime), N'Student', N'nguyenvana@example.com', N'Hanoi')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (10, N'Tran Thi B', CAST(N'1995-03-15T00:00:00.000' AS DateTime), CAST(N'2024-12-03T17:54:04.610' AS DateTime), N'Teacher', N'tranthib@example.com', N'HCMC')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (11, N'Le Van C', CAST(N'1988-07-20T00:00:00.000' AS DateTime), CAST(N'2024-12-03T17:54:04.610' AS DateTime), N'Student', N'levanc@example.com', N'Da Nang')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (12, N'Pham Thi D', CAST(N'1992-11-30T00:00:00.000' AS DateTime), CAST(N'2024-12-03T17:54:04.610' AS DateTime), NULL, N'phamthid@example.com', N'Hai Phong')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (13, N'Hoang Van E', CAST(N'2000-05-10T00:00:00.000' AS DateTime), CAST(N'2024-12-03T17:54:04.610' AS DateTime), N'Teacher', N'hoangvane@example.com', N'Can Tho')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (14, N'Nguyen Thi F', CAST(N'1993-12-25T00:00:00.000' AS DateTime), CAST(N'2024-12-03T17:54:04.610' AS DateTime), N'Student', N'nguyenthif@example.com', N'Nha Trang')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (15, N'Tran Van G', CAST(N'1985-06-17T00:00:00.000' AS DateTime), CAST(N'2024-12-03T17:54:04.610' AS DateTime), NULL, N'tranvang@example.com', N'Hue')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (16, N'Le Thi H', CAST(N'1998-09-05T00:00:00.000' AS DateTime), CAST(N'2024-12-03T17:54:04.610' AS DateTime), N'Student', N'lethih@example.com', N'Vung Tau')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (17, N'Pham Van I', CAST(N'1991-04-14T00:00:00.000' AS DateTime), CAST(N'2024-12-03T17:54:04.610' AS DateTime), N'Teacher', N'phamvani@example.com', N'Quang Ninh')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [DATECREATED], [READERTYPE], [EMAIL], [ADDRESS]) VALUES (18, N'Hoang Thi J', CAST(N'1989-02-18T00:00:00.000' AS DateTime), CAST(N'2024-12-03T17:54:04.610' AS DateTime), N'Student', N'hoangthij@example.com', N'Da Lat')
SET IDENTITY_INSERT [dbo].[READER] OFF


GO
SET IDENTITY_INSERT [dbo].[LIST_BORROWED] ON 

INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (1, 1, 3, N'Cũ', CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (2, 2, 4, N'Cũ', CAST(N'2024-05-02T00:00:00.000' AS DateTime), CAST(N'2024-11-16T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (3, 3, 6, N'Cũ', CAST(N'2024-11-03T00:00:00.000' AS DateTime), CAST(N'2024-11-17T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (4, 4, 7, N'Cũ', CAST(N'2024-11-04T00:00:00.000' AS DateTime), CAST(N'2024-11-18T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (5, 5, 9, N'Cũ', CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-11-19T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (6, 6, 10, N'Cũ', CAST(N'2024-06-06T00:00:00.000' AS DateTime), CAST(N'2024-11-20T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (21, 2, 39, N'Cũ', CAST(N'2023-10-25T00:00:00.000' AS DateTime), CAST(N'2023-11-10T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (22, 2, 39, N'Cũ', CAST(N'2023-10-18T00:00:00.000' AS DateTime), CAST(N'2023-11-02T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (23, 3, 47, N'Cũ', CAST(N'2023-11-03T00:00:00.000' AS DateTime), CAST(N'2023-11-17T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (24, 3, 47, N'Cũ', CAST(N'2023-10-30T00:00:00.000' AS DateTime), CAST(N'2023-11-13T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (25, 4, 47, N'Cũ', CAST(N'2023-10-22T00:00:00.000' AS DateTime), CAST(N'2023-11-06T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (26, 5, 43, N'Cũ', CAST(N'2023-11-02T00:00:00.000' AS DateTime), CAST(N'2023-11-16T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (27, 5, 43, N'Cũ', CAST(N'2023-10-29T00:00:00.000' AS DateTime), CAST(N'2023-11-12T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (28, 4, 43, N'Cũ', CAST(N'2023-10-25T00:00:00.000' AS DateTime), CAST(N'2023-11-09T00:00:00.000' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (32, 1, 4, N'Mới', CAST(N'2024-12-04T00:58:51.437' AS DateTime), CAST(N'2025-01-04T00:58:51.437' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (33, 1, 6, N'Mới', CAST(N'2024-12-04T01:09:59.660' AS DateTime), CAST(N'2025-01-04T01:09:59.660' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (34, 1, 47, N'Mới', CAST(N'2024-12-04T01:23:03.750' AS DateTime), CAST(N'2025-01-04T01:23:03.753' AS DateTime))
INSERT [dbo].[LIST_BORROWED] ([ID], [ID_READER], [ID_BOOK], [STATUS_BORROW], [DATE_BORROWED], [DATE_EXPIRED]) VALUES (35, 1, 4, N'Mới', CAST(N'2024-12-04T01:47:41.760' AS DateTime), CAST(N'2025-01-04T01:47:41.760' AS DateTime))
SET IDENTITY_INSERT [dbo].[LIST_BORROWED] OFF



