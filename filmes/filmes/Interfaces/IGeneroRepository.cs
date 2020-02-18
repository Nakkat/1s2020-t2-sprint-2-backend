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

        void Cadastrar (GeneroDomain generoDomain);

        void Alterar(GeneroDomain generoDomain);

        void Deletar(GeneroDomain generoDomain);

    }


    
}
