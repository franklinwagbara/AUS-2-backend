CREATE TABLE [dbo].[OutOfOffices] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Reliever]  NVARCHAR (MAX) NULL,
    [Relieved]  NVARCHAR (MAX) NULL,
    [StartDate] DATETIME2 (7)  NULL,
    [EndDate]   DATETIME2 (7)  NULL,
    [Comment]   NVARCHAR (MAX) NULL,
    [Status]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_OutOfOffices] PRIMARY KEY CLUSTERED ([Id] ASC)
);

