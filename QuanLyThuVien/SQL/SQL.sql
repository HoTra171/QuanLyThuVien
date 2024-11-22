CREATE DATABASE LIBRARY_MANAGER;
GO

USE LIBRARY_MANAGER;
GO

CREATE TABLE ACCOUNT_USER (
    ID INT PRIMARY KEY IDENTITY(1,1),
    USER_ACCOUNT VARCHAR(255) NOT NULL,
    USER_NAME_TEXT VARCHAR(255) NOT NULL,
    PASSWORD_TEXT VARCHAR(255) NOT NULL,
    DOB DATETIME NOT NULL,
    PHONE_NUMBER VARCHAR(255) NOT NULL
);

CREATE TABLE READER (
    ID INT PRIMARY KEY IDENTITY(1,1),
    USER_NAME_TEXT VARCHAR(255) NOT NULL,
    DOB DATETIME NOT NULL,
    PHONE_NUMBER VARCHAR(255) NOT NULL
);

CREATE TABLE BOOK (
    ID INT PRIMARY KEY IDENTITY(1,1),
    BOOK_NAME VARCHAR(255) NOT NULL,
    AUTHOR VARCHAR(255) NOT NULL,
    DATE_PUBLISH DATETIME,
    DATE_IMPORT DATETIME,
    PRICE DECIMAL(10, 2)
);

CREATE TABLE CARD_READER (
    ID INT PRIMARY KEY IDENTITY(1,1),
    ID_READER INT NOT NULL,
    CREATE_AT DATETIME,
    TYPE_READER INT DEFAULT 0,
    FOREIGN KEY (ID_READER) REFERENCES READER(ID) ON DELETE CASCADE
);

CREATE TABLE LIST_BORROWED (
    ID INT PRIMARY KEY IDENTITY(1,1),
    ID_CARD INT NOT NULL,
    ID_BOOK INT NOT NULL,
    STATUS_BORROW BIT DEFAULT 0,
    DATE_BORROWED DATETIME NOT NULL,
    DATE_EXPIRED DATETIME NOT NULL,
    FOREIGN KEY (ID_CARD) REFERENCES CARD_READER(ID) ON DELETE CASCADE,
    FOREIGN KEY (ID_BOOK) REFERENCES BOOK(ID) ON DELETE SET NULL
);