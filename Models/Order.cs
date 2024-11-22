using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace freak_store.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        // Clave foránea de tipo string para coincidir con el tipo Id de User
        [Required]
        public string UserId { get; set; }  
        public User User { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public string Status { get; set; } // Ejemplo: "Pendiente", "Enviado", "Completado"

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Colección para los detalles de la orden
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
