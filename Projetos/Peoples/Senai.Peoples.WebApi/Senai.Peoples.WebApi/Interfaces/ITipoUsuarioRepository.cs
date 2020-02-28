using Senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface ITipoUsuarioRepository
    {
        List<TipoUsuarioDomain> ListarTipoUsuario();

        TipoUsuarioDomain BuscarPorIdTipoUsuario(int id);

        void CadastrarTipoUsuario(TipoUsuarioDomain TipoUsuario);

        void AlterarTipoUsuario(int id, TipoUsuarioDomain TipoUsuario);

        void DeletarTipoUsuario(int id);
    }
}
