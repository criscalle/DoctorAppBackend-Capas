using System.ComponentModel.DataAnnotations;

namespace Models.DTOS;

public class MedicoDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Apellido es Requerido")]
    [StringLength(60, MinimumLength = 1, ErrorMessage = "Apellido debe ser de Mínimo 1 Máximo 60 caracteres")]
    public string Apellido { get; set; }

    [Required(ErrorMessage = "Nombre es Requerido")]
    [StringLength(60, MinimumLength = 1, ErrorMessage = "Nombre debe ser de Mínimo 1 Máximo 60 caracteres")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Direccion es Requerido")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Direccion debe ser de Mínimo 1 Máximo 100 caracteres")]
    public string Direccion { get; set; }

    [MaxLength(40)]
    public string Telefono { get; set; }

    [Required(ErrorMessage = "Genero es Requerido")]
    public char Genero { get; set; }

    [Required(ErrorMessage = "Especialidad es Requerida")]
    public int EspecialidadId { get; set; }

    public string NameSpeciality  { get; set; }

    public int Estado { get; set; } // 1 o 0

}
