CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (450)     NOT NULL,
    [UserName]             NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    [CompanyId]            INT                NULL,
    [BranchId]             INT                NULL,
    [UserType]             NVARCHAR (MAX)     NULL,
    [FirstName]            NVARCHAR (MAX)     NULL,
    [LastName]             NVARCHAR (MAX)     NULL,
    [ContactPhone]         NVARCHAR (MAX)     NULL,
    [OfficeId]             INT                NULL,
    [Browser]              NVARCHAR (MAX)     NULL,
    [ElpsId]               INT                NOT NULL,
    [CreatedBy]            NVARCHAR (MAX)     NULL,
    [CreatedOn]            DATETIME2 (7)      NULL,
    [UpdatedBy]            NVARCHAR (MAX)     NULL,
    [UpdatedOn]            DATETIME2 (7)      NULL,
    [Status]               BIT                NOT NULL,
    [LastLogin]            DATETIME2 (7)      NULL,
    [LoginCount]           INT                NULL,
    [Signature]            NVARCHAR (MAX)     NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetUsers_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([Id]),
    CONSTRAINT [FK_AspNetUsers_FieldOffices_OfficeId] FOREIGN KEY ([OfficeId]) REFERENCES [dbo].[FieldOffices] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_OfficeId]
    ON [dbo].[AspNetUsers]([OfficeId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [dbo].[AspNetUsers]([NormalizedEmail] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_CompanyId]
    ON [dbo].[AspNetUsers]([CompanyId] ASC);

