SELECT IdFuncionario as Funcionario, Nome, Sobrenome, DataNascimento, Email, Senha, Titulo FROM Funcionarios 
INNER JOIN Usuario on Usuario.IdUsuario = Funcionarios.IdUsuario 
INNER JOIN TipoUsuario on TipoUsuario.IdTipoUsuario = Funcionarios.IdUsuario	
WHERE IdFuncionario = 1
;
SELECT IdUsuario as Usuario, Email, Senha, Titulo FROM Usuario
INNER JOIN TipoUsuario on Usuario.IdTipoUsuario = TipoUsuario.IdTipoUsuario
;

SELECT IdTipoUsuario as TipoUsuario, Titulo FROM TipoUsuario;
SELECT * FROM Funcionarios WHERE Nome LIKE '%{nome}%';