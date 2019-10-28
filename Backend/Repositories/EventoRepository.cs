using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories {
    public class EventoRepository : IEvento {
        public async Task<Evento> Alterar (Evento evento) {
            using (BDGUFOSContext _context = new BDGUFOSContext ()) {
                _context.Entry (evento).State = EntityState.Modified;
                await _context.SaveChangesAsync ();
                return evento;

            }
        }

        public async Task<Evento> BuscarPorID (int id) {
            using (BDGUFOSContext _context = new BDGUFOSContext ()) {
                return await _context.Evento.Include ("Categoria").Include ("Localizacao").FirstOrDefaultAsync (e => e.EventoId == id);
            }
        }

        public async Task<Evento> Excluir (Evento evento) {
            using (BDGUFOSContext _context = new BDGUFOSContext ()) {
                _context.Evento.Remove (evento);
                await _context.SaveChangesAsync ();
                return (evento);
            }
        }

        public async Task<List<Evento>> Listar () {
            using (BDGUFOSContext _context = new BDGUFOSContext ()) {
             return await _context.Evento.Include ("Categoria").Include ("Localizacao").ToListAsync ();
            }
        }

        public async Task<Evento> Salvar (Evento evento) {
            using (BDGUFOSContext _context = new BDGUFOSContext ()) {
                await _context.AddAsync (evento);
                await _context.SaveChangesAsync ();
                return evento;

            }

        }
    }
}