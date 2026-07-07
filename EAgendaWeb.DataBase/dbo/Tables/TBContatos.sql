CREATE TABLE [dbo].[TBContatos] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Nome]     NVARCHAR (100)   NOT NULL,
    [Email]    NVARCHAR (100)   NOT NULL,
    [Telefone] NVARCHAR (50)    NOT NULL,
    [Cargo]    NVARCHAR (50)    NULL,
    [Empresa]  NVARCHAR (50)    NULL
);
GO

ALTER TABLE [dbo].[TBContatos]
    ADD CONSTRAINT [PK_TBContatos] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

