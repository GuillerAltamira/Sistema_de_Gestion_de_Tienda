using System;

namespace TecnoVenta.Models
{
    public class Venta
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }
    }
}
