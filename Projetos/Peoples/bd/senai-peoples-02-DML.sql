INSERT INTO Funcionarios VALUES (
	'Catarina','Strada'
),
(
	'Tadeu','Vitelli'
)
;

update Funcionarios set DataNascimento = '02/10/1960' where IdFuncionario = 4;
update Funcionarios set DataNascimento = '02/10/1960' where IdFuncionario = 5;
update Funcionarios set DataNascimento = '02/10/1960' where IdFuncionario = 6;
update Funcionarios set DataNascimento = '02/10/1960' where IdFuncionario = 7;
update Funcionarios set DataNascimento = '02/10/1960' where IdFuncionario = 9

CREATE PROCEDURE BuscarPorNome @Nome VARCHAR(255)
AS
SELECT * FROM Funcionarios WHERE Nome = @Nome  

EXEC BuscarPorNome 'Carlos'

CREATE PROCEDURE OrdernarNomesASC
AS
SELECT * FROM Funcionarios ORDER BY Nome ASC

EXEC OrdernarNomesASC