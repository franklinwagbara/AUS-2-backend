CREATE TABLE [dbo].[ApplicationForm] (
    [Id]                              INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationId]                   INT            NOT NULL,
    [LandSize]                        INT            NULL,
    [WellLocationCategory]            NVARCHAR (MAX) NULL,
    [Field]                           NVARCHAR (MAX) NULL,
    [Block]                           NVARCHAR (MAX) NULL,
    [Terrain]                         NVARCHAR (MAX) NULL,
    [WellSpudName]                    NVARCHAR (MAX) NULL,
    [WellPreSpudName]                 NVARCHAR (MAX) NULL,
    [WellSurfaceCoordinates]          NVARCHAR (MAX) NULL,
    [WellClassApplied]                NVARCHAR (MAX) NULL,
    [ProposedRig]                     NVARCHAR (MAX) NULL,
    [ExpectedVolumes]                 NVARCHAR (MAX) NULL,
    [TargetReserves]                  NVARCHAR (MAX) NULL,
    [Afe]                             NVARCHAR (MAX) NULL,
    [EstimatedOperationsDays]         INT            NOT NULL,
    [WellName]                        NVARCHAR (MAX) NULL,
    [NatureOfOperation]               NVARCHAR (MAX) NULL,
    [WellCompletionInterval]          NVARCHAR (MAX) NULL,
    [RigForOperation]                 NVARCHAR (MAX) NULL,
    [PreOperationProductionRate]      NVARCHAR (MAX) NULL,
    [PostOperationProductionRate1]    NVARCHAR (MAX) NULL,
    [InitialReservesAllocationOfWell] NVARCHAR (MAX) NULL,
    [CumulativeProductionForWell]     NVARCHAR (MAX) NULL,
    [PlugbackInterval]                NVARCHAR (MAX) NULL,
    [LastProductionRate]              NVARCHAR (MAX) NULL,
    [SpudDate]                        DATETIME2 (7)  NULL,
    CONSTRAINT [PK_ApplicationForm] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApplicationForm_Applications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Applications] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationForm_ApplicationId]
    ON [dbo].[ApplicationForm]([ApplicationId] ASC);

