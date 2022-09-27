CREATE TABLE [dbo].[Payments] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [ApplicationId]        INT             NOT NULL,
    [TransactionDate]      DATETIME2 (7)   NULL,
    [TransactionId]        NVARCHAR (MAX)  NULL,
    [Description]          NVARCHAR (MAX)  NULL,
    [Rrreference]          NVARCHAR (MAX)  NULL,
    [AppReceiptId]         NVARCHAR (MAX)  NULL,
    [TxnAmount]            DECIMAL (18, 2) NULL,
    [Arrears]              DECIMAL (18, 2) NOT NULL,
    [BankCode]             NVARCHAR (MAX)  NULL,
    [Account]              NVARCHAR (MAX)  NULL,
    [TxnMessage]           NVARCHAR (MAX)  NULL,
    [Status]               NVARCHAR (MAX)  NULL,
    [RetryCount]           INT             NULL,
    [LastRetryDate]        DATETIME2 (7)   NULL,
    [ActionBy]             NVARCHAR (MAX)  NULL,
    [ServiceCharge]        DECIMAL (18, 2) NULL,
    [ProcessingFee]        DECIMAL (18, 2) NULL,
    [ApplicationRequestId] INT             NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Payments_Applications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Applications] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Payments_ApplicationId]
    ON [dbo].[Payments]([ApplicationId] ASC);

