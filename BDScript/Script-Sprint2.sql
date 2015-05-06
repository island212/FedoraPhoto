USE [H15_PROJET_E07]
GO

CREATE TABLE Seances.Forfait
(
	ForfaitID int NOT NULL IDENTITY,
	NomForfait varchar(50) NOT NULL,
	DescriptionForfait nvarchar(200) NOT NULL,
	PrixForfait decimal(10,2) NOT NULL,
	NbPhotos int NOT NULL,
	Temps int NOT NULL
)

ALTER TABLE Seances.Seance
	ADD ForfaitID int NULL;
	
ALTER TABLE Seances.Seance
	ADD Statut nvarchar(50) NOT NULL DEFAULT 'demandée';


ALTER TABLE Seances.Photo
	DROP COLUMN Photo

ALTER TABLE Seances.Photo
	ADD PhotoPath nvarchar(200) NOT NULL,
	PhotoType nvarchar(20) NOT NULL;


ALTER TABLE Seances.Seance ADD
	DateDispo Datetime NULL,
	DateFacture Datetime NULL;


/* Primary Keys: */

ALTER TABLE Seances.Forfait
	ADD CONSTRAINT PK_Forfait_ForfaitID
	PRIMARY KEY (ForfaitID);



/* Foreign Keys: */

ALTER TABLE Seances.Seance
	ADD CONSTRAINT FK_Seance_Forfait_ForfaitID
	FOREIGN KEY (ForfaitID)
	REFERENCES Seances.Forfait(ForfaitID);




/*  d’afficher pour une propriété les informations sur l’agent, le photographe,
 le prix du forfait et la galerie de photos de la propriété. */


SET NOCOUNT ON
USE [H15_PROJET_E07]

IF OBJECT_ID('Seances.uvwInfoSeances', 'V') IS NOT NULL DROP VIEW Seances.uvwInfoSeances
GO

CREATE VIEW Seances.uvwInfoSeances
AS

SELECT b.PhotoName, b.PhotoType, b.PhotoPath, c.Nom, c.Prenom, c.Courriel, d.NomForfait, d.DescriptionForfait, d.PrixForfait, a.Adresse
FROM Seances.Seance a
INNER JOIN Seances.Photo b
ON a.SeanceID = b.SeanceID
INNER JOIN Agents.Agent c
ON a.AgentID = c.AgentID
INNER JOIN [Seances].[Forfait] d
ON a.ForfaitID = d.ForfaitID;

GO



/* Triggers: */


/* Confirmée / Reportée */
USE [H15_PROJET_E07]
GO

IF OBJECT_ID('Seances.trgConfirmee') IS NOT NULL DROP TRIGGER Seances.trgConfirmee
GO

CREATE TRIGGER Seances.trgConfirmee ON Seances.Seance FOR UPDATE
AS

IF NOT EXISTS(SELECT * FROM inserted) RETURN;

DECLARE @idSeance int = (SELECT TOP(1) SeanceID FROM inserted)
DECLARE @dateAvant datetime = (SELECT TOP(1) DateSeance FROM deleted)
DECLARE @dateApres datetime = (SELECT TOP(1) DateSeance FROM inserted)


IF(@dateAvant IS NULL AND @dateApres IS NOT NULL)
BEGIN
	UPDATE Seances.Seance SET Statut = 'Confirmée' WHERE SeanceID = @idSeance
END
ELSE IF (@dateAvant IS NOT NULL AND @dateApres IS NOT NULL AND @dateAvant != @dateApres)
BEGIN
	UPDATE Seances.Seance SET Statut = 'Reportée' WHERE SeanceID = @idSeance
END

GO


/* Réalisé */
USE [H15_PROJET_E07]
GO

IF OBJECT_ID('Seances.trgRealisee') IS NOT NULL DROP TRIGGER Seances.trgRealisee
GO

CREATE TRIGGER Seances.trgRealisee ON Seances.Seance FOR UPDATE
AS

IF NOT EXISTS(SELECT * FROM inserted) RETURN;

DECLARE @idSeance int = (SELECT TOP(1) SeanceID FROM inserted)
DECLARE @dateAvant datetime = (SELECT TOP(1) DateDispo FROM deleted)
DECLARE @dateApres datetime = (SELECT TOP(1) DateDispo FROM inserted)

IF(@dateAvant IS NULL AND @dateApres IS NOT NULL)
BEGIN
	UPDATE Seances.Seance SET Statut = 'Réalisé' WHERE SeanceID = @idSeance
END
GO


/* Livrée */

USE [H15_PROJET_E07]
GO

IF OBJECT_ID('Seances.trgLivree') IS NOT NULL DROP TRIGGER Seances.trgLivree
GO

CREATE TRIGGER Seances.trgLivree ON Seances.Photo FOR INSERT
AS

IF NOT EXISTS(SELECT * FROM inserted) RETURN;

DECLARE @idSeance int = (SELECT TOP(1) SeanceID FROM inserted)

UPDATE Seances.Seance SET Statut = 'Livrée' WHERE SeanceID = @idSeance

GO





