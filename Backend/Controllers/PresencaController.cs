using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class PresencaController : ControllerBase {
        PresencaRepository _repositorio = new PresencaRepository ();

        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get () {
            var presencas = await _repositorio.Listar ();

            if (presencas == null) {
                return NotFound ();
            }
            return presencas;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Presenca>> Get (int id) {
            var presenca = await _repositorio.BuscarPorID (id);

            if (presenca == null) {
                return NotFound ();
            }
            return presenca;
        }
        //fim get

        [HttpPost]
        public async Task<ActionResult<Presenca>> Post (Presenca presenca) {
            try {
                await _repositorio.Salvar (presenca);
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return presenca;
        }
        //fim Post

        [HttpPut ("{id}")]
        public async Task<IActionResult> Put (int id, Presenca presenca) {
            if (id != presenca.PresencaId) {
                return BadRequest ();
            }

            try {
                await _repositorio.Alterar (presenca);
            } catch (DbUpdateConcurrencyException) {
                var presenca_valido = await _repositorio.BuscarPorID (id);

                if (presenca_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }

            return NoContent ();
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult<Presenca>> Delete (int id) {
            var presenca = await _repositorio.BuscarPorID (id);

            if (presenca == null) {
                return NotFound ();
            }

            await _repositorio.Excluir (presenca);

            return presenca;
        }

    }
}
