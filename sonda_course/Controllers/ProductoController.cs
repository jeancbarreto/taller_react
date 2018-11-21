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
    public class ProductoController : ControllerBase
    {
        private readonly DbProducto dbProducto;


        public ProductoController(DbProducto dbProducto)
        {
            this.dbProducto = dbProducto;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var ListaProducto = this.dbProducto.GetAll();
            return Ok(ListaProducto);

        }

        [HttpPost]
        public IActionResult Post([FromBody]Producto Producto)
        {
            var result = this.dbProducto.Create(Producto);

            if (result == null)
            {
                return BadRequest($"No fue posible crear el producto {Producto.CodeProduct}.");
            }
            return Ok(result);
        }

        [HttpPut]
        [EnableCors("AllowAll", "", "PUT")]
        public IActionResult Put([FromBody]Producto producto)
        {
            var result = this.dbProducto.Update(producto);
            

            if (result == null)
            {
                return BadRequest($"No se pudo actualizar el producto {producto.CodeProduct}.");
            }
            dbProducto.SaveChanges();
            return Ok(result);
        }


        [HttpGet("{code}")]
        [EnableCors("AllowAll", "", "GET")]
        public IActionResult Get(string code)
        {
            var cliente = this.dbProducto.GetByCode(code);

            if (cliente == null)
            {
                return BadRequest($"El producto {code} no se encuentra registrado.");
            }

            return Ok(cliente);
        }

        [HttpDelete("{code}")]
        [EnableCors("AllowAll", "", "DELETE")]
        public IActionResult Delete(string code)
        {
            var result = this.dbProducto.Delete(code);
            if (result == null)
            {
                return BadRequest($"No fue posible eliminar el producto {code}.");
            }

            return Ok(result);
        }
    }
}
