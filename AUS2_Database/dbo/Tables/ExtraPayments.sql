CREATE TABLE [dbo].[ExtraPayments] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [ApplicationId]      INT             NOT NULL,
    [OrderId]            NVARCHAR (MAX)  NULL,
    [TransactionDate]    DATETIME2 (7)   NULL,
    [TransactionId]      NVARCHAR (MAX)  NULL,
    [Description]        NVARCHAR (MAX)  NULL,
    [Rrreference]        NVARCHAR (MAX)  NULL,
    [AppReceiptId]       NVARCHAR (MAX)  NULL,
    [TxnAmount]          DECIMAL (18, 2) NULL,
    [Arrears]            DECIMAL (18, 2) NOT NULL,
    [BankCode]           NVARCHAR (MAX)  NULL,
    [Account]            NVARCHAR (MAX)  NULL,
    [TxnMessage]         NVARCHAR (MAX)  NULL,
    [Status]             NVARCHAR (MAX)  NULL,
    [RetryCount]         INT             NULL,
    [CreatedOn]          DATETIME2 (7)   NULL,
    [LastRetryDate]      DATETIME2 (7)   NULL,
    [ExtraPaymentAppRef] NVARCHAR (MAX)  NULL,
    [SanctionType]       NVARCHAR (MAX)  NULL,
    [ExtraPaymentBy]     NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_ExtraPayments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExtraPayments_Applications_ApplicationId] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Applications] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ExtraPayments_ApplicationId]
    ON [dbo].[ExtraPayments]([ApplicationId] ASC);

