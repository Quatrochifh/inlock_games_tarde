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
    // ex: http://localhost:5000/api/estudio
    [Route("api/[controller]")]

    [ApiController]
    //[Authorize]
    public class EstudioController : ControllerBase
    {
        private IEstudioRepository _estudioRepository { get; set; }
      
        public EstudioController()
        {
            _estudioRepository = new EstudioRepository();
        }

        //Listar
        [HttpGet]
        public IActionResult Get()
        {
            List<EstudioDomain> listaEstudio = _estudioRepository.ListarTodos();
            return Ok(listaEstudio);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarIdUrl(int id, EstudioDomain EstudioAtualizado)
        {
            EstudioDomain aluguelBuscado = _estudioRepository.BuscarPorId(id);

            if (aluguelBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "Estudio de jogos não encontrado!",
                            erro = true
                        }
                    );
            }

            try
            {
                _estudioRepository.AtualizarIdUrl(id, EstudioAtualizado);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        // ex: http://localhost:5000/api/estudio/2
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EstudioDomain EstudioBuscado = _estudioRepository.BuscarPorId(id);

            if (EstudioBuscado == null)
            {
                return NotFound("Nenhum estudioencontrado!");
            }

            return Ok(EstudioBuscado);
        }

        //Postar
        [HttpPost]
        public IActionResult Post(EstudioDomain novoEstudio)
        {
            //Faz a chamada para o método .cadastrar
            _estudioRepository.Cadastrar(novoEstudio);

            //Retorna um status code 201 - Created
            return StatusCode(201);
        }


        //Deletar
        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            // Faz a chamada para o método .Deletar()
            _estudioRepository.Deletar(id);

            // Retorna um status code 204 - No Content
            return NoContent();
        }

    }
}
