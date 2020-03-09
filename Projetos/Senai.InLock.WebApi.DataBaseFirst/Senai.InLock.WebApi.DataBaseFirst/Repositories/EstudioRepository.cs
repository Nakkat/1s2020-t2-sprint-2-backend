using Microsoft.EntityFrameworkCore;
using Senai.InLock.WebApi.DataBaseFirst.Domains;
using Senai.InLock.WebApi.DataBaseFirst.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.DataBaseFirst.Repositories
{
    /// <summary>
    /// Repositório dos Estúdios
    /// </summary>
    public class EstudioRepository : IEstudioRepository
    {
        /// <summary>
        /// Objeto contexto por onde serão chamados os métodos do EF Core
        /// </summary>
        InLockContext ctx = new InLockContext();

        /// <summary>
        /// Altera um estúdio através do Id
        /// </summary>
        /// <param name="id">Id do estúdio que será buscado</param>
        /// <param name="estudioAlterado">Objeto estudioAlterado que será alterado</param>
        public void Alterar(int id, Estudios estudioAlterado)
        {
            // Cria-se objeto para Estudios para buscar por Id
            Estudios estudio = ctx.Estudios.FirstOrDefault(e => e.IdEstudio == id);

            // Se a propriedade NomeEstudio não for nula, altera-se o mesmo e salve as alterações
            if (String.IsNullOrEmpty(estudioAlterado.NomeEstudio) == false)
            {
                estudio.NomeEstudio = estudioAlterado.NomeEstudio;
            }
            ctx.SaveChanges();
        }

        /// <summary>
        /// Busca um estúdio através do ID
        /// </summary>
        /// <param name="id">ID do estúdio que será buscado</param>
        /// <returns>Um estúdio buscado</returns>
        public Estudios BuscarPorId(int id)
        {
            // Retorna o primeiro estúdio encontrado para o ID informado
            return ctx.Estudios.FirstOrDefault(e => e.IdEstudio == id);
        }

        /// <summary>
        /// Cadastra um novo estúdio
        /// </summary>
        /// <param name="novoEstudio">Objeto novoEstudio que será cadastrado</param>
        public void Cadastrar(Estudios novoEstudio)
        {
            // Adiciona este novoEstudio
            ctx.Estudios.Add(novoEstudio);
            // Salva as informações para serem gravadas no banco de dados
            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta um estúdio
        /// </summary>
        /// <param name="id">Id que será buscado</param>
        public void Deletar(int id)
        {
            // Busque o Id e Remova-o, e então salve as alterações
            ctx.Estudios.Remove(BuscarPorId(id));
            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista todos os estúdios
        /// </summary>
        /// <returns>Uma lista de estúdios</returns>
        public List<Estudios> Listar()
        {
            // Retorna uma lista com todas as informações dos estúdios
            return ctx.Estudios.ToList();
        }

        /// <summary>
        /// Lista todos os estúdios com os respectivos jogos
        /// </summary>
        /// <returns>Uma lista de estúdios com os jogos</returns>
        public List<Estudios> ListarJogos()
        {
            // Retorna uma lista de estúdios com seus jogos
            return ctx.Estudios.Include(e => e.Jogos).ToList();
        }
    }
}