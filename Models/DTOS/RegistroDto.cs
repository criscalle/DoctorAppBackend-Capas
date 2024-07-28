using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS;

public class RegistroDto
{
    [Required(ErrorMessage ="UserName es Requerido")] // validacion para que sea obligatorio el usuario
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password es Requerido")] // validacion para que sea obligatoria la contraseña
    [StringLength(8, MinimumLength =4, ErrorMessage ="El password debe ser Mínimo 4 máximo 10 caracteres")]
    public string Password { get; set; }
}
