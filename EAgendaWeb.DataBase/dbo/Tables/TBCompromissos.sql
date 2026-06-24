CREATE TABLE [dbo].[TBCompromissos] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [Assunto]           NVARCHAR (100)   NOT NULL,
    [DataAcorrencia]    DATE             NOT NULL,
    [HoraInicio]        TIME (0)         NOT NULL,
    [HoraTermino]       TIME (0)         NOT NULL,
    [TipoDeCompromisso] NVARCHAR (50)    NOT NULL,
    [Local]             NVARCHAR (50)    NOT NULL,
    [ContatoId]         UNIQUEIDENTIFIER NULL
);
GO

ALTER TABLE [dbo].[TBCompromissos]
    ADD CONSTRAINT [FK_TBCompromisso_TBContato] FOREIGN KEY ([ContatoId]) REFERENCES [dbo].[TBContatos] ([Id]);
GO

ALTER TABLE [dbo].[TBCompromissos]
    ADD CONSTRAINT [PK_TBCompromissos] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

