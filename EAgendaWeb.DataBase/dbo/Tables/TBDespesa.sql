CREATE TABLE [dbo].[TBDespesa] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Descricao]      NVARCHAR (250)   NOT NULL,
    [Valor]          DECIMAL (10)     NOT NULL,
    [FormaPagamento] NVARCHAR (50)    NOT NULL
);
GO

ALTER TABLE [dbo].[TBDespesa]
    ADD CONSTRAINT [PK_TBDespesa] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

