CREATE DATABASE Manha_Peoples;
USE Manha_Peoples;


CREATE TABLE TipoUsuario (
	IdTipoUsuario INT PRIMARY KEY IDENTITY,
	Titulo VARCHAR (255),
);

CREATE TABLE Usuario (
	IdUsuario INT PRIMARY KEY IDENTITY,
	Email VARCHAR (255) NOT NULL,
	Senha VARCHAR (255) NOT NULL,
	IdTipoUsuario INT FOREIGN KEY REFERENCES TipoUsuario(IdTipoUsuario)
);

CREATE TABLE Funcionarios (
	IdFuncionario INT PRIMARY KEY IDENTITY,
	Nome VARCHAR (255),
	Sobrenome VARCHAR (255),
	IdUsuario INT FOREIGN KEY REFERENCES Usuario(IdUsuario),
	DataNascimento DATE 
);

CREATE PROCEDURE OrdernarNomesASC
AS
SELECT * FROM Funcionarios ORDER BY Nome ASC

EXEC OrdernarNomesASC



