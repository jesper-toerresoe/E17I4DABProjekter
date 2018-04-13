SET IDENTITY_INSERT [dbo].[Håndværker] ON
INSERT INTO [dbo].[Håndværker] ([HåndværkerId], [Ansættelsedato], [Efternavn], [Fagområde], [Fornavn]) VALUES (245, N'2018-04-11', N'Hansen', N'Snedker', N'Peter')
INSERT INTO [dbo].[Håndværker] ([HåndværkerId], [Ansættelsedato], [Efternavn], [Fagområde], [Fornavn]) VALUES (366, N'2016-03-19', N'Sørensn', N'Smed', N'Søren')
SET IDENTITY_INSERT [dbo].[Håndværker] OFF
SET IDENTITY_INSERT [dbo].[Værktøjskasse] ON
INSERT INTO [dbo].[Værktøjskasse] ([VKasseId], [Anskaffet], [Fabrikat], [Håndværker], [Model], [Serienummer], [Farve]) VALUES (5664, N'2018-04-11', N'Bosch', 245, N'xxf6', N'3475693', N'Grøn')
INSERT INTO [dbo].[Værktøjskasse] ([VKasseId], [Anskaffet], [Fabrikat], [Håndværker], [Model], [Serienummer], [Farve]) VALUES (33221, N'2013-07-10', N'AEG', 366, N'Alpfa', N'767458', N'Blå')

SET IDENTITY_INSERT [dbo].[Værktøjskasse] OFF
SET IDENTITY_INSERT [dbo].[Værktøj] ON
INSERT INTO [dbo].[Værktøj] ([VærktøjsId], [Anskaffet], [Fabrikat], [Model], [Serienr], [Type], [Værktøjskasse]) VALUES (636, N'2018-04-11', N'ToolMate', N'Hammer', N'46745', N'Lægtehammer', 5664)
INSERT INTO [dbo].[Værktøj] ([VærktøjsId], [Anskaffet], [Fabrikat], [Model], [Serienr], [Type], [Værktøjskasse]) VALUES (1111, N'2018-04-11', N'Bacho', N'Skruetrækker', N'vh374w', N'Philips', 33221)
SET IDENTITY_INSERT [dbo].[Værktøj] OFF