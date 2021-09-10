CREATE DATABASE inlock_games_tarde;


USE	inlock_games_tarde;


------------------DDL------------------

CREATE TABLE estudios
(
	idEstudio INT PRIMARY KEY IDENTITY,
	nomeEstudio VARCHAR(100) UNIQUE NOT NULL
);
GO

CREATE TABLE jogos
(
	idJogo INT PRIMARY KEY IDENTITY,
	nomeJogo VARCHAR(100) UNIQUE NOT NULL,
	descricao VARCHAR (250) NOT NULL,
	dataLancamento	DATE NOT NULL,
	valor VARCHAR(100) NOT NULL,
	idEstudio INT FOREIGN KEY REFERENCES estudios(idEstudio)
);
GO

CREATE TABLE tiposUsuarios
(
	idTipoUsuario INT PRIMARY KEY IDENTITY,
	titulo VARCHAR (100) NOT NULL
);
GO

CREATE TABLE usuarios
(
	idUsuario INT PRIMARY KEY IDENTITY,
	email VARCHAR(200) UNIQUE NOT NULL,
	senha VARCHAR(100) NOT NULL,
	idTipoUsuario INT FOREIGN KEY REFERENCES tiposUsuarios(idTipoUsuario)
);
GO