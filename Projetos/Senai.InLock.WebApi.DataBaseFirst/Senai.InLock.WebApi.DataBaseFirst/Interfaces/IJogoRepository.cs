using Senai.InLock.WebApi.DataBaseFirst.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.DataBaseFirst.Interfaces
{
    interface IJogoRepository
    {
        /// <summary>
        /// Lista todos os jogos
        /// </summary>
        /// <returns>Uma lista de jogos</returns>
        List<Jogos> ListarJogo();

        /// <summary>
        /// Busca um jogo através do ID
        /// </summary>
        /// <param name="id">ID do jogo que será buscado</param>
        /// <returns>Um jogo buscado</returns>
        Jogos BuscarPorIdJogo(int id);

        /// <summary>
        /// Cadastra um novo jogo
        /// </summary>
        /// <param name="novoJogo">Objeto novoJogo que será cadastrado</param>
        void CadastrarJogo(Jogos novoJogo);

        /// <summary>
        /// Altera um jogo
        /// </summary>
        /// <param name="jogoAlterado">Um jogo alterado</param>
        void AlterarJogo(int id, Jogos jogoAlterado);

        /// <summary>
        /// Deleta um jogo
        /// </summary>
        /// <returns>Um jogo deletado</returns>
        void DeletarJogo(int id);

        /// <summary>
        /// Lista todos os jogos com as informações dos estúdios
        /// </summary>
        /// <returns>Uma lista de jogos com os estúdios</returns>
        List<Jogos> ListarComEstudios();

        /// <summary>
        /// Lista todos os jogos de um determinado estúdio
        /// </summary>
        /// <param name="id">ID do estúdio do qual os jogos serão listados</param>
        /// <returns>Uma lista de jogos de um determinado estúdio</returns>
        List<Jogos> ListarUmEstudio(int id);
    }
}
