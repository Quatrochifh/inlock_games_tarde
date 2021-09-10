USE inlock_games_tarde;


-------DML-------
INSERT INTO tiposUsuarios(titulo)
VALUES	('Administrador'),('Cliente');
GO

INSERT INTO usuarios (email,senha,idTipoUsuario)
VALUES	('admin@admin.com', 'admin', 1),('cliente@cliente.com', 'cliente', 2);
GO

INSERT INTO estudios (nomeEstudio)
VALUES	('Blizzard'),('Rockstar Studios'),('Square Enix');
GO

INSERT INTO jogos (nomeJogo,descricao,valor,dataLancamento,idEstudio)
VALUES	('Diablo 3', 'é um jogo que contém bastante ação e é viciante, seja você um novato ou um fã',99.00, '15/05/2012', 1 ),
('Red Dead Redemption II', 'jogo eletrônico de ação-aventura western',120.00, '26/10/2018', 2);
GO