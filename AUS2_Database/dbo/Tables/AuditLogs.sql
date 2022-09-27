CREATE TABLE [dbo].[AuditLogs] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [UserId]          NVARCHAR (450) NULL,
    [Type]            NVARCHAR (MAX) NULL,
    [TableName]       NVARCHAR (MAX) NULL,
    [Date]            DATETIME2 (7)  NOT NULL,
    [OldValues]       NVARCHAR (MAX) NULL,
    [NewValues]       NVARCHAR (MAX) NULL,
    [AffectedColumns] NVARCHAR (MAX) NULL,
    [PrimaryKey]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AuditLogs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuditLogs_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AuditLogs_UserId]
    ON [dbo].[AuditLogs]([UserId] ASC);

