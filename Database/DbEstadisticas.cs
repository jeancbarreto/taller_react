using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Database
{
    public class DbEstadisticas : DbContext
    {
        public DbSet<Cliente> cliente { get; set; }

        public DbEstadisticas(DbContextOptions<DbCliente> options)
            : base(options)
        {

        }


        public List<Estadisticas> GetAll()
        {
            // Ejemplo con una Query de SQL
            List<Estadisticas> ListEstadistica = new List<Estadisticas>();
            Estadisticas estadistica = new Estadisticas();
            List<Cliente> clientes = this
                                        .cliente
                                        .FromSql("SELECT * FROM Cliente")
                                        .ToList();

            
            foreach (var cl in clientes.Where(x => Convert.ToDateTime(x.Fecha).Month == 1)) {
                estadistica.Mes = Convert.ToDateTime(cl.Fecha).Month;

                ListEstadistica.Add(estadistica);
            }

            return ListEstadistica;

        }
    }
}
