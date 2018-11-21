using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string CodeProduct { get; set; }
        public string NombreProducto { get; set; }
        public string TipoProducto { get; set; }
        public string SaldoMinimo { get; set; }
        public int Estado { get; set; }

    }
}
