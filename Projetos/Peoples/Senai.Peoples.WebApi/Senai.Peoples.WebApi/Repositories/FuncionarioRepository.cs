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
                string queryUpdate = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome, DataNascimento = @DataNascimento, IdUsuario = @IdUsuario WHERE IdFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", funcionarios.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", funcionarios.Sobrenome);
                    cmd.Parameters.AddWithValue("@DataNascimento", funcionarios.DataNascimento);
                    cmd.Parameters.AddWithValue("@IdUsuario", funcionarios.IdUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FuncionarioDomain BuscarPorId(int id)
        {
           
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT IdFuncionario as Funcionario, Nome, Sobrenome, DataNascimento, Email, Senha, Titulo FROM Funcionarios " +
                                         "  INNER JOIN Usuario on Usuario.IdUsuario = Funcionarios.IdUsuario " +
                                         "  INNER JOIN TipoUsuario on TipoUsuario.IdTipoUsuario = Funcionarios.IdUsuario " +
                                         "  WHERE IdFuncionario = ID ";

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
                            Sobrenome = rdr[2].ToString(),
                            DataNascimento = rdr[3].ToString(),
                            IdUsuario = Convert.ToInt32(rdr[4]),
                            Usuario = new UsuarioDomain
                            {
                                IdUsuario = Convert.ToInt32(rdr[4]),
                                Email = rdr[5].ToString(),
                                Senha = rdr[6].ToString(),
                                IdTipoUsuario = Convert.ToInt32(rdr[7]),
                                TipoUsuario = new TipoUsuarioDomain
                                {
                                    IdTipoUsuario = Convert.ToInt32(rdr[7]),
                                    Titulo = rdr[8].ToString()
                                }
                            },
                        };

                        // Retorna o genero com os dados obtidos
                        return funcionarios;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }

        public List<FuncionarioDomain> BuscarPorNome(string nome)
        {
            List<FuncionarioDomain> ListaFuncionario = new List<FuncionarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = $"SELECT * FROM Funcionarios WHERE Nome LIKE '%{nome}%'";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FuncionarioDomain funcionarios = new FuncionarioDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr["IdFuncionario"]),
                            Nome = rdr["Nome"].ToString() + " " + rdr["Sobrenome"].ToString(),
                            DataNascimento = rdr["DataNascimento"].ToString()
                        };
                        ListaFuncionario.Add(funcionarios);
                    }
                }

                return ListaFuncionario;
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
                string queryInsert = "INSERT INTO Funcionarios(Nome,Sobrenome,DataNascimento, IdUsuario) VALUES (@N,@S,@D,@IdUsurio)";

                SqlCommand cmd = new SqlCommand(queryInsert, con);

                cmd.Parameters.AddWithValue("@N", funcionarios.Nome);
                cmd.Parameters.AddWithValue("@S", funcionarios.Sobrenome);
                cmd.Parameters.AddWithValue("@D", funcionarios.DataNascimento);
                cmd.Parameters.AddWithValue("@IdUsuario", funcionarios.IdUsuario);

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
                string querySelectAll = " SELECT IdFuncionario as Funcionario, Nome, Sobrenome, DataNascimento, Email, Senha, Titulo FROM Funcionarios " +
                                        " INNER JOIN Usuario on Usuario.IdUsuario = Funcionarios.IdUsuario " +
                                        " INNER JOIN TipoUsuario on TipoUsuario.IdTipoUsuario = Funcionarios.IdUsuario ";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            // Atribui à propriedade IdGenero o valor da coluna "IdGenero" da tabela do banco
                            IdFuncionario = Convert.ToInt32(rdr[0]),
                            Nome = rdr[1].ToString(),
                            Sobrenome = rdr[2].ToString(),
                            DataNascimento = rdr[3].ToString(),
                            IdUsuario = Convert.ToInt32(rdr[4]),
                            Usuario = new UsuarioDomain
                            {
                                IdUsuario = Convert.ToInt32(rdr[4]),
                                Email = rdr[5].ToString(),
                                Senha = rdr[6].ToString(),
                                IdTipoUsuario = Convert.ToInt32(rdr[7]),
                                TipoUsuario = new TipoUsuarioDomain
                                {
                                    IdTipoUsuario = Convert.ToInt32(rdr[7]),
                                    Titulo = rdr[8].ToString()
                                }
                            },
                        };

                        funcionarios.Add(funcionario);
                    }
                }
            }
            return funcionarios;
        }
    }
}
