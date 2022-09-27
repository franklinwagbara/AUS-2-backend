CREATE TABLE [dbo].[PhaseDocuments] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CatoeryId]   INT            NOT NULL,
    [PhaseId]     INT            NOT NULL,
    [ApptypeId]   INT            NOT NULL,
    [DocId]       INT            NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [DocType]     NVARCHAR (MAX) NULL,
    [IsMandatory] BIT            NOT NULL,
    [Status]      BIT            NOT NULL,
    [SortId]      INT            NULL,
    [CategoryId]  INT            NULL,
    CONSTRAINT [PK_PhaseDocuments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PhaseDocuments_ApplicationTypes_ApptypeId] FOREIGN KEY ([ApptypeId]) REFERENCES [dbo].[ApplicationTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PhaseDocuments_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]),
    CONSTRAINT [FK_PhaseDocuments_Phases_PhaseId] FOREIGN KEY ([PhaseId]) REFERENCES [dbo].[Phases] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PhaseDocuments_PhaseId]
    ON [dbo].[PhaseDocuments]([PhaseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PhaseDocuments_CategoryId]
    ON [dbo].[PhaseDocuments]([CategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PhaseDocuments_ApptypeId]
    ON [dbo].[PhaseDocuments]([ApptypeId] ASC);

