using filmes.Domain;
using filmes.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace filmes.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {

        //Conexão que fornece parametro pro acesso ao banco de dados
        private string StringConexao = "Data Source= DEV301\\SQLEXPRESS; initial catalog=Filmes_Manha; user Id=sa; pwd=sa@132";

        List<GeneroDomain> Genero = new List<GeneroDomain>();
        //listar os generos
        public List<GeneroDomain> Listar()
        {

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "SELECT IdGenero, Nome from Genero";

                //Abrir a conexão
                con.Open();

                //Lê o os dados banco de dados
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    //comandos para leitura
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain
                        {
                            IdGenero = Convert.ToInt32(rdr[0]),
                            Nome = rdr["Nome"].ToString()
                        };

                        Genero.Add(genero);
                    }
                }
            }
            return Genero;
        }

        public GeneroDomain GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryBuscar = "SELECT IdGenero, Nome FROM Genero";

                using (SqlCommand cmd = new SqlCommand(queryBuscar, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain
                        {
                            IdGenero = Convert.ToInt32(rdr["IdGenero"]),
                            Nome = rdr["Nome"].ToString()
                        };

                        return genero;
                    }
                    return null;
                }
            }
        }
    
        public void Cadastrar(GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryInsert = "INSERT INTO Genero (Nome) VALUES(@Nome)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Atualiza um gênero existente
        /// </summary>
        /// <param name="genero">Objeto gênero que será atualizado</param>
        /// 
        public void AtualizarIdCorpo(GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "UPDATE Genero SET Nome = @Nome WHERE IdGenero= @ID";
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                    cmd.Parameters.AddWithValue("@ID", genero.IdGenero);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Atualiza um gênero existente
        /// </summary>
        /// <param name="genero">Objeto que será atualizado</param>
        public void AtualizarIdUrl(int id, GeneroDomain genero)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryAtualizar = "UPDATE Genero SET Nome = @Nome WHERE IdGenero= @ID";
                using (SqlCommand cmd = new SqlCommand(queryAtualizar, con)) {
                    cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                    cmd.Parameters.AddWithValue("@ID", genero.IdGenero);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "DELETE FROM Genero WHERE IdGenero= @ID";

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
 }

