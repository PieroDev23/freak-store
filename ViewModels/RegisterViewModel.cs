using System.ComponentModel.DataAnnotations;

namespace freak_store.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El campo Nombre de usuario es obligatorio.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El campo Correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ser un correo electrónico válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo Confirmar contraseña es obligatorio.")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo Teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Debe ser un número de teléfono válido.")]
        public string Phone { get; set; }
    }
}
