using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Repositories;
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
        UsuarioRepository _repositorio = new UsuarioRepository();

        //METODO PARA LISTAR TODOS OS DADOS DA LISTA DE CATEGORIA PARA PEGAR DO MODEL SELECT*FROM CATEGORIA
        //GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get () {
            //Adiciona como se fosse join
            var usuarios = await _repositorio.Listar ();

            if (usuarios == null) {
                return NotFound ();
            }
            return usuarios;
        }
        //apievento 2 metodo para buscar umaevento só
        //SELECT * FROM CATEGORIA WHERE ID=2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Usuario>> Get (int id) {
            var usuario = await _repositorio.BuscarPorID(id);

            if (usuario == null) {
                return NotFound ();
            }
            return usuario;
        }
        //fim get

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario)
        {
            try
            {
                await _repositorio.Salvar(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return usuario;
        }        


        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return BadRequest();
            }
            try
            {
                await _repositorio.Alterar(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                var usuario_valido = await _repositorio.BuscarPorID(id);

                if (usuario_valido == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            var usuario = await _repositorio.BuscarPorID(id);
            if (usuario == null)
            {
                return NotFound();
            }

            await _repositorio.Excluir(usuario);

            return usuario;
        }
    }
}
