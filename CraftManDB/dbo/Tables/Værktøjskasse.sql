CREATE TABLE [dbo].[Værktøjskasse] (
    [VKasseId]    INT            IDENTITY (1, 1) NOT NULL,
    [VTKAnskaffet]   DATE           NOT NULL,
    [VTKFabrikat]    NVARCHAR (50)  NULL,
    [HåndværkerID]  INT            NULL,
    [VTKModel]       NVARCHAR (50)  NULL,
    [VTKSerienummer] NVARCHAR (50)  NULL,
    [VTKFarve]       NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Værktøjskasse] PRIMARY KEY CLUSTERED ([VKasseId] ASC),
    CONSTRAINT [FK_Værktøjskasse_ToHåndværker] FOREIGN KEY ([HåndværkerID]) REFERENCES [dbo].[Håndværker] ([HåndværkerId])
);

