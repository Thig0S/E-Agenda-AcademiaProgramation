CREATE TABLE [dbo].[TBCategoria_Despesa] (
    [DespesaId]   UNIQUEIDENTIFIER NOT NULL,
    [CategoriaID] UNIQUEIDENTIFIER NULL
);
GO

ALTER TABLE [dbo].[TBCategoria_Despesa]
    ADD CONSTRAINT [FK_TBCategoriaDespensa_TBDespensa] FOREIGN KEY ([DespesaId]) REFERENCES [dbo].[TBDespesa] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[TBCategoria_Despesa]
    ADD CONSTRAINT [FK_TBCategoriaDespensa_TBDCategoria] FOREIGN KEY ([CategoriaID]) REFERENCES [dbo].[TBCategoria] ([Id]);
GO

