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
    /// <summary>
    /// Controller responsável pelos endpoints referentes aos estudios
    /// </summary>

    // Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    // Define que a rota de uma requisição será no formato domínio/api/NomeController
    [Route("api/[controller]")]

    // Define que é um controlador de API
    [ApiController]
    public class EstudiosController : ControllerBase
    {
        /// <summary>
        /// Cria um objeto _estudioRepository que irá receber todos os métodos definidos na interface
        /// </summary>
        private IEstudioRepository _estudioRepository;

        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public EstudiosController()
        {
            _estudioRepository = new EstudioRepository();
        }

        /// <summary>
        /// Lista todos os estúdios
        /// </summary>
        /// <returns>Uma lista de estúdios e um status code 200 - Ok</returns>
        [HttpGet]
        public IActionResult Get()
        {
            // Retora a resposta da requisição fazendo a chamada para o método
            return Ok(_estudioRepository.Listar());
        }

        /// <summary>
        /// Busca um estúdio através do ID
        /// </summary>
        /// <param name="id">ID do estúdio que será buscado</param>
        /// <returns>Um estúdio buscado e um status code 200 - Ok</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Retora a resposta da requisição fazendo a chamada para o método
            return Ok(_estudioRepository.BuscarPorId(id));
        }

        /// <summary>
        /// Cadastra um novo estúdio
        /// </summary>
        /// <param name="novoEstudio">Objeto novoEstudio que será cadastrado</param>
        /// <returns>Um status code 201 - Created</returns>
        [HttpPost]
        public IActionResult Post(Estudios novoEstudio)
        {
            // Tenta fazer o método
            try
            {
                // Faz a chamada para o método
                _estudioRepository.Cadastrar(novoEstudio);

                // Retorna um status code
                return StatusCode(201);
            }
            // Caso contrário retorna uma mensagem de erro de má requisição
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Altera um estúdio
        /// </summary>
        /// <param name="id">Id do estúdio que será buscado</param>
        /// <param name="estudioAtualizado">Objeto estudioAtualizado que será alterado</param>
        /// <returns>Um Status Code 204 (No Content)</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Estudios estudioAtualizado)
        {
            // Cria um objeto em Estudios para armazenar  IdBuscado
            Estudios estudioBuscado = _estudioRepository.BuscarPorId(id);

            // Se o Id do estúdio buscado for nulo :
            if (estudioBuscado == null)
            {
                return NotFound
                    (
                        new
                        {
                            mensagem = "Estúdio não encontrado",
                            erro = true
                        }
                    );
            }

            // Tenta fazer o método
            try
            {
                _estudioRepository.Alterar(id, estudioAtualizado);

                return NoContent();
            }

            // Caso contrário : 
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Deleta um estúdio
        /// </summary>
        /// <param name="id">Id do estúdio que será deletado</param>
        /// <returns>Um status code 200</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _estudioRepository.Deletar(id);

                return Ok("Estúdio deletado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista todos os estúdios com seus respectivos jogos
        /// </summary>
        /// <returns>Lista de estúdios com os jogos e um status code 200 - Ok</returns>
        [HttpGet("Jogos")]
        public IActionResult GetJogos()
        {
            // Retorna a resposta da requisição fazendo a chamada para o método
            return Ok(_estudioRepository.ListarJogos());
        }
    }
}