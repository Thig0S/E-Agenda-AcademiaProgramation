CREATE TABLE [dbo].[TBItensTarefa]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Titulo] NVARCHAR (100) NOT NULL,
    [Concluido] BIT NOT NULL,
    [TarefaId] UNIQUEIDENTIFIER NOT NULL
);
GO

ALTER TABLE [dbo].[TBItensTarefa]
    ADD CONSTRAINT [PK_TBItensTarefa] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[TBItensTarefa]
    ADD CONSTRAINT [FK_TBItensTarefa_TBTarefa] FOREIGN KEY ([TarefaId]) REFERENCES [dbo].[TBTarefas] ([Id]);
GO