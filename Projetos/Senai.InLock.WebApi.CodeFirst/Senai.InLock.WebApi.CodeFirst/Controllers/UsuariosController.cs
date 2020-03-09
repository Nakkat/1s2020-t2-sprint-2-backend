using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.InLock.WebApi.CodeFirst.Domains;
using Senai.InLock.WebApi.CodeFirst.Interfaces;
using Senai.InLock.WebApi.CodeFirst.Repositories;

namespace Senai.InLock.WebApi.CodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        /// <summary>
        /// Cria um objeto _usuarioRepository que irá receber todos os métodos definidos na interface
        /// </summary>
        private IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>Uma lista de usuários e um status code 200 - Ok</returns>
        [HttpGet]
        public IActionResult Get()
        {
            // Retora a resposta da requisição fazendo a chamada para o método
            return Ok(_usuarioRepository.ListarUsuario());
        }

        /// <summary>
        /// Busca um usuário através do ID
        /// </summary>
        /// <param name="id">ID do usuário que será buscado</param>
        /// <returns>Um usuário buscado e um status code 200 - Ok</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Retora a resposta da requisição fazendo a chamada para o método
            return Ok(_usuarioRepository.BuscarPorIdUsuario(id));
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="novoEstudio">Objeto novoUsuario que será cadastrado</param>
        /// <returns>Um status code 201 - Created</returns>
        [HttpPost]
        public IActionResult Post(Usuarios novoUsuario)
        {
            // Faz a chamada para o método
            _usuarioRepository.CadastrarUsuario(novoUsuario);

            // Retorna um status code
            return StatusCode(201);
        }

        /// <summary>
        /// Altera um usuário
        /// </summary>
        /// <param name="id">Id que será buscado</param>
        /// <param name="tipoUsuarioAtualizado">Objeto usuarioAtualizado que será atualizado</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Usuarios usuarioAtualizado)
        {
            // Criando objeto de Usuarios e atribuindo o método Buscar por ID
            Usuarios usuarioBuscado = _usuarioRepository.BuscarPorIdUsuario(id);

            // Se o usuarioBuscado for nulo:
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

            // Caso contrário, tente:
            try
            {
                _usuarioRepository.AlterarUsuario(id, usuarioAtualizado);

                return NoContent();
            }
            // Se não retorna:
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Deleta um usuário
        /// </summary>
        /// <param name="id">Id que será buscado</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _usuarioRepository.DeletarUsuario(id);

            return Ok("Usuário deletado");
        }
    }
}