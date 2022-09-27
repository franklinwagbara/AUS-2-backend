CREATE TABLE [dbo].[Companies] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (MAX) NULL,
    [RegisteredAddressId] INT            NULL,
    [RegisteredAddress]   NVARCHAR (MAX) NULL,
    [CacNumber]           NVARCHAR (MAX) NULL,
    [TIN]                 NVARCHAR (MAX) NULL,
    [NationalityId]       INT            NOT NULL,
    [StateId]             INT            NOT NULL,
    [YearIncorporated]    NVARCHAR (MAX) NULL,
    [PostalCode]          NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED ([Id] ASC)
);

