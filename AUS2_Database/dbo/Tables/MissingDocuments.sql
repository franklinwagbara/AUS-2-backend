CREATE TABLE [dbo].[MissingDocuments] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [ApplicationId] INT NOT NULL,
    [DocId]         INT NOT NULL,
    CONSTRAINT [PK_MissingDocuments] PRIMARY KEY CLUSTERED ([Id] ASC)
);



