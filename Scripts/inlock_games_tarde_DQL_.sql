USE inlock_games_tarde;


-------DQL-------

SELECT * FROM tiposUsuarios;
GO 

SELECT * FROM usuarios;
GO 

SELECT * FROM estudios;
GO

SELECT * FROM jogos;
GO

SELECT nomeJogo, nomeEstudio FROM jogos
INNER JOIN estudios
ON jogos.idEstudio = estudios.idEstudio;
GO

SELECT nomeEstudio, nomeJogo FROM estudios
LEFT JOIN jogos
ON estudios.idEstudio = jogos.idEstudio;
GO

SELECT email,senha, idUsuario,usuarios.idTipoUsuario , titulo FROM usuarios 
INNER JOIN tiposUsuarios ON usuarios.idTipoUsuario = tiposUsuarios.idTipoUsuario
WHERE email = 'admin@admin.com' AND senha = 'admin';
GO

SELECT idJogo,nomeJogo, descricao, valor, dataLancamento FROM jogos
WHERE idJogo = 1;
GO

SELECT idEstudio, nomeEstudio FROM estudios
WHERE idEstudio = 2;
GO
