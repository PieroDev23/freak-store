using System;

namespace freak_store.Models
{
    public class Inventory
    {
        public Guid Id { get; set; } // Clave primaria
        public int Quantity { get; set; } // Cantidad en inventario
        public DateTime CreatedAt { get; set; } // Fecha de creación

        public Guid ProductId { get; set; } // Clave foránea hacia Product
        public Product Product { get; set; } // Relación con Product
    }
}
