using Senai.InLock.WebApi.DataBaseFirst.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.DataBaseFirst.Interfaces
{
    interface IUsuarioRepository
    {
        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>Uma lista de usuários</returns>
        List<Usuarios> ListarUsuario();

        /// <summary>
        /// Busca um usuário através do ID
        /// </summary>
        /// <param name="id">ID do usuário que será buscado</param>
        /// <returns>Um usuário buscado</returns>
        Usuarios BuscarPorIdUsuario(int id);

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="novoUsuario">Objeto novoUsuario que será cadastrado</param>
        void CadastrarUsuario(Usuarios novoUsuario);

        /// <summary>
        /// Altera um usuário
        /// </summary>
        /// <param name="usuarioAlterado">Um usuário alterado</param>
        void AlterarUsuario(int id, Usuarios usuarioAlterado);

        /// <summary>
        /// Deleta um usuário
        /// </summary>
        /// <param name="usuarioDeletado">Um usuário deletado</param>
        void DeletarUsuario(int id);
    }
}