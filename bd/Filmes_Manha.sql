CREATE DATABASE Filmes_Manha;
USE Filmes_Manha;

CREATE TABLE Filmes (
	IdFilmes INT PRIMARY KEY IDENTITY,
	Titulo	 VARCHAR(255)
)

CREATE TABLE Genero (
	IdGenero INT PRIMARY KEY IDENTITY,
	Nome	 VARCHAR(255)
)

INSERT INTO Filmes VALUES (
	'TriploX',1
);
INSERT INTO Filmes VALUES (
	'Up Altas Aventuras',2
);

INSERT INTO Genero VALUES (
	'Ação'
);

INSERT INTO Genero VALUES (
	'Aventura'
);

SELECT * FROM  Genero;
SELECT * FROM  Filmes