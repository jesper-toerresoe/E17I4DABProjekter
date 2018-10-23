SELECT * FROM Håndværker
SELECT * FROM Værktøjskasse
SELECT * FROM Værktøj
SELECT * FROM Håndværker
SELECT [Efternavn],[Ansættelsedato],[Fagområde],[Fornavn] FROM Håndværker WHERE ([HåndværkerId] = 1)

SELECT        Håndværker.HåndværkerId, Håndværker.Ansættelsedato, Håndværker.Efternavn, Håndværker.Fagområde, Håndværker.Fornavn, Værktøjskasse.VKasseId, Værktøjskasse.Anskaffet, Værktøjskasse.Fabrikat, Værktøj.VærktøjsId, 
                         Værktøj.Anskaffet AS VKTAnskaf, Værktøj.Fabrikat AS Expr2, Værktøj.Model, Værktøj.Serienr, Værktøj.Type
FROM            Håndværker LEFT OUTER JOIN
                         Værktøjskasse ON Håndværker.HåndværkerId = Værktøjskasse.Håndværker LEFT OUTER JOIN
                         Værktøj ON Værktøjskasse.VKasseId = Værktøj.Værktøjskasse
WHERE        (Håndværker.HåndværkerId = 1)

SELECT        Håndværker.HåndværkerId, Håndværker.Ansættelsedato, Håndværker.Efternavn, Håndværker.Fagområde, Håndværker.Fornavn, Værktøjskasse.VKasseId, Værktøjskasse.Anskaffet, Værktøjskasse.Fabrikat, Værktøjskasse.Model, 
                         Værktøjskasse.Serienummer, Værktøjskasse.Farve, Værktøj.VærktøjsId, Værktøj.Anskaffet AS Expr1, Værktøj.Fabrikat AS Expr2, Værktøj.Model AS Expr3, Værktøj.Serienr, Værktøj.Type
FROM            Håndværker INNER JOIN
                         Værktøjskasse ON Håndværker.HåndværkerId = Værktøjskasse.Håndværker INNER JOIN
                         Værktøj ON Værktøjskasse.VKasseId = Værktøj.Værktøjskasse
WHERE        (Håndværker.HåndværkerId = 1)

SELECT        Håndværker.HåndværkerId, Håndværker.Ansættelsedato, Håndværker.Efternavn, Håndværker.Fagområde, Håndværker.Fornavn, Værktøjskasse.VKasseId, Værktøjskasse.Anskaffet, Værktøjskasse.Fabrikat, Værktøjskasse.Model, 
                         Værktøjskasse.Serienummer, Værktøjskasse.Farve, Værktøj.VærktøjsId, Værktøj.Anskaffet AS Expr1, Værktøj.Fabrikat AS Expr2, Værktøj.Model AS Expr3, Værktøj.Serienr, Værktøj.Type
FROM            Håndværker INNER JOIN
                         Værktøjskasse ON Håndværker.HåndværkerId = Værktøjskasse.Håndværker INNER JOIN
                         Værktøj ON Værktøjskasse.VKasseId = Værktøj.Værktøjskasse
WHERE        (Håndværker.HåndværkerId = 1)

SELECT  Håndværker.HåndværkerId, Håndværker.Ansættelsedato, Håndværker.Efternavn, Håndværker.Fagområde, Håndværker.Fornavn, Værktøjskasse.VKasseId, Værktøjskasse.Anskaffet, Værktøjskasse.Fabrikat, Værktøjskasse.Håndværker, 
                Værktøjskasse.Model, Værktøjskasse.Serienummer, Værktøjskasse.Farve, Værktøj.VærktøjsId, Værktøj.Anskaffet AS VTAnskaffet, Værktøj.Fabrikat AS VTFabrikat, Værktøj.Model AS VTModel, Værktøj.Serienr, Værktøj.Type, 
                Værktøj.Værktøjskasse
                FROM      Håndværker INNER JOIN
                Værktøjskasse ON Håndværker.HåndværkerId = Værktøjskasse.Håndværker INNER JOIN
                Værktøj ON Værktøjskasse.VKasseId = Værktøj.Værktøjskasse
                WHERE   (Håndværker.HåndværkerId = 1)

SELECT Håndværker.HåndværkerId, Håndværker.Ansættelsedato, Håndværker.Efternavn, Håndværker.Fagområde, Håndværker.Fornavn, Værktøjskasse.VKasseId, Værktøjskasse.VTKAnskaffet, Værktøjskasse.VTKFabrikat, Værktøjskasse.VTKModel, 
                Værktøjskasse.VTKSerienummer, Værktøjskasse.VTKFarve, Værktøj.VærktøjsId, Værktøj.VTAnskaffet, Værktøj.VTFabrikat, Værktøj.VTModel, Værktøj.VTSerienr, Værktøj.VTType, Værktøj.VTKId, Værktøjskasse.HåndværkerID AS Expr1
                FROM      Håndværker INNER JOIN
                Værktøjskasse ON Håndværker.HåndværkerId = Værktøjskasse.HåndværkerID INNER JOIN
                Værktøj ON Værktøjskasse.VKasseId = Værktøj.VTKId
                WHERE   (Håndværker.HåndværkerId = 1)