using System;

namespace freak_store.ViewModels
{
    public class OfferViewModel
    {
        public string Title { get; set; } // Título del juego
        public string Thumbnail { get; set; } // Imagen del juego
        public string DealID { get; set; } // ID de la oferta
        public string Store { get; set; } // Nombre de la tienda
        public decimal SalePrice { get; set; } // Precio de oferta
        public decimal NormalPrice { get; set; } // Precio normal
        public string Thumb { get; set; } // URL de la miniatura
        // Agrega otras propiedades si es necesario según la estructura de la respuesta JSON
    }
}
