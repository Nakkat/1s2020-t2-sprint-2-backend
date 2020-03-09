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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FuncionariosController : ControllerBase
    {
        private IFuncionarioRepository _funcionarioRepository { get; set; }

        public FuncionariosController()
        {
            _funcionarioRepository = new FuncionarioRepository();
        }

        [HttpGet]
        public IEnumerable<FuncionarioDomain> Get()
        {
            
            return _funcionarioRepository.Listar();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);

            if (funcionarioBuscado == null)
            {
               
                return NotFound("Nenhum funcionário encontrado");
            }

            return Ok(funcionarioBuscado);
        }

        [Authorize(Roles = "1")]
        [HttpGet("Buscar/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            return StatusCode(200, _funcionarioRepository.BuscarPorNome(nome));
        }

        [Authorize(Roles = "1")]
        [HttpGet("Ordenacao/{ordem}")]
        public IActionResult OrdenarAsc(string ordem)
        {
            if (ordem == "ASC")
            {
                return StatusCode(200, _funcionarioRepository.OrdenarAsc());
            }
            else
            {
                return StatusCode(400);
            }
        }

        [HttpPost]
        public IActionResult Post(FuncionarioDomain novoFuncionario)
        {
            // Faz a chamada para o método .Cadastrar();
            _funcionarioRepository.Cadastrar(novoFuncionario);

            // Retorna um status code 201 - Created
            return StatusCode(201);
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, FuncionarioDomain funcionarioAtualizado)
        {
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);

            if (funcionarioBuscado == null)
            {
                return NotFound
                    (
                        new
                        {
                            mensagem = "Funcionário não encontrado",
                            erro = true
                        }
                    );
            }
       
            try
            {
                _funcionarioRepository.Alterar(id, funcionarioAtualizado);

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
            _funcionarioRepository.Deletar(id);

            return Ok("Funcionário deletado");
        }
    }
}