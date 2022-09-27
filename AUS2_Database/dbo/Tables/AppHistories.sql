CREATE TABLE [dbo].[AppHistories] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationId]        INT            NOT NULL,
    [FieldLocationApply]   NVARCHAR (MAX) NULL,
    [CurrentStageId]       SMALLINT       NULL,
    [Action]               NVARCHAR (MAX) NULL,
    [ActionDate]           DATETIME2 (7)  NULL,
    [TriggeredBy]          NVARCHAR (MAX) NULL,
    [TriggeredByRole]      NVARCHAR (MAX) NULL,
    [Message]              NVARCHAR (MAX) NULL,
    [TargetedTo]           NVARCHAR (MAX) NULL,
    [TargetedToRole]       NVARCHAR (MAX) NULL,
    [NextStateId]          SMALLINT       NULL,
    [StatusMode]           NVARCHAR (MAX) NULL,
    [ActionMode]           NVARCHAR (MAX) NULL,
    [ApplicationRequestId] INT            NULL,
    CONSTRAINT [PK_AppHistories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AppHistories_Applications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Applications] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AppHistories_ApplicationId]
    ON [dbo].[AppHistories]([ApplicationId] ASC);

