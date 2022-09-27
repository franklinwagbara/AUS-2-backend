CREATE TABLE [dbo].[UserLogins] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [UserId]       NVARCHAR (MAX) NULL,
    [UserType]     NVARCHAR (MAX) NULL,
    [Browser]      NVARCHAR (MAX) NULL,
    [Client]       NVARCHAR (MAX) NULL,
    [LoginTime]    DATETIME2 (7)  NULL,
    [Status]       NVARCHAR (MAX) NULL,
    [LoginMessage] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED ([Id] ASC)
);

