CREATE TABLE [dbo].[Applications] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [UserId]            NVARCHAR (450) NULL,
    [PhaseId]           INT            NOT NULL,
    [FacilityId]        INT            NOT NULL,
    [ApplicationTypeId] INT            NOT NULL,
    [Reference]         NVARCHAR (MAX) NULL,
    [FlowStageId]       INT            NULL,
    [Status]            NVARCHAR (MAX) NULL,
    [IsLegacy]          NVARCHAR (MAX) NULL,
    [CurrentUser]       NVARCHAR (MAX) NULL,
    [AddedDate]         DATETIME2 (7)  NULL,
    [ModifiedDate]      DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Applications_ApplicationTypes_ApplicationTypeId] FOREIGN KEY ([ApplicationTypeId]) REFERENCES [dbo].[ApplicationTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Applications_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Applications_Facilities_FacilityId] FOREIGN KEY ([FacilityId]) REFERENCES [dbo].[Facilities] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Applications_flowStages_FlowStageId] FOREIGN KEY ([FlowStageId]) REFERENCES [dbo].[flowStages] ([Id]),
    CONSTRAINT [FK_Applications_Phases_PhaseId] FOREIGN KEY ([PhaseId]) REFERENCES [dbo].[Phases] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Applications_UserId]
    ON [dbo].[Applications]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Applications_PhaseId]
    ON [dbo].[Applications]([PhaseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Applications_FlowStageId]
    ON [dbo].[Applications]([FlowStageId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Applications_FacilityId]
    ON [dbo].[Applications]([FacilityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Applications_ApplicationTypeId]
    ON [dbo].[Applications]([ApplicationTypeId] ASC);

