CREATE TABLE [dbo].[Appointments] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [ApplicationId]        INT            NOT NULL,
    [TypeOfAppoinment]     NVARCHAR (MAX) NULL,
    [AppointmentDate]      DATETIME2 (7)  NULL,
    [AppointmentNote]      NVARCHAR (MAX) NULL,
    [AppointmentVenue]     NVARCHAR (MAX) NULL,
    [ScheduledBy]          NVARCHAR (MAX) NULL,
    [ScheduledDate]        DATETIME2 (7)  NULL,
    [ContactPerson]        NVARCHAR (MAX) NULL,
    [ContactPhone]         NVARCHAR (MAX) NULL,
    [LastApprovedCustDate] DATETIME2 (7)  NULL,
    [LastCustComment]      NVARCHAR (MAX) NULL,
    [Status]               NVARCHAR (MAX) NULL,
    [SchduleExpiryDate]    DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED ([Id] ASC)
);

