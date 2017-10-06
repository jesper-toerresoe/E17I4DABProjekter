CREATE TABLE [dbo].[Værktøjskasse] (
    [VKasseId]    INT            IDENTITY (1, 1) NOT NULL,
    [Anskaffet]   DATE           NOT NULL,
    [Fabrikat]    NVARCHAR (50)  NULL,
    [Håndværker]  INT            NULL,
    [Model]       NVARCHAR (50)  NULL,
    [Serienummer] NVARCHAR (50)  NULL,
    [Farve]       NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Værktøjskasse] PRIMARY KEY CLUSTERED ([VKasseId] ASC),
    CONSTRAINT [FK_Værktøjskasse_ToHåndværker] FOREIGN KEY ([Håndværker]) REFERENCES [dbo].[Håndværker] ([HåndværkerId])
);

