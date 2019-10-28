using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers {
    //COMO SE FOSSE OS COMANDOS DQL AQUI NO BACKEND

    //DEFININDO ROTA do controller e dizendo que é um controller para api
    [Route ("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase {

        //INSTANCIANDO REPOSITORIO
        CategoriaRepository _repositorio = new CategoriaRepository ();

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get () {
            var categorias = await _repositorio.Listar ();

            if (categorias == null) {
                return NotFound ();
            }
            return categorias;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Categoria>> Get (int id) {
            var categoria = await _repositorio.BuscarPorID (id);

            if (categoria == null) {
                return NotFound ();
            }
            return categoria;
        }


        //POST INSERT API/CATEGORIA
        [HttpPost]
        public async Task<ActionResult<Categoria>> Post (Categoria categoria) {
            try {
                await _repositorio.Salvar (categoria);
            } catch (DbUpdateConcurrencyException) {
                //Mostra erro
                throw;
            }
            return categoria;
        }
        //fim Post

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Categoria categoria) {
            if (id != categoria.CategoriaId) {
                return BadRequest ();
            }
            try {
                await _repositorio.Alterar (categoria);

            } catch (DbUpdateConcurrencyException) {
                var categoria_valido = await _repositorio.BuscarPorID (id);

                if (categoria_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            //retorna erro 204
            return NoContent ();
        }

        //DELETE API/CATEGORIA
        [HttpDelete ("{id}")]
        public async Task<ActionResult<Categoria>> Delete (int id) {
            var categoria = await _repositorio.BuscarPorID (id);

            if (categoria == null) {
                return NotFound ();
            }
            await _repositorio.Excluir(categoria);

            return categoria;
        }
    }
}