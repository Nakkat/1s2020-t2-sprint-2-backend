using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.Filmes.WebApi.Domains;
using senai.Filmes.WebApi.Interfaces;
using senai.Filmes.WebApi.Repositories;

namespace senai.Filmes.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private IFilmeRepository _filmeRepository { get; set; }

        public FilmesController()
        {
            _filmeRepository = new FilmeRepository();
        }

        [HttpGet]
        public IEnumerable<FilmeDomain> Get()
        {
            return _filmeRepository.Get();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            FilmeDomain filmeBuscado = _filmeRepository.GetById(id);

            if (filmeBuscado == null)
            {
                return NotFound("Nenhum filme encontrado");
            }

            return Ok(filmeBuscado);
        }

        [HttpGet("pesquisar/{busca}")]
        public IActionResult GetByTitle(string busca)
        {
            return Ok(_filmeRepository.BuscarPorTitulo(busca));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Post(FilmeDomain novoFilme)
        {
            _filmeRepository.Register(novoFilme);

            return StatusCode(201);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, FilmeDomain filmeAtualizado)
        {
            FilmeDomain filmeBuscado = _filmeRepository.GetById(id);

            if (filmeBuscado == null)
            {
                return NotFound
                    (
                        new
                        {
                            mensagem = "Filme não encontrado",
                            erro = true
                        }
                    );
            }

            try
            {
                _filmeRepository.Alterar(id, filmeAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult Apagar(int id)
        {
            _filmeRepository.Apagar(id);

            return Ok("Filme deletado");
        }
    }
}