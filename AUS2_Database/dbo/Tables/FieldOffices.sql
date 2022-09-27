CREATE TABLE [dbo].[FieldOffices] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Address]   NVARCHAR (MAX) NULL,
    [StateId]   INT            NOT NULL,
    [AddedDate] DATETIME2 (7)  NOT NULL,
    [Status]    BIT            NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_FieldOffices] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FieldOffices_States_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[States] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_FieldOffices_StateId]
    ON [dbo].[FieldOffices]([StateId] ASC);

