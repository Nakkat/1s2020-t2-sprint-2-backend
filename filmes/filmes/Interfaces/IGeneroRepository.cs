using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using filmes.Domain;

namespace filmes.Interfaces
{
    interface IGeneroRepository
    {
        /// <summary>
        /// Lista todos os generos
        /// </summary>
        /// <returns>Retorna uma lista de generos</returns>
        List<GeneroDomain> Listar();

        // Retorno NomeMetodo(Parâmetro)
        /// <summary>
        /// Cadastra um novo gênero
        /// </summary>
        /// <param name="genero">Objeto genero que será cadastrado</param>
        void Cadastrar  (GeneroDomain genero);

        /// <summary>
        /// Atualizadq um gênero através do corpo
        /// </summary>
        /// <param name="genero">Nome do gênero que será atualizado</param>
        void AtualizarIdCorpo(GeneroDomain genero);

        /// <summary>
        /// Atualiza um gênero através do seu ID
        /// </summary>
        /// <param name="id">ID do gênero que será atualizado</param>
        /// <param name="genero">Nome do gênero que será atualizado</param>
        void AtualizarIdUrl(int id, GeneroDomain genero);

        /// <summary>
        /// Busca um gênero através do seu ID
        /// </summary>
        /// <param name="id">ID do gênero que será buscado</param>
        GeneroDomain GetById(int id);

        /// <summary>
        /// Deleta um gênero através do seu ID
        /// </summary>
        /// <param name="id">ID do gênero que será deletado</param>
        void Deletar(int id);

    }


    
}
