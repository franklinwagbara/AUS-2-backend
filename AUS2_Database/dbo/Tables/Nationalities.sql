CREATE TABLE [dbo].[Nationalities] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CountryName] NVARCHAR (MAX) NULL,
    [CountryCode] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Nationalities] PRIMARY KEY CLUSTERED ([Id] ASC)
);

