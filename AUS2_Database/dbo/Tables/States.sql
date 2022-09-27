CREATE TABLE [dbo].[States] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [StateCode]    NVARCHAR (MAX) NULL,
    [StateName]    NVARCHAR (MAX) NULL,
    [StateAddress] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED ([Id] ASC)
);

