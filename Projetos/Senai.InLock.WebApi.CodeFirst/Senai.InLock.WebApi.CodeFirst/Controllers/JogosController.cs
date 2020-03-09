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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        /// <summary>
        /// Cria um objeto _jogoRepository que irá receber todos os métodos definidos na interface
        /// </summary>
        private IJogoRepository _jogoRepository;

        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public JogosController()
        {
            _jogoRepository = new JogoRepository();
        }

        /// <summary>
        /// Lista todos os jogos
        /// </summary>
        /// <returns>Uma lista de jogos e um status code 200 - Ok</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            // Retora a resposta da requisição fazendo a chamada para o método
            return Ok(_jogoRepository.ListarJogo());
        }

        /// <summary>
        /// Busca um jogo através do ID
        /// </summary>
        /// <param name="id">ID do jogo que será buscado</param>
        /// <returns>Um jogo buscado e um status code 200 - Ok</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarporId(int id)
        {
            // Retora a resposta da requisição fazendo a chamada para o método
            return Ok(_jogoRepository.BuscarPorIdJogo(id));
        }

        /// <summary>
        /// Cadastra um novo jogo
        /// </summary>
        /// <param name="novoEstudio">Objeto novoJogo que será cadastrado</param>
        /// <returns>Um status code 201 - Created</returns>
        [HttpPost]
        public IActionResult Cadastrar(Jogos novoJogo)
        {
            try
            {
                // Faz a chamada para o método
                _jogoRepository.CadastrarJogo(novoJogo);

                // Retorna um status code
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Altera um jogo
        /// </summary>
        /// <param name="id">Id do jogo que será buscado</param>
        /// <param name="jogoAtualizado">Objeto jogoAtualizado que será atualizado</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Jogos jogoAtualizado)
        {
            // Criando objeto de Jogos e  atribuindo o método Buscar por ID
            Jogos jogoBuscado = _jogoRepository.BuscarPorIdJogo(id);

            // Se o jogoBuscado for nulo: 
            if (jogoBuscado == null)
            {
                return NotFound
                    (
                        new
                        {
                            mensagem = "Jogo não encontrado",
                            erro = true
                        }
                    );
            }

            // Caso contrário, tente:
            try
            {
                _jogoRepository.AlterarJogo(id, jogoAtualizado);

                return NoContent();
            }
            // Se não retorna:
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Deleta um jogo
        /// </summary>
        /// <param name="id">Id que será buscado</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            _jogoRepository.DeletarJogo(id);

            return Ok("Jogo deletado");
        }

        /// <summary>
        /// Lista todos os jogos com as informações dos estúdios
        /// </summary>
        /// <returns>Uma lista de jogos</returns>
        [HttpGet("Estudios")]
        public IActionResult GetEstudios()
        {
            // Retorna a resposta da requisição fazendo a chamada para o método
            return Ok(_jogoRepository.ListarComEstudios());
        }

        /// <summary>
        /// Lista todos os jogos de um determinado estúdio
        /// </summary>
        /// <param name="id">ID do estúdio do qual os jogos serão listados</param>
        /// <returns>Uma lista de jogos</returns>
        [HttpGet("Estudios/{id}")]
        public IActionResult GetUmEstudio(int id)
        {
            // Retorna a resposta da requisição fazendo a chamada para o método
            return Ok(_jogoRepository.ListarUmEstudio(id));
        }
    }
}
