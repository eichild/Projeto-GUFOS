using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase {

        LocalizacaoRepository _repositorio = new LocalizacaoRepository ();
        [HttpGet]
        public async Task<ActionResult<List<Localizacao>>> Get () {
            var localizacoes = await _repositorio.Listar ();

            if (localizacoes == null) {
                return NotFound ();
            }
            return localizacoes;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Localizacao>> Get (int id) {
            var localizacao = await _repositorio.BuscarPorID (id);

            if (localizacao == null) {
                return NotFound ();
            }
            return localizacao;
        }

        [HttpPost]
        public async Task<ActionResult<Localizacao>> Post (Localizacao localizacao) {
            try {
                await _repositorio.Salvar (localizacao);
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

            try {
                await _repositorio.Alterar (localizacao);
            } catch (DbUpdateConcurrencyException) {
                var localizacao_valido = await _repositorio.BuscarPorID (id);

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
            var localizacao = await _repositorio.BuscarPorID (id);

            if (localizacao == null) {
                return NotFound ();
            }
            await _repositorio.Excluir (localizacao);

            return localizacao;
        }
    }
}