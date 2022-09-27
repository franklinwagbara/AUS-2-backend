CREATE TABLE [dbo].[Categories] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [Code]        NVARCHAR (MAX) NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Status]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([Id] ASC)
);

