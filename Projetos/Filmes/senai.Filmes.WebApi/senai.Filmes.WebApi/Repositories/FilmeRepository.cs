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

        public List<FilmeDomain> BuscarPorTitulo(string busca)
        {
            // Cria uma lista filmes onde serão armazenados os dados
            List<FilmeDomain> filmes = new List<FilmeDomain>();

            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT IdFilme, Titulo, Filmes.IdGenero, Genero.Nome FROM Filmes" +
                                        " INNER JOIN Genero" +
                                        " ON Filmes.IdGenero = Genero.IdGenero" +
                                        $" WHERE Titulo LIKE '%{busca}%' ORDER BY Titulo DESC";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para receber os dados do banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {

                    // Executa a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para serem lidos no rdr, o laço se repete
                    while (rdr.Read())
                    {
                        // Instancia um objeto filme do tipo FilmeDomain
                        FilmeDomain filme = new FilmeDomain
                        {
                            // Atribui à propriedade IdFilme o valor da coluna IdFilme da tabela do banco de dados
                            IdFilme = Convert.ToInt32(rdr["IdFilme"])

                            // Atribui à propriedade Titulo o valor da coluna Titulo da tabela do banco de dados
                            ,
                            Titulo = rdr["Titulo"].ToString()

                            // Atribui à propriedade IdGenero o valor da coluna IdGenero da tabela do banco de dados
                            ,
                            IdGenero = Convert.ToInt32(rdr["IdGenero"])

                            ,
                            Genero = new GeneroDomain
                            {
                                // Atribui à propriedade IdGenero o valor da coluna IdGenero da tabela do banco de dados
                                IdGenero = Convert.ToInt32(rdr["IdGenero"])

                                // Atribui à propriedade Nome o valor da coluna Nome da tabela do banco de dados
                                ,
                                Nome = rdr["Nome"].ToString()
                            }
                        };

                        // Adiciona o filme criado à lista filmes
                        filmes.Add(filme);
                    }
                }
            }
            return filmes;
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

