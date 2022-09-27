CREATE TABLE [dbo].[flowStages] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    [StateType] NVARCHAR (MAX) NULL,
    [Rate]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_flowStages] PRIMARY KEY CLUSTERED ([Id] ASC)
);

