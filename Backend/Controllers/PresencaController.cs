using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class PresencaController : ControllerBase {
        BDGUFOSContext _contexto = new BDGUFOSContext ();

        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get () {
            var presencas = await _contexto.Presenca.Include ("Usuario").Include ("Evento").ToListAsync ();

            if (presencas == null) {
                return NotFound ();
            }
            return presencas;
        }
       
        [HttpGet ("{id}")]
        public async Task<ActionResult<Presenca>> Get (int id) {
            var presenca = await _contexto.Presenca.FindAsync (id);

            if (presenca == null) {
                return NotFound ();
            }
            return presenca;
        }
        //fim get

        [HttpPost]
        public async Task<ActionResult<Presenca>> Post (Presenca presenca) {
            try {
                await _contexto.AddAsync (presenca);
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return presenca;
        }
        //fim Post

    }
}