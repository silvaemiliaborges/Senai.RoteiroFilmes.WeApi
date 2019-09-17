using Microsoft.AspNetCore.Mvc;
using Senai.RoteiroFilmess.WebApi.Domains;
using Senai.RoteiroFilmess.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.RoteiroFilmess.WebApi.Controllers
{
    public class FilmesControllers
    {
        [Route("api/[controller]")]
        [ApiController]
        [Produces("application/json")]
        public class FilmesController : ControllerBase
        {
            FilmesRepository filmesRepository = new FilmesRepository();

            [HttpGet]
            public IEnumerable<Filmes> Listar()
            {
                return filmesRepository.Listar();
            }

            [HttpGet("{id}")]
            public IActionResult BuscarPorId(int id)
            {
                Filmes filmesDomain = filmesRepository.BuscarPorId(id);
                if (filmesDomain == null)
                    return NotFound();
                return Ok(filmesDomain);
            }



            [HttpPost]
            public IActionResult Cadastrar(Filmes filme)
            {
                try
                {
                    filmesRepository.Cadastrar(filme);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(new { mensagem = "Man deu erro " + ex.Message });

                }
            }

            [HttpDelete("{id}")]
            public IActionResult Deletar(int id)
            {
                filmesRepository.Deletar(id);
                return Ok();
            }

        }
    }
}

