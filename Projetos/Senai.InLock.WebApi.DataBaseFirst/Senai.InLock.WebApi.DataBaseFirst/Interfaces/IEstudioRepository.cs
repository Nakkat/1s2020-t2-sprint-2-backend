﻿using Senai.InLock.WebApi.DataBaseFirst.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.DataBaseFirst.Interfaces
{
    /// <summary>
    /// Interface responsável pelo EstudioRepository
    /// </summary>
    interface IEstudioRepository
    {
        /// <summary>
        /// Lista todos os estúdios
        /// </summary>
        /// <returns>Uma lista de estúdios</returns>
        List<Estudios> Listar();

        /// <summary>
        /// Busca um estúdio através do ID
        /// </summary>
        /// <param name="id">ID do estúdio que será buscado</param>
        /// <returns>Um estúdio buscado</returns>
        Estudios BuscarPorId(int id);

        /// <summary>
        /// Cadastra um novo estúdio
        /// </summary>
        /// <param name="novoEstudio">Objeto novoEstudio que será cadastrado</param>
        void Cadastrar(Estudios novoEstudio);

        /// <summary>
        /// Altera um estúdio
        /// </summary>
        /// <param name="estudioAlterado">Um estúdio alterado</param>
        void Alterar(int id, Estudios estudioAlterado);

        /// <summary>
        /// Deleta um estúdio
        /// </summary>
        /// <returns>Um estúdio deletado</returns>
        void Deletar(int id);

        /// <summary>
        /// Lista todos os estúdios com a lista de jogos
        /// </summary>
        /// <returns>Lista de estúdios com os seus jogos</returns>
        List<Estudios> ListarJogos();
    }
}