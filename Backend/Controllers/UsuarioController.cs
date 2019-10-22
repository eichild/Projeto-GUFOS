using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//PARA ADICIONAR A ÁRVORE DE OBJETOS ADICIONAMOS UMA NOVA BIBLIOTECA JSON
//dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
namespace Backend.Controllers {
    //COMO SE FOSSE OS COMANDOS DQL AQUI NO BACKEND

    //DEFININDO ROTA do controller e dizendo que é um controller para api
    [Route ("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase {
        //INSTANCIANDO OBJETO
        BDGUFOSContext _contexto = new BDGUFOSContext ();

        //METODO PARA LISTAR TODOS OS DADOS DA LISTA DE CATEGORIA PARA PEGAR DO MODEL SELECT*FROM CATEGORIA
        //GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get () {
            //Adiciona como se fosse join
            var usuarios = await _contexto.Usuario.Include ("TipoUsuario").ToListAsync ();

            if (usuarios == null) {
                return NotFound ();
            }
            return usuarios;
        }
        //apievento 2 metodo para buscar umaevento só
        //SELECT * FROM CATEGORIA WHERE ID=2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Usuario>> Get (int id) {
            var usuario = await _contexto.Usuario.FindAsync (id);

            if (usuario == null) {
                return NotFound ();
            }
            return usuario;
        }
        //fim get
    }
}