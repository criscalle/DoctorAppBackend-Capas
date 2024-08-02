using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS;

public class SpecialityDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(60, MinimumLength = 1, ErrorMessage = "El nombre debe ser de Mínimo 1 máximo 60 caracteres")]
    public string namespeciality { get; set; }

    [Required(ErrorMessage = "La descripcion es requerida")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "la Descripcion debe ser de Mínimo 1 máximo 100 caracteres")]
    public string description { get; set; }

    [Required(ErrorMessage = "La estado es requerido")]
    public int state { get; set; } // se cambia para que resiva valores de 1 y 0 y luego se hace la conversion
}
