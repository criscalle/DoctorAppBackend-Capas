using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities;

public class Speciality
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(60, MinimumLength =1, ErrorMessage = "El nombre debe ser de Mínimo 1 máximo 60 caracteres")]
    public string namespeciality { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "la Descripcion debe ser de Mínimo 1 máximo 100 caracteres")]
    public string description { get; set; }

    public bool state { get; set; }

    public DateTime datecreation { get; set; }

    public DateTime dateupdate { get; set; }
}
