CREATE TABLE [dbo].[Værktøj] (
    [VærktøjsId]    BIGINT           IDENTITY (1, 1) NOT NULL,
    [VTAnskaffet]     DATE          NOT NULL,
    [VTFabrikat]      NVARCHAR (MAX) NULL,
    [VTModel]         NVARCHAR (50) NULL,
    [VTSerienr]       NVARCHAR (50) NULL,
    [VTType]          NVARCHAR (50) NULL,
    [VTKId] INT           NULL,
    CONSTRAINT [PK_Værktøj] PRIMARY KEY CLUSTERED ([VærktøjsId] ASC),
    CONSTRAINT [FK_Værktøj_Værktøjskasse] FOREIGN KEY ([VTKId]) REFERENCES [dbo].[Værktøjskasse] ([VKasseId])
);

