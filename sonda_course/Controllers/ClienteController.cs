using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace sonda_course.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClienteController : ControllerBase
    {
        private readonly DbCliente dbCliente;


        public ClienteController(DbCliente dbCliente)
        {
            this.dbCliente = dbCliente;
        }


        [HttpGet]
        [EnableCors("AllowAll", "","GET")]
        public ActionResult Get()
        {
            var ListaClientes = this.dbCliente.GetAll();
            return Ok(ListaClientes);
            
        }

        [HttpPost]
        [EnableCors("AllowAll", "", "POST")]
        public IActionResult Post([FromBody]Cliente Cliente)
        {
            var result = this.dbCliente.Create(Cliente);

            if (result == null)
            {
                return BadRequest($"No fue posible crear al cliente {Cliente.Rut}.");
            }
            return Ok(result);
        }

        [HttpPut]
        [EnableCors("AllowAll", "", "PUT")]
        public IActionResult Put([FromBody]Cliente cliente)
        {
            var result = this.dbCliente.Update(cliente);
            
            if(result == null)
            {
                return BadRequest($"No se pudo actualizar el cliente {cliente.Rut}.");
            }
            dbCliente.SaveChanges();
            return Ok(result);
        }

        [HttpGet("{rut}")]
        [EnableCors("AllowAll", "", "GET")]
        public IActionResult Get(string rut)
        {
            var cliente = this.dbCliente.GetByRut(rut);

            if (cliente == null)
            {
                return BadRequest($"El cliente {rut} no se encuentra registrado.");
            }

            return Ok(cliente);
        }

        [HttpDelete("{rut}")]
        [EnableCors("AllowAll", "", "DELETE")]
        public IActionResult Delete(string rut)
        {
            var result = this.dbCliente.Delete(rut);
            if (result == null)
            {
                return BadRequest($"No fue posible eliminar al cliente {rut}.");
            }

            return Ok(result);
        }
    }
}