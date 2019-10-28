using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories {
    public class CategoriaRepository : ICategoria {
        public async Task<Categoria> Alterar (Categoria categoria) {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                _contexto.Entry (categoria).State = EntityState.Modified;

                await _contexto.SaveChangesAsync ();
                return categoria;
            }
        }

        public async Task<Categoria> BuscarPorID (int id) {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                return await _contexto.Categoria.FindAsync (id);
            }
        }

        public async Task<Categoria> Excluir (Categoria categoria) {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                //Removendo objeto e salva as mudanças
                _contexto.Categoria.Remove (categoria);
                await _contexto.SaveChangesAsync ();

                return categoria;
            }
        }

        public async Task<List<Categoria>> Listar () {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                return await _contexto.Categoria.ToListAsync ();
            }
        }

        public async Task<Categoria> Salvar (Categoria categoria) {
            using (BDGUFOSContext _contexto = new BDGUFOSContext ()) {
                //Tratamos contra ataques de SQL INJECTION
                await _contexto.AddAsync (categoria);
                //Salvando objeto no banco de dados
                await _contexto.SaveChangesAsync ();
                return categoria;
            }
        }
    }
}