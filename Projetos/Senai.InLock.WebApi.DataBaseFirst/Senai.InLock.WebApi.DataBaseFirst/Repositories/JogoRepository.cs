using Microsoft.EntityFrameworkCore;
using Senai.InLock.WebApi.DataBaseFirst.Domains;
using Senai.InLock.WebApi.DataBaseFirst.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.DataBaseFirst.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        InLockContext ctx = new InLockContext();

        /// <summary>
        /// Atualiza um jogo pelo Id
        /// </summary>
        /// <param name="id">Id que será buscado</param>
        /// <param name="jogoAlterado">Objeto jogoAlterado que será atualizado</param>
        public void AlterarJogo(int id, Jogos jogoAlterado)
        {
            Jogos jogo = ctx.Jogos.FirstOrDefault(e => e.IdJogo == id);

            if (String.IsNullOrEmpty(jogoAlterado.NomeJogo) == false)
            {
                jogo.NomeJogo = jogoAlterado.NomeJogo;
            }

            if (String.IsNullOrEmpty(jogoAlterado.Descricao) == false)
            {
                jogo.Descricao = jogoAlterado.Descricao;
            }

            if (jogoAlterado.DataLancamento != null)
            {
                jogo.DataLancamento = jogoAlterado.DataLancamento;
            }

            if (String.IsNullOrEmpty(jogoAlterado.Valor) == false)
            {
                jogo.Valor = jogoAlterado.Valor;
            }

            if (jogoAlterado.IdEstudio != null)
            {
                jogo.IdEstudio = jogoAlterado.IdEstudio;
            }

            ctx.SaveChanges(); 
        }

        /// <summary>
        /// Busca um jogo por ID
        /// </summary>
        /// <param name="id">Id do jogo que será buscado</param>
        /// <returns></returns>
        public Jogos BuscarPorIdJogo(int id)
        {
            // Retorna o primeiro tipo de usuário encontrado para o ID informado
            return ctx.Jogos.FirstOrDefault(e => e.IdJogo == id);
        }

        /// <summary>
        /// Cadastra um jogo novo
        /// </summary>
        /// <param name="novoJogo">Objeto novoJogo que será cadastrado</param>
        public void CadastrarJogo(Jogos novoJogo)
        {
            // Adiciona este novoEstudio
            ctx.Jogos.Add(novoJogo);
            // Salva as informações para serem gravadas no banco de dados
            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta um jogo
        /// </summary>
        /// <param name="id">Id que será buscado</param>
        public void DeletarJogo(int id)
        {
            // Busque o Id do jogo e remova-o, e então salve as alterações
            ctx.Jogos.Remove(BuscarPorIdJogo(id));
            ctx.SaveChanges();
        }

        /// <summary>
        /// Uma lista de Jogos
        /// </summary>
        /// <returns>Retorna uma lista de jogos</returns>
        public List<Jogos> ListarJogo()
        {
            return ctx.Jogos.ToList();
        }

        /// <summary>
        /// Lista todos os jogos com os estúdios
        /// </summary>
        /// <returns>Uma lista de jogos com os estúdios</returns>
        public List<Jogos> ListarComEstudios()
        {
            // Retorna uma lista com todas as informações dos jogos e estúdios
            return ctx.Jogos.Include(e => e.IdEstudioNavigation).ToList();
            // return ctx.Jogos.Include("IdEstudioNavigation").ToList();
        }

        /// <summary>
        /// Lista todos os jogos de um determinado estúdio
        /// </summary>
        /// <param name="id">ID do estúdio do qual os jogos serão listados</param>
        /// <returns>Uma lista de jogos de um determinado estúdio</returns>
        public List<Jogos> ListarUmEstudio(int id)
        {
            return ctx.Jogos.ToList().FindAll(j => j.IdEstudio == id);
        }
    }
}
