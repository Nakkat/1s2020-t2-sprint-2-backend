using Senai.InLock.WebApi.CodeFirst.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.CodeFirst.Interfaces
{
    interface ITipoUsuarioRepository
    {
        /// <summary>
        /// Lista todos os tipos de usuário
        /// </summary>
        /// <returns>Uma lista de tipos de usuário</returns>
        List<TiposUsuario> ListarTipoUsuario();

        /// <summary>
        /// Busca um tipo de usuário através do ID
        /// </summary>
        /// <param name="id">ID do tipo de usuário que será buscado</param>
        /// <returns>Um estúdio buscado</returns>
        TiposUsuario BuscarPorIdTipoUsuario(int id);

        /// <summary>
        /// Cadastra um novo tipo de usuário
        /// </summary>
        /// <param name="novoTipoUsuario">Objeto novoTipoUsuario que será cadastrado</param>
        void CadastrarTipoUsuario(TiposUsuario novoTipoUsuario);

        /// <summary>
        /// Altera um tipo de usuário
        /// </summary>
        /// <param name="tipoUsuarioAlterado">Um tipo de usuário alterado</param>
        void AlterarTipoUsuario(int id, TiposUsuario tipoUsuarioAlterado);

        /// <summary>
        /// Deleta um tipo de usuário
        /// </summary>
        /// <returns>Um tipo de usuário deletado</returns>
        void DeletarTipoUsuario(int id);
    }
}