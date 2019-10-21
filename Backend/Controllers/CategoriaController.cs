using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers {
    //COMO SE FOSSE OS COMANDOS DQL AQUI NO BACKEND

    //DEFININDO ROTA do controller e dizendo que é um controller para api
    [Route ("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase {
        //INSTANCIANDO OBJETO
        BDGUFOSContext _contexto = new BDGUFOSContext ();

        //METODO PARA LISTAR TODOS OS DADOS DA LISTA DE CATEGORIA PARA PEGAR DO MODEL SELECT*FROM CATEGORIA
        //GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get () {
            var categorias = await _contexto.Categoria.ToListAsync ();

            if (categorias == null) {
                return NotFound ();
            }
            return categorias;
        }
        //api categoria 2 metodo para buscar uma categoria só
        //SELECT * FROM CATEGORIA WHERE ID=2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Categoria>> Get (int id) {
            var categoria = await _contexto.Categoria.FindAsync (id);

            if (categoria == null) {
                return NotFound ();
            }
            return categoria;
        }
        //fim get

        //POST INSERT API/CATEGORIA
        [HttpPost]
        public async Task<ActionResult<Categoria>> Post (Categoria categoria) {
            try {
                //Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync (categoria);
                //Salvando objeto no banco de dados
                await _contexto.SaveChangesAsync ();
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
            //Comparamos os atributos que foram modificados atraves do EF
            //COMO SE FOSSE UM UPDATE
            _contexto.Entry (categoria).State = EntityState.Modified;

            try {
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                var categoria_valido = await _contexto.Categoria.FindAsync (id);

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
            var categoria = await _contexto.Categoria.FindAsync (id);

            if (categoria == null) {
                return NotFound ();
            }

            //Removendo objeto e salva as mudanças
            _contexto.Categoria.Remove (categoria);
            await _contexto.SaveChangesAsync ();

            return categoria;
        }
    }
}