CREATE TABLE [dbo].[Værktøj] (
    [VærktøjsId]    INT           IDENTITY (1, 1) NOT NULL,
    [Anskaffet]     DATE          NOT NULL,
    [Fabrikat]      NVARCHAR (50) NULL,
    [Model]         NVARCHAR (50) NULL,
    [Serienr]       NVARCHAR (50) NULL,
    [Type]          NVARCHAR (50) NULL,
    [Værktøjskasse] INT           NULL,
    CONSTRAINT [PK_Værktøj] PRIMARY KEY CLUSTERED ([VærktøjsId] ASC),
    CONSTRAINT [FK_Værktøj_Værktøjskasse] FOREIGN KEY ([Værktøjskasse]) REFERENCES [dbo].[Værktøjskasse] ([VKasseId])
);

