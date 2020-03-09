SELECT IdFuncionario as Funcionario, Nome, Sobrenome, DataNascimento, Usuario.IdUsuario as USuario, 
Email, Senha, TipoUsuario.IdTipoUsuario as TipoUsuario, Titulo FROM Funcionarios 
INNER JOIN Usuario on Usuario.IdUsuario = Funcionarios.IdUsuario 
INNER JOIN TipoUsuario on TipoUsuario.IdTipoUsuario = Funcionarios.IdUsuario	
WHERE IdFuncionario = 1
;
SELECT IdUsuario as Usuario, Email, Senha, TipoUsuario.IdTipoUsuario as TipoUsuario, Titulo FROM Usuario
INNER JOIN TipoUsuario on Usuario.IdTipoUsuario = TipoUsuario.IdTipoUsuario
;

SELECT IdTipoUsuario as TipoUsuario, Titulo FROM TipoUsuario;

SELECT IdFuncionario as Funcionario, Nome, Sobrenome, DataNascimento, Usuario.IdUsuario as USuario, 
Email, Senha, TipoUsuario.IdTipoUsuario as TipoUsuario, Titulo FROM Funcionarios 
INNER JOIN Usuario on Usuario.IdUsuario = Funcionarios.IdUsuario 
INNER JOIN TipoUsuario on TipoUsuario.IdTipoUsuario = Funcionarios.IdUsuario
WHERE Nome LIKE '%Ca%';


SELECT IdUsuario, Email, Senha, TipoUsuario.IdTipoUSuario as TipoUsuario, Titulo FROM Usuario

INNER JOIN TipoUsuario on Usuario.IdTipoUsuario = TipoUsuario.IdTipoUsuario
WHERE Email = 'cat@adm.com' and Senha = '123'