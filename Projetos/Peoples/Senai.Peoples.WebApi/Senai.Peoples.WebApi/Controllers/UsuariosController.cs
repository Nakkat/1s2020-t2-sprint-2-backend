using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpGet]
        public IEnumerable<UsuarioDomain> Get()
        {

            return _usuarioRepository.ListarUsuario();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarUsuario(int id)
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
            _usuarioRepository.CadastrarUsuario(novoUsuario);

            // Retorna um status code 201 - Created
            return StatusCode(201);
        }

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

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            _usuarioRepository.DeletarUsuario(id);

            return Ok("Usuário deletado");
        }
    }
}
    
