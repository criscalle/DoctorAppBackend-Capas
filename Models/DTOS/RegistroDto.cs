using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS;

public class RegistroDto
{
    [Required(ErrorMessage ="Usuario es Requerido")] // validacion para que sea obligatorio el usuario
    public string UserName { get; set; }

    [Required(ErrorMessage = "Contraseña es Requerida")] // validacion para que sea obligatoria la contraseña
    public string Password { get; set; }
}
