using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.InLock.WebApi.DataBaseFirst.Domains;
using Senai.InLock.WebApi.DataBaseFirst.Interfaces;
using Senai.InLock.WebApi.DataBaseFirst.Repositories;

namespace Senai.InLock.WebApi.DataBaseFirst.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TiposUsuarioController : ControllerBase
    {
        /// <summary>
        /// Cria um objeto _tipoUsuarioRepository que irá receber todos os métodos definidos na interface
        /// </summary>
        private ITipoUsuarioRepository _tipoUsuarioRepository;

        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public TiposUsuarioController()
        {
            _tipoUsuarioRepository = new TipoUsuarioRepository();
        }

        /// <summary>
        /// Lista todos os tipos de usuários
        /// </summary>
        /// <returns>Uma lista dos tipos de usuário e um status code 200 - Ok</returns>
        [HttpGet]
        public IActionResult Get()
        {
            // Retora a resposta da requisição fazendo a chamada para o método
            return Ok(_tipoUsuarioRepository.ListarTipoUsuario());
        }

        /// <summary>
        /// Busca um tipo de usuário através do ID
        /// </summary>
        /// <param name="id">ID do estúdio que será buscado</param>
        /// <returns>Um estúdio buscado e um status code 200 - Ok</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Retora a resposta da requisição fazendo a chamada para o método
            return Ok(_tipoUsuarioRepository.BuscarPorIdTipoUsuario(id));
        }

        /// <summary>
        /// Cadastra um novo tipo de usuário
        /// </summary>
        /// <param name="novoEstudio">Objeto novoTipoUsuario que será cadastrado</param>
        /// <returns>Um status code 201 - Created</returns>
        [HttpPost]
        public IActionResult Post(TiposUsuario novoTipoUsuario)
        {
            // Faz a chamada para o método
            _tipoUsuarioRepository.CadastrarTipoUsuario(novoTipoUsuario);

            // Retorna um status code
            return StatusCode(201);
        }

        /// <summary>
        /// Altera um tipo de usuário
        /// </summary>
        /// <param name="id">Id que será buscado</param>
        /// <param name="tipoUsuarioAtualizado">Objeto tipoUsuarioAtualizado que será atualizado</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, TiposUsuario tipoUsuarioAtualizado)
        {
            // Criando objeto de TiposUsuario e  atribuindo o método Buscar por ID
            TiposUsuario estudioBuscado = _tipoUsuarioRepository.BuscarPorIdTipoUsuario(id);

            // Se o jogoBuscado for nulo:
            if (estudioBuscado == null)
            {
                return NotFound
                    (
                        new
                        {
                            mensagem = "Tipo de usuário não encontrado",
                            erro = true
                        }
                    );
            }

            // Caso contrário, tente:
            try
            {
                _tipoUsuarioRepository.AlterarTipoUsuario(id, tipoUsuarioAtualizado);

                return NoContent();
            }
            // Se não retorna:
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Deleta um tipo de usuário
        /// </summary>
        /// <param name="id">Id que será buscado</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _tipoUsuarioRepository.DeletarTipoUsuario(id);

            return Ok("Tipo de usuário deletado");
        }
    }
}