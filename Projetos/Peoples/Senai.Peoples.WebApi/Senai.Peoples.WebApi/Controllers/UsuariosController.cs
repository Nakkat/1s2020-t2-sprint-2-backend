using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public IEnumerable<UsuarioDomain> ListarUsuario()
        {

            return _usuarioRepository.ListarUsuario();
        }

        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult BuscarPorIdUsuario(int id)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorIdUsuario(id);

            if (usuarioBuscado == null)
            {

                return NotFound("Nenhum usuário encontrado");
            }

            return Ok(usuarioBuscado);
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(UsuarioDomain novoUsuario)
        {
            // Faz a chamada para o método .Cadastrar();

            try
            {
            _usuarioRepository.CadastrarUsuario(novoUsuario);
                return StatusCode(201);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            // Retorna um status code 201 - Created
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult AlterarUsuario(int id, UsuarioDomain usuarioAtualizado)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorIdUsuario(id);

            if (usuarioBuscado == null)
            {
                return NotFound
                    (
                        new
                        {
                            mensagem = "Usuário não encontrado",
                            erro = true
                        }
                    );
            }

            try
            {
                _usuarioRepository.AlterarUsuario(id, usuarioAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            _usuarioRepository.DeletarUsuario(id);

            return Ok("Usuário deletado");
        }
    }
}
    
