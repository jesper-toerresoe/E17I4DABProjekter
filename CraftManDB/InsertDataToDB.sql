SET IDENTITY_INSERT [dbo].[Håndværker] ON
INSERT INTO [dbo].[Håndværker] ([HåndværkerId], [Ansættelsedato], [Efternavn], [Fagområde], [Fornavn]) VALUES (1, N'2018-04-11', N'Hansen', N'Snedker', N'Peter')
INSERT INTO [dbo].[Håndværker] ([HåndværkerId], [Ansættelsedato], [Efternavn], [Fagområde], [Fornavn]) VALUES (2, N'2016-03-19', N'Sørensn', N'Smed', N'Søren')
SET IDENTITY_INSERT [dbo].[Håndværker] OFF
SET IDENTITY_INSERT [dbo].[Værktøjskasse] ON
INSERT INTO [dbo].[Værktøjskasse] ([VKasseId], [Anskaffet], [Fabrikat], [Håndværker], [Model], [Serienummer], [Farve]) VALUES (1, N'2018-04-11', N'Bosch', 1, N'xxf6', N'3475693', N'Grøn')
INSERT INTO [dbo].[Værktøjskasse] ([VKasseId], [Anskaffet], [Fabrikat], [Håndværker], [Model], [Serienummer], [Farve]) VALUES (33, N'2013-07-10', N'AEG', 2, N'Alpfa', N'767458', N'Blå')

SET IDENTITY_INSERT [dbo].[Værktøjskasse] OFF
SET IDENTITY_INSERT [dbo].[Værktøj] ON
INSERT INTO [dbo].[Værktøj] ([VærktøjsId], [Anskaffet], [Fabrikat], [Model], [Serienr], [Type], [Værktøjskasse]) VALUES (111, N'2018-04-11', N'ToolMate', N'Hammer', N'46745', N'Lægtehammer', 1)
INSERT INTO [dbo].[Værktøj] ([VærktøjsId], [Anskaffet], [Fabrikat], [Model], [Serienr], [Type], [Værktøjskasse]) VALUES (5016, N'2018-04-11', N'Bacho', N'Skruetrækker', N'vh374w', N'Philips', 33)
SET IDENTITY_INSERT [dbo].[Værktøj] OFF