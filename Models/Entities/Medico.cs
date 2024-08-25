using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Models.Entities;

public class Medico
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage ="Apellido es Requerido")]
    [StringLength(60, MinimumLength =1, ErrorMessage ="Apellido debe ser de Mínimo 1 Máximo 60 caracteres")]
    public string Apellido { get; set; }

    [Required(ErrorMessage = "Nombre es Requerido")]
    [StringLength(60, MinimumLength = 1, ErrorMessage = "Nombre debe ser de Mínimo 1 Máximo 60 caracteres")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Direccion es Requerido")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Direccion debe ser de Mínimo 1 Máximo 100 caracteres")]
    public string Direccion { get; set; }

    [StringLength(40, MinimumLength = 1, ErrorMessage = "Telefono debe ser de Mínimo 1 Máximo 40 caracteres")]
    public string Telefono { get; set; }

    [Required(ErrorMessage = "Genero es Requerido")]
    public char Genero { get; set; }

    [Required(ErrorMessage = "Especialidad es Requerida")]
    public int EspecialidadId { get; set; }

    [ForeignKey("EspecialidadId")]
    public Speciality Especialidad {  get; set; }

    public bool Estado { get; set; }

    public DateTime FechaCreacion {  get; set; }

    public DateTime FechaActualizacion { get; set; }

}
