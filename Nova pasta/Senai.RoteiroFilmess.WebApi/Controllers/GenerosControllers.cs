using Microsoft.AspNetCore.Mvc;
using Senai.RoteiroFilmess.WebApi.Domains;
using Senai.RoteiroFilmess.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.RoteiroFilmess.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        GeneroRepository GeneroRepository = new GeneroRepository();
        [HttpGet]
        public IEnumerable<Generos> Listar()
        {
            return GeneroRepository.Listar();
        }

        [HttpPost]
        public IActionResult Cadastrar(Generos genero)
        {
            GeneroRepository.Cadastrar(genero);
            return Ok();
        }

        [HttpPut]
        public IActionResult Atualizar(Generos genero)
        {
            GeneroRepository.Atualizar(genero);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            GeneroRepository.Deletar(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            Generos estiloDomain = GeneroRepository.BuscarPorId(id);
            if (estiloDomain == null)
                return NotFound();
            return Ok(estiloDomain);
        }
    }
}

