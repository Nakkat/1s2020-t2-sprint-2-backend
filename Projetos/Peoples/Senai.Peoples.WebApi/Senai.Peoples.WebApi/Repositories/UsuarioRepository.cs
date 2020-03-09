using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string stringConexao = "Data Source=DEV301\\SQLEXPRESS; initial catalog=Manha_Peoples; user Id=sa; pwd=sa@132";

        public List<UsuarioDomain> ListarUsuario()
        {
            List<UsuarioDomain> usuarios = new List<UsuarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = " SELECT IdUsuario as Usuario, Email, Senha, tipoUsuario.IdTipoUsuario as TipoUsuario, Titulo FROM Usuario " +
                                        " INNER JOIN TipoUsuario on Usuario.IdTipoUsuario = TipoUsuario.IdTipoUsuario ";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr[0]),
                            Email = rdr[1].ToString(),
                            Senha = rdr[2].ToString(),
                            IdTipoUsuario = Convert.ToInt32(rdr[3]),
                            TipoUsuario = new TipoUsuarioDomain
                            {
                                IdTipoUsuario = Convert.ToInt32(rdr[3]),
                                Titulo = rdr[4].ToString()
                            }
                        };

                        usuarios.Add(usuario);
                    }
                }
            }
            return usuarios;
        }

        public UsuarioDomain BuscarPorIdUsuario(int id)
        {

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = " SELECT IdUsuario as Usuario, Email, Senha, TipoUsuario.IdTipoUsuario as TipoUSuario, Titulo FROM Usuario " +
                                         " INNER JOIN TipoUsuario on Usuario.IdTipoUsuario = TipoUsuario.IdTipoUsuario " +
                                         " WHERE IdUsuario = @ID ";

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
                        UsuarioDomain usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr[0]),
                            Email = rdr[1].ToString(),
                            Senha = rdr[2].ToString(),
                            IdTipoUsuario = Convert.ToInt32(rdr[3]),
                            TipoUsuario = new TipoUsuarioDomain
                            {
                                IdTipoUsuario = Convert.ToInt32(rdr[3]),
                                Titulo = rdr[4].ToString()
                            }
                        };

                        // Retorna o genero com os dados obtidos
                        return usuario;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }

        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = "SELECT IdUsuario, Email, Senha, TipoUsuario.IdTipoUSuario, Titulo FROM Usuario " +
                                     "INNER JOIN TipoUsuario on Usuario.IdTipoUsuario = TipoUsuario.IdTipoUsuario " +
                                     "WHERE Email = @Email AND Senha = @Senha";
                                     

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        UsuarioDomain usuario = new UsuarioDomain();

                        while (rdr.Read())
                        {
                            usuario.IdUsuario = Convert.ToInt32(rdr[0]);

                            usuario.Email = rdr[1].ToString();

                            usuario.Senha = rdr[2].ToString();

                            usuario.IdTipoUsuario = Convert.ToInt32(rdr[3]);

                            usuario.TipoUsuario  = new TipoUsuarioDomain
                            {
                                IdTipoUsuario = Convert.ToInt32(rdr[3]),
                                Titulo = rdr[4].ToString(),
                            };
                        }

                        return usuario;
                    }
                }

                return null;
            }
        }

        public void CadastrarUsuario(UsuarioDomain usuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Usuario(Email, Senha, TipoUsuario.IdTipoUsuario) VALUES (@E,@S,@T)";

                SqlCommand cmd = new SqlCommand(queryInsert, con);

                cmd.Parameters.AddWithValue("@E", usuario.Email);
                cmd.Parameters.AddWithValue("@S", usuario.Senha);
                cmd.Parameters.AddWithValue("@T", usuario.IdTipoUsuario);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void AlterarUsuario(int id, UsuarioDomain usuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE Usuario SET Email = @Email, Senha = @Senha WHERE IdUsuario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                    cmd.Parameters.AddWithValue("@IdTU", usuario.IdTipoUsuario);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletarUsuario(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string queryDelete = "DELETE FROM Usuario WHERE IdUsuario = @ID";

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
