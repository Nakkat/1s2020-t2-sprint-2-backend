using Senai.InLock.WebApi.DataBaseFirst.Domains;
using Senai.InLock.WebApi.DataBaseFirst.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.DataBaseFirst.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        InLockContext ctx = new InLockContext();

        /// <summary>
        /// Altera um usuário por Id
        /// </summary>
        /// <param name="id">Id que será buscado</param>
        /// <param name="usuarioAlterado">Objeto usuarioAlterado que será atualizado</param>
        public void AlterarUsuario(int id, Usuarios usuarioAlterado)
        {
            Usuarios usuario = ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (String.IsNullOrEmpty(usuarioAlterado.Email) == false)
            {
                usuario.Email = usuarioAlterado.Email;
            }

            if (String.IsNullOrEmpty(usuarioAlterado.Senha) == false)
            {
                usuario.Senha = usuarioAlterado.Senha;
            }

            if (usuarioAlterado.IdTipoUsuario != null)
            {
                usuario.IdTipoUsuario = usuarioAlterado.IdTipoUsuario;
            }
            ctx.SaveChanges();
        }

        /// <summary>
        /// Buscar usuários por Id
        /// </summary>
        /// <param name="id">Id que será buscado</param>
        /// <returns></returns>
        public Usuarios BuscarPorIdUsuario(int id)
        {
            // Retorna o primeiro tipo de usuário encontrado para o ID informado
            return ctx.Usuarios.FirstOrDefault(e => e.IdUsuario == id);
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="novoUsuario">Objeto novoUsuario que será cadastrado</param>
        public void CadastrarUsuario(Usuarios novoUsuario)
        {
            // Adiciona este novoEstudio
            ctx.Usuarios.Add(novoUsuario);
            // Salva as informações para serem gravadas no banco de dados
            ctx.SaveChanges(); 
        }

        /// <summary>
        /// Deleta um usuário por Id
        /// </summary>
        /// <param name="id">Id que será buscado</param>
        public void DeletarUsuario(int id)
        {
            // Busque o Id do usuário e remova-o e então, salve as alterações
            ctx.Usuarios.Remove(BuscarPorIdUsuario(id));
            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista de usuários
        /// </summary>
        /// <returns>Retorna uma lista de usuários</returns>
        public List<Usuarios> ListarUsuario()
        {
            return ctx.Usuarios.ToList();
        }
    }
}
