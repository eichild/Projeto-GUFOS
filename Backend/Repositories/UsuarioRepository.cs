using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories {
    public class UsuarioRepository : IUsuario {
        public async Task<Usuario> Alterar (Usuario usuario) {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                 _contexto.Entry(usuario).State = EntityState.Modified;
                  await _contexto.SaveChangesAsync();
                  return usuario;
            }
        }

        public async Task<Usuario> BuscarPorID (int id) {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
               return await _contexto.Usuario.Include(t => t.TipoUsuario).FirstOrDefaultAsync(e => e.UsuarioId == id);

            }
        }

        public async Task<Usuario> Excluir (Usuario usuario) {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                _contexto.Usuario.Remove(usuario);
                await _contexto.SaveChangesAsync();
                return usuario;
            }
        }

        public async Task<List<Usuario>> Listar () {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                return await _contexto.Usuario.ToListAsync ();
            }
        }

        public async Task<Usuario> Salvar (Usuario usuario) {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                await _contexto.AddAsync(usuario);
                await _contexto.SaveChangesAsync();
                return usuario;
            }
        }
    }
}