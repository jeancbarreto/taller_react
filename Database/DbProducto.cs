using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database
{
    public class DbProducto : DbContext
    {
        public DbSet<Producto> Producto { get; set; }

        public DbProducto(DbContextOptions<DbProducto> options)
            : base(options)
        {

        }


        public List<Producto> GetAll()
        {
            // Ejemplo con una Query de SQL
            List<Producto> Productos = this
                                        .Producto
                                        .FromSql("SELECT * FROM Producto")
                                        .ToList();

            return Productos;
        }

        public Producto GetByCode(string codeProduct)
        {
            // Ejemplo con LinQ
            Producto producto = this
                                .Producto
                                .FirstOrDefault(c => c.CodeProduct == codeProduct);
            return producto;
        }

        public Producto Create(Producto producto)
        {
            try
            {
                this
                    .Producto
                    .Add(producto);

                this.SaveChanges();

                return producto;
            }
            catch (Exception ex)
            {
                //throw new Exception("No fue posible crear el cliente");
                return null;
            }
        }

        public Producto Delete(string codeProduct)
        {
            try
            {
                var code = this.GetByCode(codeProduct);

                if (code == null)
                {
                    //throw new Exception($"El Producto código {codeProduct} no se puede eliminar debido a no existe.");
                    return null;
                }

                this
                    .Producto
                    .Remove(code);

                this.SaveChanges();

                return code;
            }
            catch (Exception ex)
            {
                //throw new Exception($"No fue posible eliminar el producto {codeProduct}");
                return null;
            }
        }

    }
}
