CREATE TABLE [dbo].[Facilities] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [CompanyId]  INT            NOT NULL,
    [LgaId]      INT            NOT NULL,
    [ElpsId]     INT            NOT NULL,
    [Address]    NVARCHAR (MAX) NULL,
    [Cordinates] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Facilities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Facilities_LGAs_LgaId] FOREIGN KEY ([LgaId]) REFERENCES [dbo].[LGAs] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Facilities_LgaId]
    ON [dbo].[Facilities]([LgaId] ASC);

