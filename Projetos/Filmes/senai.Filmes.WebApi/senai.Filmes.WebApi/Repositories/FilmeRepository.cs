using senai.Filmes.WebApi.Domains;
using senai.Filmes.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Filmes.WebApi.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private string stringConexao = "Data Source=DEV301\\SQLEXPRESS; initial catalog=Filmes_Manha; user Id=sa; pwd=sa@132";

        public List<FilmeDomain> Get()
        {
         
            List<FilmeDomain> filmes = new List<FilmeDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idfilme AS Filme, titulo, filmes.idgenero as Numero, nome AS Genero FROM filmes INNER JOIN genero ON genero.IdGenero = filmes.IdGenero";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain
                        {
                            IdFilme = Convert.ToInt32(rdr[0]),
                            Titulo = rdr[1].ToString(),
                            IdGenero = Convert.ToInt32(rdr[2]),
                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(rdr[2]),
                                Nome = rdr[3].ToString()
                            }
                        };

                        filmes.Add(filme);
                    }
                }
            }
            return filmes;
        }

        public FilmeDomain GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idfilme AS Filme, titulo AS Nome,genero.idgenero as Genero, nome AS Titulo FROM filmes INNER JOIN genero ON genero.IdGenero = filmes.IdGenero WHERE idfilme = @ID";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FilmeDomain filmes = new FilmeDomain
                        {
                            IdFilme = Convert.ToInt32(rdr[0]),
                            Titulo = rdr["Titulo"].ToString(),
                            IdGenero = Convert.ToInt32(rdr[2]),
                            Genero = new GeneroDomain
                            {
                                IdGenero = Convert.ToInt32(rdr[2]),
                                Nome = rdr["Nome"].ToString()
                            }
                        };
                        return filmes;
                    }
                    return null;
                }
            }
        }

        public void Register(FilmeDomain filmes)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Filmes(Titulo,IdGenero) VALUES (@T,@Id)";

                SqlCommand cmd = new SqlCommand(queryInsert, con);

                cmd.Parameters.AddWithValue("@T", filmes.Titulo);
                cmd.Parameters.AddWithValue("@Id", filmes.IdGenero);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Alterar(int id, FilmeDomain filmes)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Filmes SET Titulo = @Titulo, IdGenero = @ID2 WHERE IdFilme= @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", filmes.Titulo);
                    cmd.Parameters.AddWithValue("@ID2", filmes.IdGenero);
                    cmd.Parameters.AddWithValue("@ID", id);
                  
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Apagar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Filmes WHERE IdFilme = @ID";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

