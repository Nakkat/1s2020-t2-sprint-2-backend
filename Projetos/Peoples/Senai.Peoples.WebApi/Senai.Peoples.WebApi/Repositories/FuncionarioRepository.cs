using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private string stringConexao = "Data Source=DEV301\\SQLEXPRESS; initial catalog=Manha_Peoples; user Id=sa; pwd=sa@132";

        public void Alterar(int id, FuncionarioDomain funcionarios)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE IdFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", funcionarios.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", funcionarios.Sobrenome);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FuncionarioDomain BuscarPorId(int id)
        { 
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT IdFuncionario as Funcionario, Nome,Sobrenome FROM Funcionarios WHERE IdFuncionario = @ID";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Executa a query
                    rdr = cmd.ExecuteReader();

                    // Caso a o resultado da query possua registro
                    if (rdr.Read())
                    {
                        // Cria um objeto genero
                        FuncionarioDomain funcionarios = new FuncionarioDomain
                        {
                            // Atribui à propriedade IdGenero o valor da coluna "IdGenero" da tabela do banco
                            IdFuncionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr[1].ToString(),
                            Sobrenome = rdr[2].ToString()
                        };

                        // Retorna o genero com os dados obtidos
                        return funcionarios;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }

        public FuncionarioDomain BuscarPorNome(string nome)
        {
            FuncionarioDomain funcionarios = new FuncionarioDomain();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string query = "EXEC BuscarPorNome @Nome";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para percorrer a tabela do banco
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    // Executa a query
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para ler, o laço se repete
                    while (rdr.Read())
                    {

                        funcionarios.IdFuncionario = Convert.ToInt32(rdr["IdFuncionario"]);
                        funcionarios.Nome = rdr["Nome"].ToString() + " " + rdr["Sobrenome"].ToString();
                        funcionarios.DataNascimento = rdr["DataNascimento"].ToString();

                    }
                }

                return funcionarios;
            }
        }

        public List<FuncionarioDomain> OrdenarAsc()
        {
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string query = "EXEC OrdernarNomesASC";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para percorrer a tabela do banco
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Executa a query
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para ler, o laço se repete
                    while (rdr.Read())
                    {
                        // Cria um objeto genero do tipo GeneroDomain
                        FuncionarioDomain funcionario = new FuncionarioDomain();

                        funcionario.IdFuncionario = Convert.ToInt32(rdr["IdFuncionario"]);
                        funcionario.Nome = rdr["Nome"].ToString();
                        funcionario.Sobrenome = rdr["Sobrenome"].ToString();
                        funcionario.DataNascimento = rdr["DataNascimento"].ToString();

                        funcionarios.Add(funcionario);
                    }
                }
            }
            return funcionarios;
        }

            public void Cadastrar(FuncionarioDomain funcionarios)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Funcionarios(Nome,Sobrenome) VALUES (@F,@S)";

                SqlCommand cmd = new SqlCommand(queryInsert, con);

                cmd.Parameters.AddWithValue("@F", funcionarios.Nome);
                cmd.Parameters.AddWithValue("@S", funcionarios.Sobrenome);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
               
                string queryDelete = "DELETE FROM Funcionarios WHERE IdFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                  
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<FuncionarioDomain> Listar()
        {
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdFuncionario as Funcionario, Nome, Sobrenome FROM Funcionarios";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr[1].ToString(),
                            Sobrenome = rdr[2].ToString()
                        };

                        funcionarios.Add(funcionario);
                    }
                }
            }
            return funcionarios;
        }

        public FuncionarioDomain BuscarNomeCompletoPorOrdem(FuncionarioDomain Funcionario)
        {
            throw new NotImplementedException();
        }
    }
}
