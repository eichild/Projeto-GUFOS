using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories {
   public class PresencaRepository : IPresenca {
      public async Task<Presenca> Alterar (Presenca presenca) {
         using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
            _contexto.Entry (presenca).State = EntityState.Modified;
             await _contexto.SaveChangesAsync();
             return presenca;

         }
      }

      public async Task<Presenca> BuscarPorID (int id) {
         using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
            return await _contexto.Presenca.FindAsync (id);
         }
      }
      public async Task<Presenca> Excluir (Presenca presenca) {
         using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
            _contexto.Presenca.Remove (presenca);
            await _contexto.SaveChangesAsync ();
            return presenca;
         }
      }

      public async Task<List<Presenca>> Listar () {
         using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
            return await _contexto.Presenca.Include ("Usuario").Include ("Evento").ToListAsync ();
         }
      }

      public async Task<Presenca> Salvar (Presenca presenca) {
         using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
            await _contexto.AddAsync (presenca);
            await _contexto.SaveChangesAsync ();
            return presenca;
         }
      }
   }
}