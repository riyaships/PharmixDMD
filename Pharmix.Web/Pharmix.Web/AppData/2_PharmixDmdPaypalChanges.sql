-- Columns

CREATE TABLE [dbo].[PackagePlans]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[PackageName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Price] [decimal] (18, 2) NOT NULL,
[Duration] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Details] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ArchivedDate] [datetime2] NULL,
[ArchivedUser] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatedDate] [datetime2] NOT NULL,
[CreatedUser] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsArchived] [bit] NOT NULL,
[UpdatedDate] [datetime2] NULL,
[UpdatedUser] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[PackagePlans] ADD CONSTRAINT [PK_PackagePlan] PRIMARY KEY CLUSTERED  ([Id])
GO

-- Columns

CREATE TABLE [dbo].[Business_Subscription]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[IdentityUserId] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PackagePlanId] [int] NOT NULL,
[StartDate] [date] NOT NULL,
[EndDate] [date] NOT NULL,
[PaidAmount] [decimal] (18, 2) NOT NULL,
[Currency] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PaymentReceipt] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PaymentReceivedDate] [datetime] NULL,
[PaymentReceived] [bit] NOT NULL,
[PaymentNotes] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PaymentVia] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ArchivedDate] [datetime2] NULL,
[ArchivedUser] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatedDate] [datetime2] NOT NULL,
[CreatedUser] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsArchived] [bit] NOT NULL,
[UpdatedDate] [datetime2] NULL,
[UpdatedUser] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
-- Constraints and Indexes

ALTER TABLE [dbo].[Business_Subscription] ADD CONSTRAINT [PK_Business_Subscription] PRIMARY KEY CLUSTERED  ([Id])
GO


ALTER TABLE  [dbo].[IdentityUser] ADD
[GenderId] [int] NULL,
[DateOfBirth] [datetime] NULL,
[FirstName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Surname] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MobileNumber] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AlternativeTel] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsResetPasswordRequired] [bit] NULL,
[AddressId] [int] NULL
