using senai.Filmes.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Filmes.WebApi.Interfaces
{
    interface IFilmeRepository
    {
        List<FilmeDomain> Get();

        FilmeDomain GetById(int id);

        void Register(FilmeDomain Filme);

        void Alterar(int id, FilmeDomain Filme);

        void Apagar(int id);
    }
}
