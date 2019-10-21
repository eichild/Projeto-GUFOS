using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase {
        BDGUFOSContext _contexto = new BDGUFOSContext ();

        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get () {
            //Adiciona como se fosse join
            var eventos = await _contexto.Evento.Include ("Categoria").Include ("Localizacao").ToListAsync ();

            if (eventos == null) {
                return NotFound ();
            }
            return eventos;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Evento>> Get (int id) {
            var evento = await _contexto.Evento.Include ("Categoria").Include ("Localizacao").FirstOrDefaultAsync (e => e.EventoId == id);

            if (evento == null) {
                return NotFound ();
            }
            return evento;
        }

        [HttpPost]
        public async Task<ActionResult<Evento>> Post (Evento evento) {
            try {
                await _contexto.AddAsync (evento);
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return evento;
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Evento evento) {
            if (id != evento.EventoId) {
                return BadRequest ();
            }

            _contexto.Entry (evento).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                var evento_valido = await _contexto.Evento.FindAsync (id);

                if (evento_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            return NoContent ();
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult<Evento>> Delete (int id) {
            var evento = await _contexto.Evento.FindAsync (id);

            if (evento == null) {
                return NotFound ();
            }
            _contexto.Evento.Remove (evento);
            await _contexto.SaveChangesAsync ();

            return evento;
        }
    }
}