using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//PARA ADICIONAR A ÁRVORE DE OBJETOS ADICIONAMOS UMA NOVA BIBLIOTECA JSON
//dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
namespace Backend.Controllers {
    //COMO SE FOSSE OS COMANDOS DQL AQUI NO BACKEND

    //DEFININDO ROTA do controller e dizendo que é um controller para api
    [Route ("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase {
        //INSTANCIANDO OBJETO
        BDGUFOSContext _contexto = new BDGUFOSContext ();

        //METODO PARA LISTAR TODOS OS DADOS DA LISTA DE CATEGORIA PARA PEGAR DO MODEL SELECT*FROM CATEGORIA
        //GET: api/Evento
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get () {
            //Adiciona como se fosse join
            var eventos = await _contexto.Evento.Include ("Categoria").Include ("Localizacao").ToListAsync ();

            if (eventos == null) {
                return NotFound ();
            }
            return eventos;
        }
        //apievento 2 metodo para buscar umaevento só
        //SELECT * FROM CATEGORIA WHERE ID=2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Evento>> Get (int id) {
            var evento = await _contexto.Evento.Include ("Categoria").Include ("Localizacao").FirstOrDefaultAsync (e => e.EventoId == id);

            if (evento == null) {
                return NotFound ();
            }
            return evento;
        }
        //fim get

        //POST INSERT API/CATEGORIA
        [HttpPost]
        public async Task<ActionResult<Evento>> Post (Evento evento) {
            try {
                //Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync (evento);
                //Salvando objeto no banco de dados
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                //Mostra erro
                throw;
            }
            return evento;
        }
        //fim Post

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Evento evento) {
            if (id != evento.EventoId) {
                return BadRequest ();
            }
            //Comparamos os atributos que foram modificados atraves do EF
            //COMO SE FOSSE UM UPDATE
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
            //retorna erro 204
            return NoContent ();
        }

        //DELETE API/CATEGORIA
        [HttpDelete ("{id}")]
        public async Task<ActionResult<Evento>> Delete (int id) {
            var evento = await _contexto.Evento.FindAsync (id);

            if (evento == null) {
                return NotFound ();
            }

            //Removendo objeto e salva as mudanças
            _contexto.Evento.Remove (evento);
            await _contexto.SaveChangesAsync ();

            return evento;
        }
    }
}