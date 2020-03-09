using Senai.InLock.WebApi.DataBaseFirst.Domains;
using Senai.InLock.WebApi.DataBaseFirst.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.InLock.WebApi.DataBaseFirst.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        InLockContext ctx = new InLockContext();

        public void AlterarTipoUsuario(int id, TiposUsuario tipoUsuarioAlterado)
        {
            TiposUsuario tipoUsuario = ctx.TiposUsuario.FirstOrDefault(e => e.IdTipoUsuario == id);

            if (String.IsNullOrEmpty(tipoUsuarioAlterado.Titulo) == false)
            {
                tipoUsuario.Titulo = tipoUsuarioAlterado.Titulo;
            }
            ctx.SaveChanges();
        }

        // public void Atualizar (int id, TiposUsuario tipoUsuarioAtualizado)
        // TiposUsuario tipoUsuarioBuscado = ctx.TiposUsuario.Find(id);
        // tipoUsuarioBuscado.Titulo = tipoUsuarioAtulizado.Titulo;
        // ctx.TiposUsuario.Update(tipoUsuarioBuscado);
        // ctx.SaveChanges
        
        public TiposUsuario BuscarPorIdTipoUsuario(int id)
        {
            // Retorna o primeiro tipo de usuário encontrado para o ID informado
            return ctx.TiposUsuario.FirstOrDefault(e => e.IdTipoUsuario == id);
        }

        public void CadastrarTipoUsuario(TiposUsuario novoTipoUsuario)
        {
            // Adiciona este novoEstudio
            ctx.TiposUsuario.Add(novoTipoUsuario);
            // Salva as informações para serem gravadas no banco de dados
            ctx.SaveChanges();
        }

        public void DeletarTipoUsuario(int id)
        {
            ctx.TiposUsuario.Remove(BuscarPorIdTipoUsuario(id));
            ctx.SaveChanges();
        }

        public List<TiposUsuario> ListarTipoUsuario()
        {
            return ctx.TiposUsuario.ToList();
        }
    }
}
