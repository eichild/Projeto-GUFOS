using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase {
        BDGUFOSContext _contexto = new BDGUFOSContext ();

        /// <summary>
        /// Metodo para listar os tipos de usuario
        /// </summary>
        /// <returns>Tipo de usuario</returns>
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get () {
            var tipoUsuarios = await _contexto.TipoUsuario.ToListAsync ();

            if (tipoUsuarios == null) {
                return NotFound ();
            }
            return tipoUsuarios;
        }
        [HttpGet ("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get (int id) {
            var usuario = await _contexto.TipoUsuario.FindAsync (id);

            if (usuario == null) {
                return NotFound ();
            }
            return usuario;
        }
    }
}   