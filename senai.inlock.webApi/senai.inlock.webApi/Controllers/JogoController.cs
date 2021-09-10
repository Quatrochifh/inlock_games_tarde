using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Controllers
{
    [Produces("application/json")]

    //Define que a rota de uma requisição será no formato domino/api/nomeController.
    // ex: http://localhost:5000/api/jogo
    [Route("api/[controller]")]

    [ApiController]
    //[Authorize ]
    public class JogoController : ControllerBase
    {
        private IJogosRepository _jogoRepository { get; set; }

        public JogoController()
        {
            _jogoRepository = new JogoRepository();
        }


        //Listar
        [Authorize (Roles = "Administrador,Cliente")]
        [HttpGet]
        public IActionResult Get()
        {
            List<JogosDomain> listaJogo = _jogoRepository.ListarTodos();
            return Ok(listaJogo);
        }



        [HttpPut("{id}")]
        public IActionResult AtualizarIdUrl(int id, JogosDomain JogoAtualizado)
        {
            JogosDomain JogoBuscado = _jogoRepository.BuscarPorId(id);

            if (JogoBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Jogo não foi encontrado!",
                            erro = true
                        }
                    );
            }

            try
            {
                _jogoRepository.AtualizarIdUrl(id, JogoAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        // ex: http://localhost:5000/api/jogo/2
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            JogosDomain JogoBuscado = _jogoRepository.BuscarPorId(id);

            if (JogoBuscado == null)
            {
                return NotFound("Nenhum Jogo foi encontrado!");
            }

            return Ok(JogoBuscado);
        }

        //Postar
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Post(JogosDomain novoJogo)
        {
            //Faz a chamada para o método .cadastrar
            _jogoRepository.Cadastrar(novoJogo);

            //Retorna um status code 201 - Created
            return StatusCode(201);
        }


        //Deletar
        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            // Faz a chamada para o método .Deletar()
            _jogoRepository.Deletar(id);

            // Retorna um status code 204 - No Content
            return NoContent();
        }

    }
}
