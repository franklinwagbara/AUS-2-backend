CREATE TABLE [dbo].[Phases] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [CategoryId]    INT             NOT NULL,
    [Sort]          INT             NOT NULL,
    [Code]          NVARCHAR (MAX)  NULL,
    [ShortName]     NVARCHAR (MAX)  NULL,
    [Fee]           DECIMAL (18, 2) NOT NULL,
    [ServiceCharge] DECIMAL (18, 2) NOT NULL,
    [Description]   NVARCHAR (MAX)  NULL,
    [Status]        BIT             NOT NULL,
    [LicenseSerial] INT             NOT NULL,
    CONSTRAINT [PK_Phases] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Phases_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Phases_CategoryId]
    ON [dbo].[Phases]([CategoryId] ASC);

