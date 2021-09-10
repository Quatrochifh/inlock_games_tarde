using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Controllers
{
    [Produces("application/json")]

    //Define que a rota de uma requisição será no formato domino/api/nomeController.
    // ex: http://localhost:5000/api/usuario
    [Route("api/[controller]")]

    [ApiController]
    //[Authorize]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }



        //Listar
        [HttpGet]
        public IActionResult Get()
        {
            List<UsuarioDomain> listaUsuario = _usuarioRepository.ListarTodos();
            return Ok(listaUsuario);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarIdUrl(int id, UsuarioDomain UsuarioAtualizado)
        {
            UsuarioDomain UsuarioBuscado = _usuarioRepository.BuscarPorId(id);

            if (UsuarioBuscado == null)
            {
                return NotFound(
                        new
                        {
                            mensagem = "usuario não foi encontrado!",
                            erro = true
                        }
                    );
            }

            try
            {
                _usuarioRepository.AtualizarIdUrl(id, UsuarioAtualizado);

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
            UsuarioDomain UsuarioBuscado = _usuarioRepository.BuscarPorId(id);

            if (UsuarioBuscado == null)
            {
                return NotFound("Nenhum usuario foi encontrado!");
            }

            return Ok(UsuarioBuscado);
        }

        //Postar
        [HttpPost]
        public IActionResult Post(UsuarioDomain novoUsuario)
        {
            //Faz a chamada para o método .cadastrar
            _usuarioRepository.Cadastrar(novoUsuario);

            //Retorna um status code 201 - Created
            return StatusCode(201);
        }


        //Deletar
        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(int id)
        {
            // Faz a chamada para o método .Deletar()
            _usuarioRepository.Deletar(id);

            // Retorna um status code 204 - No Content
            return NoContent();
        }

        [HttpPost("login")]
        public IActionResult Login(UsuarioDomain login)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorEmailSenha(login.email, login.senha);

            if (usuarioBuscado == null)
                return NotFound("E-mail ou senha inválidos!");

            // return Ok(usuarioBuscado);

            // Define os dados que serão fornecidos no token - Payload
            var minhasClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.idUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.tipoDeUsuario.titulo),
                new Claim("Claim personalizada", "Valor Teste")
            };

            // Define a chave de acesso ao token, NUNCA ESQUECER DE COLOCAR AS CHAVES IGUAIS
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("inlock-chave-autenticacao"));

            // Define as credenciais do token - signature
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Composição do token
            var meuToken = new JwtSecurityToken(
                    issuer: "Senai.inlock.webApi",                // emissor do token
                    audience: "Senai.inlock.webApi",                // destinatário do token
                    claims: minhasClaims,                   // dados definidos acima (linha 118)
                    expires: DateTime.Now.AddMinutes(70),    // tempo de expiração do token
                    signingCredentials: creds                   // credenciais do token
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(meuToken)
            });
        }


    }
}
