CREATE TABLE [dbo].[SubmittedDocuments] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationId] INT            NOT NULL,
    [FileId]        INT            NOT NULL,
    [DocId]         INT            NOT NULL,
    [DocSource]     NVARCHAR (MAX) NULL,
    [DocType]       NVARCHAR (MAX) NULL,
    [DocName]       NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_SubmittedDocuments] PRIMARY KEY CLUSTERED ([Id] ASC)
);

