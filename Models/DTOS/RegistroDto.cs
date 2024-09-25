using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS;

public class RegistroDto
{
    [Required(ErrorMessage ="Username es Requerido")] // validacion para que sea obligatorio el usuario
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password es Requerido")] // validacion para que sea obligatoria la contraseña
    [StringLength(8, MinimumLength =4, ErrorMessage ="El password debe ser Mínimo 4 máximo 10 caracteres")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Apellido es Requerido")] 
    public string Apellido { get; set; }

    [Required(ErrorMessage = "Nombre es Requerido")] 
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Email es Requerido")] 
    public string Email { get; set; }

    [Required(ErrorMessage = "Rol es Requerido")] 
    public string Rol { get; set; }


}
