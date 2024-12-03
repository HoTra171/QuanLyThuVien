﻿CREATE DATABASE LIBRARY_MANAGER;
GO

USE LIBRARY_MANAGER;
GO

CREATE TABLE ACCOUNT_USER (
    ID INT PRIMARY KEY IDENTITY(1,1),
    USER_ACCOUNT VARCHAR(255) NOT NULL,
    USER_NAME_TEXT VARCHAR(255) NOT NULL,
    PASSWORD_TEXT VARCHAR(255) NOT NULL,
    DOB DATETIME NOT NULL,
    ROLE BIT DEFAULT 0 NOT NULL,
    PHONE_NUMBER VARCHAR(255)  NULL,
    ADDRESS VARCHAR(255)  NULL
);

CREATE UNIQUE INDEX IX_USER_ACCOUNT ON ACCOUNT_USER (USER_ACCOUNT);

CREATE TABLE READER (
    ID INT PRIMARY KEY IDENTITY(1,1),
    USER_NAME_TEXT VARCHAR(255) NOT NULL,
    DOB DATETIME NOT NULL,
    EMAIL VARCHAR(255) NOT NULL,
    ADDRESS VARCHAR(255) NOT NULL
);

CREATE TABLE BOOK (
    ID INT PRIMARY KEY IDENTITY(1,1),
    BOOK_NAME VARCHAR(255) NOT NULL,
    AUTHOR VARCHAR(255) NOT NULL,
    DATE_PUBLISH DATETIME,
    DATE_IMPORT DATETIME,
    PRICE DECIMAL(10, 2)
);



CREATE TABLE LIST_BORROWED (
    ID INT PRIMARY KEY IDENTITY(1,1),
    ID_READER INT NOT NULL,
    ID_BOOK INT NOT NULL,
    STATUS_BORROW BIT DEFAULT 0,
    DATE_BORROWED DATETIME NOT NULL,
    DATE_EXPIRED DATETIME NOT NULL,
    FOREIGN KEY (ID_READER) REFERENCES READER(ID) ON DELETE CASCADE,
    FOREIGN KEY (ID_BOOK) REFERENCES BOOK(ID) ON DELETE SET NULL
);


INSERT [dbo].[ACCOUNT_USER] ([ID], [USER_ACCOUNT], [USER_NAME_TEXT], [PASSWORD_TEXT], [DOB], [PHONE_NUMBER], [ROLE], [ADDRESS]) VALUES (1, N'Ho Viet Tra', N'hotra123', N'123', CAST(N'2003-01-17T00:00:00.000' AS DateTime), N'123456', 1, N'BN')
INSERT [dbo].[ACCOUNT_USER] ([ID], [USER_ACCOUNT], [USER_NAME_TEXT], [PASSWORD_TEXT], [DOB], [PHONE_NUMBER], [ROLE], [ADDRESS]) VALUES (7, N'Linh Doan', N'linhdoan', N'123', CAST(N'2003-10-19T00:00:00.000' AS DateTime), N'123', 0, N'TB')
INSERT [dbo].[ACCOUNT_USER] ([ID], [USER_ACCOUNT], [USER_NAME_TEXT], [PASSWORD_TEXT], [DOB], [PHONE_NUMBER], [ROLE], [ADDRESS]) VALUES (8, N'Duong Manh', N'duongmanh', N'123', CAST(N'2003-01-02T00:00:00.000' AS DateTime), N'123456', 0, N'HD')
INSERT [dbo].[ACCOUNT_USER] ([ID], [USER_ACCOUNT], [USER_NAME_TEXT], [PASSWORD_TEXT], [DOB], [PHONE_NUMBER], [ROLE], [ADDRESS]) VALUES (11, N'h', N'h', N'123', CAST(N'2003-01-02T00:00:00.000' AS DateTime), N'123', 0, N'd')

GO


INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (3, N'Truy?n Cu?i', N'H? Vi?t Trà', N'Truy?n', N'Kim Ð?ng', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(22.50 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (4, N'loskim', N'hoviettra', N'truyentranh', N'hoviettra', CAST(N'2003-01-17T00:00:00.000' AS DateTime), CAST(N'2020-09-10T00:00:00.000' AS DateTime), CAST(50000.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (6, N'Doraemon', N'HoVietTra', N'truyen tranh', N'HoVietTra', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-10-30T00:00:00.000' AS DateTime), CAST(123.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (7, N'tham tu lung danh conan', N'hvt', N'truyen tranh', N'hvt', CAST(N'2003-01-17T00:00:00.000' AS DateTime), CAST(N'2003-01-20T00:00:00.000' AS DateTime), CAST(200000.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (9, N'Các thì trong ti?ng anh', N'Kim Ð?ng', N'H?c T?p', N'Kim Ð?ng', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(50.70 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (10, N'Cap nhat', N'Cap nhat', N'Cap nhat', N'Cap nhat', CAST(N'2024-10-03T00:00:00.000' AS DateTime), CAST(N'2024-10-10T00:00:00.000' AS DateTime), CAST(10.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (11, N'hoviettra', N'hoviettra', N'hoviettra', N'hoviettra', CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(N'2024-11-20T00:00:00.000' AS DateTime), CAST(15.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (12, N'Than Dong Dat Viet', N'Ho Viet Tra', N'Truyen Tranh', N'Kim Ðong', CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(22.50 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (13, N'Võ Luy?n Ð?nh Phong', N'H? Vi?t Trà', N'Truy?n Tranh', N'H? Vi?t Trà', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-11-21T00:00:00.000' AS DateTime), CAST(18.50 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (14, N'Muoi van cau hoi vi sao', N'Ho Viet Tra', N'hoc tap', N'Kim Dong', CAST(N'2024-10-01T00:00:00.000' AS DateTime), CAST(N'2024-10-31T00:00:00.000' AS DateTime), CAST(12.70 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (15, N'Truyen cuoi1', N'Ho Viet Trà', N'Truyen', N'Kim Ðong', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(22.50 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (16, N'abcd', N'H? Vi?t Trà', N'Truyn', N'Kim Ð?ng', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(22.50 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (17, N'Truyn Cui', N'H? Vi?t Trà', N'Truyn', N'Kim Ð?ng', CAST(N'2024-10-28T00:00:00.000' AS DateTime), CAST(N'2024-10-29T00:00:00.000' AS DateTime), CAST(22.50 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (29, N'4', N'1', N'1', N'1', CAST(N'2024-11-02T00:00:00.000' AS DateTime), CAST(N'2024-11-09T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (30, N'1', N'1', N'1', N'1', CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (31, N'5', N'4', N'4', N'4', CAST(N'2024-11-16T00:00:00.000' AS DateTime), CAST(N'2024-11-24T00:00:00.000' AS DateTime), CAST(4.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (32, N'10', N'10', N'10', N'10', CAST(N'2024-11-02T00:00:00.000' AS DateTime), CAST(N'2024-11-09T00:00:00.000' AS DateTime), CAST(10.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (33, N'2', N'2', N'2', N'2', CAST(N'2024-11-02T00:00:00.000' AS DateTime), CAST(N'2024-11-08T00:00:00.000' AS DateTime), CAST(2.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (34, N'3', N'3', N'3', N'3', CAST(N'2024-11-02T00:00:00.000' AS DateTime), CAST(N'2024-11-03T00:00:00.000' AS DateTime), CAST(3.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[BOOK] ([ID], [BOOK_NAME], [AUTHOR], [CATEGORY], [PUBLISHER], [DATE_PUBLISH], [DATE_IMPORT], [PRICE], [ISSELECTED]) VALUES (35, N'6', N'6', N'6', N'6', CAST(N'2024-11-02T00:00:00.000' AS DateTime), CAST(N'2024-11-09T00:00:00.000' AS DateTime), CAST(6.00 AS Decimal(10, 2)), 0)
GO

INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [ADDRESS], [EMAIL], [DATECREATED], [READERTYPE]) VALUES (1, N'HoTra', CAST(N'2024-12-07T00:00:00.000' AS DateTime), N'BN', N'a@gmail.com', CAST(N'2024-12-12' AS Date), N'HOC SINH')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [ADDRESS], [EMAIL], [DATECREATED], [READERTYPE]) VALUES (2, N'a', CAST(N'2024-11-30T00:00:00.000' AS DateTime), N'a', N'a@gmail.com', CAST(N'2024-12-07' AS Date), N'SINH VIEN')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [ADDRESS], [EMAIL], [DATECREATED], [READERTYPE]) VALUES (3, N'user', CAST(N'1990-01-01T00:00:00.000' AS DateTime), N'123 Main St, City', N'user1@example.com', CAST(N'2024-12-01' AS Date), N'Regular')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [ADDRESS], [EMAIL], [DATECREATED], [READERTYPE]) VALUES (4, N'user2', CAST(N'1985-05-12T00:00:00.000' AS DateTime), N'456 Elm St, City', N'user2@example.com', CAST(N'2024-12-01' AS Date), N'Premium')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [ADDRESS], [EMAIL], [DATECREATED], [READERTYPE]) VALUES (6, N'user4', CAST(N'1995-03-22T00:00:00.000' AS DateTime), N'321 Pine St, City', N'user4@example.com', CAST(N'2024-12-01' AS Date), N'Regular')
INSERT [dbo].[READER] ([ID], [USER_NAME_TEXT], [DOB], [ADDRESS], [EMAIL], [DATECREATED], [READERTYPE]) VALUES (8, N'1', CAST(N'1992-11-01T00:00:00.000' AS DateTime), N'1', N'1', CAST(N'2024-12-06' AS Date), N'hs')