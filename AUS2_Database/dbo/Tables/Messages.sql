CREATE TABLE [dbo].[Messages] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Subject]       NVARCHAR (MAX) NULL,
    [Content]       NVARCHAR (MAX) NULL,
    [Read]          INT            NULL,
    [CompanyId]     INT            NULL,
    [SenderId]      NVARCHAR (MAX) NULL,
    [Date]          DATETIME2 (7)  NULL,
    [ApplicationId] INT            NOT NULL,
    [UserId]        INT            NULL,
    [UserType]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Messages_Applications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Applications] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Messages_ApplicationId]
    ON [dbo].[Messages]([ApplicationId] ASC);

