using System;

namespace freak_store.Models
{
    public class Category
    {
        public Guid Id { get; set; } // Clave primaria, tipo GUID
        public string Name { get; set; } // Nombre de la categoría
        public string Description { get; set; } // Descripción de la categoría
        public DateTime CreatedAt { get; set; } // Fecha de creación
        public DateTime? UpdatedAt { get; set; } // Fecha de actualización (opcional)
        public DateTime? DeletedAt { get; set; } // Fecha de eliminación (opcional)

        public ICollection<Product> Products { get; set; } // Relación uno a muchos con productos
    }
}
