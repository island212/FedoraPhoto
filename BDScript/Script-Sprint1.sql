/* Créer les schemas: */
USE [H15_PROJET_E07];
GO
CREATE SCHEMA Agents;
GO
CREATE SCHEMA Seances;
GO
CREATE SCHEMA Photographes;
GO

/* Créer les tables: */


CREATE TABLE Agents.Agent
(
	AgentID int NOT NULL IDENTITY,
	Nom varchar(50) NOT NULL,
	Prenom varchar(50) NOT NULL,
	Telephone varchar(20) NOT NULL,
	Courriel varchar(50) NULL
)

CREATE TABLE Seances.Seance
(
	SeanceID int NOT NULL IDENTITY,
	AgentID int NOT NULL,
	PhotographeID int NOT NULL,
	DateSeance Datetime,
	Adresse varchar(200)
)


CREATE TABLE Seances.Telephone
(
	TelephoneID int NOT NULL IDENTITY,
	SeanceID int NOT NULL,
	NumTel varchar(20)
)

CREATE TABLE Photographes.Photographe
(
	PhotographeID int NOT NULL IDENTITY,
	Nom varchar(50) NOT NULL,
	Prenom varchar(50) NOT NULL
)

CREATE TABLE Seances.Photo
(
	PhotoID int NOT NULL IDENTITY,
	SeanceID int NOT NULL,
	Photo varchar(200),
	PhotoName varchar(50)
)
GO

/* PRIMARY KEYS: */


ALTER TABLE Agents.Agent
	ADD CONSTRAINT PK_Agent_AgentID
	PRIMARY KEY (AgentID);

ALTER TABLE Seances.Seance
	ADD CONSTRAINT PK_Seance_SeanceID
	PRIMARY KEY (SeanceID);

ALTER TABLE Photographes.Photographe
	ADD CONSTRAINT PK_Photographe_PhotographeID
	PRIMARY KEY (PhotographeID);

ALTER TABLE Seances.Photo
	ADD CONSTRAINT PK_Photo_PhotoID
	PRIMARY KEY (PhotoID);

ALTER TABLE Seances.Telephone
	ADD CONSTRAINT PK_Telephone_TelephoneID
	PRIMARY KEY (TelephoneID);

	GO
	/* FOREIGN KEYS: */


ALTER TABLE Seances.Seance
	ADD CONSTRAINT FK_Sceance_Agent_AgentID
	FOREIGN KEY (AgentID)
	REFERENCES Agents.Agent(AgentID);

ALTER TABLE Seances.Seance
	ADD CONSTRAINT FK_Sceance_Photographe_PhotographeID
	FOREIGN KEY (PhotographeID)
	REFERENCES Photographes.Photographe(PhotographeID);

ALTER TABLE Seances.Photo
	ADD CONSTRAINT FK_Photo_Seance_SeanceID
	FOREIGN KEY (SeanceID)
	REFERENCES Seances.Seance(SeanceID);

ALTER TABLE Seances.Telephone
	ADD CONSTRAINT FK_Telephone_Seance_SeanceID
	FOREIGN KEY (TelephoneID)
	REFERENCES Sceances.Seance(SeanceID);



	/* Contraintes Check: */

	GO

ALTER TABLE Seances.Seance
	ADD CONSTRAINT CK_DateSeance
	CHECK ([DateSeance] > CURRENT_TIMESTAMP);


DROP DATABASE [H15_PROJET_E07];