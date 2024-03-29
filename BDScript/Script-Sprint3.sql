USE [H15_PROJET_E07]
GO

-- Gestion de concurrence d'accès --

ALTER TABLE Seances.Seance
	ADD rowVersionSeance rowversion NULL

GO

CREATE TABLE Seances.Facture
(
	FactureID int NOT NULL IDENTITY,
	FraisDeplacement int NOT NULL DEFAULT(0),
	FraisVisiteVirtuelle int NOT NULL DEFAULT(0),
	FraisTVQ int NOT NULL DEFAULT(0),
	FraisTPS int NOT NULL DEFAULT(0),
	SeanceID int NOT NULL,
	Total AS Seances.ufn_TotalFacture(SeanceID)
)

ALTER TABLE Seances.Facture
	ADD CONSTRAINT FK_Facture_SeanceID
	FOREIGN KEY (SeanceID)
	REFERENCES Seances.Seance(SeanceID);
GO

--index for the procedure and the function

CREATE NONCLUSTERED INDEX IX_Facture_SeanceID
ON Seances.Facture (
	SeanceID
);

-- Fonction --

--drop if needed
/*
DROP FUNCTION Seances.ufn_TotalFacture
GO
*/
--

CREATE FUNCTION Seances.ufn_TotalFacture
(
@seanceID int
)
RETURNS int
AS
	BEGIN
		DECLARE @prixForfait int = 0,
		@fraisDeplacement int = 0,
		@fraisVisiteVirtuelle int = 0,
		@fraisTVQ int = 0,
		@fraisTPS int = 0

		SELECT @prixForfait = fo.PrixForfait,
		@fraisDeplacement = fa.FraisDeplacement,
		@fraisVisiteVirtuelle = fa.FraisVisiteVirtuelle,
		@fraisTVQ = fa.FraisTVQ,
		@fraisTPS = fa.FraisTPS

		FROM Seances.Facture fa
		INNER JOIN Seances.Seance s
		ON fa.SeanceID = s.SeanceID
		INNER JOIN Seances.Forfait fo
		ON s.ForfaitID = fo.ForfaitID

		WHERE fa.SeanceID = @seanceID
		RETURN @prixForfait + @fraisDeplacement + @fraisVisiteVirtuelle + @fraisTVQ + @fraisTPS
	END
GO

-- Test Fonction --

/* needed test values since the table had just been created
INSERT INTO Seances.Facture
VALUES (10, 10, 10 ,10, 36)

SELECT * FROM Seances.Facture
*/

SELECT Seances.ufn_TotalFacture(36) AS 'Total'

-- Procedure --

--drop if needed
/*
DROP PROCEDURE Agents.usp_RapportVentes
GO
*/
--

CREATE PROCEDURE Agents.usp_RapportVentes
@mois int,
@annee int
AS
	BEGIN
		SELECT DateFacture, (CONVERT(VARCHAR, DateSeance, 103) + ' à ' + CONVERT(VARCHAR, HeureRDV) + ':' + CONVERT(VARCHAR, MinuteRDV)) AS 'Date de la Séance', (Prenom + ' ' + Nom) AS 'Nom Propriétaire', Adresse, fo.NomForfait, fo.PrixForfait, fa.FraisDeplacement, fa.FraisVisiteVirtuelle ,fa.FraisTPS, fa.FraisTVQ, Seances.ufn_TotalFacture(fa.SeanceID) AS 'TotalFacture'
		FROM Seances.Seance s
		INNER JOIN Seances.Forfait fo
		ON	fo.ForfaitID = s.ForfaitID
		INNER JOIN Seances.Facture fa
		ON fa.SeanceID = s.SeanceID
		WHERE (YEAR(DateFacture) = @annee) AND (MONTH(DateFacture) = @mois)
	END
GO

-- Test Procedure --

EXEC Agents.usp_RapportVentes @mois = 5, @annee = 2015

-- Suite des Triggers --

/* Facturée */

USE [H15_PROJET_E07]
GO

IF OBJECT_ID('Seances.trgFacturee') IS NOT NULL DROP TRIGGER Seances.trgFacturee
GO

CREATE TRIGGER Seances.trgFacturee ON Seances.Facture FOR INSERT
AS

IF NOT EXISTS(SELECT * FROM inserted) RETURN;

DECLARE @idSeance int = (SELECT TOP(1) SeanceID FROM inserted)

UPDATE Seances.Seance SET Statut = 'Facturée' WHERE SeanceID = @idSeance

GO

-- Amen --