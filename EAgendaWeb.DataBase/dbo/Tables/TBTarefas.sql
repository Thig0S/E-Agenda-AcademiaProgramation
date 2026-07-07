CREATE TABLE [dbo].[TBTarefas]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Titulo] NVARCHAR (50) NOT NULL,
    [Prioridade] NVARCHAR (50) NOT NULL,
    [DataCriacao] DATE NOT NULL,
    [DataConclusao] DATE NOT NULL,
    [StatusDeConclusao] NVARCHAR (50) NOT NULL,
);
GO

ALTER TABLE [dbo].[TBTarefas]
    ADD CONSTRAINT [PK_TBTarefas] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

