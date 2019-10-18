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
    public class LocalizacaoController : ControllerBase {
        //INSTANCIANDO OBJETO
        BDGUFOSContext _contexto = new BDGUFOSContext ();

        //METODO PARA LISTAR TODOS OS DADOS DA LISTA DE CATEGORIA PARA PEGAR DO MODEL SELECT*FROM CATEGORIA
        //GET: api/Localizacao
        [HttpGet]
        public async Task<ActionResult<List<Localizacao>>> Get () {
            var localizacoes = await _contexto.Localizacao.ToListAsync ();

            if (localizacoes == null) {
                return NotFound ();
            }
            return localizacoes;
        }
        //api localizacao 2 metodo para buscar uma localizacao só
        //SELECT * FROM CATEGORIA WHERE ID=2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Localizacao>> Get (int id) {
            var localizacao = await _contexto.Localizacao.FindAsync (id);

            if (localizacao == null) {
                return NotFound ();
            }
            return localizacao;
        }
        //fim get

        //POST INSERT API/CATEGORIA
        [HttpPost]
        public async Task<ActionResult<Localizacao>> Post (Localizacao localizacao) {
            try {
                //Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync (localizacao);
                //Salvando objeto no banco de dados
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                //Mostra erro
                throw;
            }
            return localizacao;
        }
        //fim Post

        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Localizacao localizacao) {
            if (id != localizacao.LocalizacaoId) {
                return BadRequest ();
            }
            //Comparamos os atributos que foram modificados atraves do EF
            //COMO SE FOSSE UM UPDATE
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
            //retorna erro 204
            return NoContent ();
        }

        //DELETE API/CATEGORIA
        [HttpDelete("{id}")]
        public async Task<ActionResult<Localizacao>> Delete(int id){
            var localizacao = await _contexto.Localizacao.FindAsync(id);

            if(localizacao==null){
                return NotFound();
            }
            
            //Removendo objeto e salva as mudanças
            _contexto.Localizacao.Remove(localizacao);
            await _contexto.SaveChangesAsync();

            return localizacao;
        }
    }
}