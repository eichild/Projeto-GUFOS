using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase {
        // BDGUFOSContext _contexto = new BDGUFOSContext ();
        EventoRepository _repositorio = new EventoRepository ();

        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get () {
            //Adiciona como se fosse join
            var eventos = await _repositorio.Listar ();

            if (eventos == null) {
                return NotFound ();
            }
            return eventos;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Evento>> Get (int id) {
            var evento = await _repositorio.BuscarPorID (id);

            if (evento == null) {
                return NotFound ();
            }
            return evento;
        }

        [HttpPost]
        public async Task<ActionResult<Evento>> Post (Evento evento) {
            try {
                await _repositorio.Salvar (evento);
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
            try {
                await _repositorio.Alterar (evento);
            } catch (DbUpdateConcurrencyException) {
                var evento_valido = await _repositorio.BuscarPorID (id);

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
            var evento = await _repositorio.BuscarPorID (id);

            if (evento == null) {
                return NotFound ();
            }
            await _repositorio.Excluir (evento);

            return evento;
        }
    }
}