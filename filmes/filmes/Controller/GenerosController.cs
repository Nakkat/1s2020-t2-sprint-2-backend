using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using filmes.Domain;
using filmes.Interfaces;
using filmes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace filmes.Controller
{
    [Produces ("application/json")]
    
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private IGeneroRepository _generoRepository { get; set; }

        public GenerosController()
        {
            _generoRepository = new GeneroRepository();
        }

        [HttpGet]
        public IEnumerable<GeneroDomain> Get()
        {
            return _generoRepository.Listar();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GeneroDomain generoBuscado = _generoRepository.GetById(id);

            if(generoBuscado == null)
            {
                return NotFound();
            }
            return StatusCode(200, generoBuscado);
        }


        [HttpPost]
        public IActionResult Cadastrar(GeneroDomain generoRecebido)
        {
            try
            {
                _generoRepository.Cadastrar(generoRecebido);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { mensagem = "Erro" + ex });
            }
            //return Ok();
            return StatusCode(201); // Status Code Created
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarIdUrl(int id, GeneroDomain generoAtualizado)
        {
            GeneroDomain generoBuscado = _generoRepository.GetById(id);
            if (generoBuscado == null)
            {
                return NotFound(new { mensagem = "Nenhum gênero encontrado", erro = true });
            };
            
            try
            {
                _generoRepository.AtualizarIdUrl(id, generoAtualizado);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { mensagem = "Erro" + ex });
            }
        }

        [HttpPut]
        public IActionResult AtualizarIdCorpo(GeneroDomain genero)
        {
            try
            {
                _generoRepository.AtualizarIdCorpo(genero);
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { mensagem = "Erro" + ex });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _generoRepository.Deletar(id);
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { mensagem = "Erro" + ex });
            }
        }
    }
}