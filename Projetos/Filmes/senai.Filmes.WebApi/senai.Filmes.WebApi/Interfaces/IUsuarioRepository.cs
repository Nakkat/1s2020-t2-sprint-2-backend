using Senai.Filmes.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Filmes.WebApi.Interfaces
{
    interface IUsuarioRepository
    {
        UsuarioDomain BuscarPorEmailSenha(string email, string senha);
    }
}
