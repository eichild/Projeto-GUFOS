using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers {
    
    [Route ("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase {
        BDGUFOSContext _contexto = new BDGUFOSContext ();

        [HttpGet]
        public async Task<ActionResult<List<Localizacao>>> Get () {
            var localizacoes = await _contexto.Localizacao.ToListAsync ();

            if (localizacoes == null) {
                return NotFound ();
            }
            return localizacoes;
        }
        [HttpGet ("{id}")]
        public async Task<ActionResult<Localizacao>> Get (int id) {
            var localizacao = await _contexto.Localizacao.FindAsync (id);

            if (localizacao == null) {
                return NotFound ();
            }
            return localizacao;
        }

        [HttpPost]
        public async Task<ActionResult<Localizacao>> Post (Localizacao localizacao) {
            try {
                await _contexto.AddAsync (localizacao);
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return localizacao;
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Localizacao localizacao) {
            if (id != localizacao.LocalizacaoId) {
                return BadRequest ();
            }
            _contexto.Entry (localizacao).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                var localizacao_valido = await _contexto.Localizacao.FindAsync (id);

                if (localizacao_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            return NoContent ();
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult<Localizacao>> Delete (int id) {
            var localizacao = await _contexto.Localizacao.FindAsync (id);

            if (localizacao == null) {
                return NotFound ();
            }
            _contexto.Localizacao.Remove (localizacao);
            await _contexto.SaveChangesAsync ();

            return localizacao;
        }
    }
}