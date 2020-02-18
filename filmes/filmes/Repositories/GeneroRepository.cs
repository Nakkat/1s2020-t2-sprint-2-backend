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
        private string StringConexao = "Data Source= DEV4\\SQLEXPRESS; initial catalog=Filmes; user Id=sa; pwd=sa@132";

        List<GeneroDomain> Genero = new List<GeneroDomain>();
        //listar os generos
        public List<GeneroDomain> Listar()
        {
            
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string query = "select IdGenero, Nome from Genero";

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

        public void Cadastrar(GeneroDomain genero)
        {
            string Query = "insert into Genero (Nome) values(@Nome)";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Alterar(GeneroDomain genero)
        {
            string Query = "update Genero set Nome = @Nome where IdGenero = @IdGenero";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                cmd.Parameters.AddWithValue("@IdGenero", genero.IdGenero);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Deletar(GeneroDomain genero)
        {
            string Query = "Delete From Genero Where IdGenero = @IdGenero";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome", genero.Nome);
                cmd.Parameters.AddWithValue("@IdGenero", genero.IdGenero);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
