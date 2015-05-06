GO
CREATE TABLE Seances.Telephone
(
	TelephoneID int NOT NULL IDENTITY,
	SeanceID int NOT NULL,
	NumTel varchar(20)
)

GO
ALTER TABLE Seances.Telephone
	ADD CONSTRAINT PK_Telephone_TelephoneID
	PRIMARY KEY (TelephoneID);


GO
ALTER TABLE Seances.Telephone
	ADD CONSTRAINT FK_Telephone_Seance_SeanceID
	FOREIGN KEY (TelephoneID)
	REFERENCES Seances.Seance(SeanceID);


GO
ALTER TABLE Seances.Seance
ADD Telephone1 varchar(20) NULL,
	Telephone2 varchar(20) NULL,
	Telephone3 varchar(20) NULL;

USE [H15_PROJET_E07]
GO
ALTER TABLE Seances.Seance
	ADD HeureRDV int NULL,
	MinuteRDV int NULL;



GO
ALTER TABLE Seances.Seance
	ADD Nom varchar(50) NULL,
	Prenom varchar(50) NULL;