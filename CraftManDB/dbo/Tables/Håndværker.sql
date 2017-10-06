CREATE TABLE [dbo].[Håndværker] (
    [HåndværkerId]   INT           IDENTITY (1, 1) NOT NULL,
    [Ansættelsedato] DATE          NOT NULL,
    [Efternavn]      NVARCHAR (50) NOT NULL,
    [Fagområde]      NVARCHAR (50) NULL,
    [Fornavn]        NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Håndværker] PRIMARY KEY CLUSTERED ([HåndværkerId] ASC)
);

