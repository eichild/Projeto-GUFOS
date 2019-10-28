using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories {
    public class LocalizacaoRepository : ILocalizacao {
        public async Task<Localizacao> Alterar (Localizacao localizacao) {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                _contexto.Entry (localizacao).State = EntityState.Modified;
                await _contexto.SaveChangesAsync ();
                return localizacao;

            }
        }

        public async Task<Localizacao> BuscarPorID (int id) {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                return await _contexto.Localizacao.FindAsync (id);
            }
        }

        public async Task<Localizacao> Excluir (Localizacao localizacao) {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                _contexto.Localizacao.Remove (localizacao);
                await _contexto.SaveChangesAsync ();
                return localizacao;
            }

        }

        public async Task<List<Localizacao>> Listar () {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                return await _contexto.Localizacao.ToListAsync ();
            }
        }

        public async Task<Localizacao> Salvar (Localizacao localizacao) {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                await _contexto.AddAsync (localizacao);
                await _contexto.SaveChangesAsync ();
                return localizacao;
            }

        }
    }
}