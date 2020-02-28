using Senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface IUsuarioRepository
    {
        List<UsuarioDomain> ListarUsuario();

        UsuarioDomain BuscarPorIdUsuario(int id);

        UsuarioDomain BuscarPorEmailSenha(string email, string senha);

        void CadastrarUsuario(UsuarioDomain Usuario);

        void AlterarUsuario(int id, UsuarioDomain Usuario);

        void DeletarUsuario(int id);
    }
}
