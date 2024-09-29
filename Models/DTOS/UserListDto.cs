using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOS;

public class UserListDto
{
    public String username { get; set; }
    public String apellido { get; set; }
    public String nombre { get; set; }
    public String email { get; set; }
    public String rol { get; set; }
}
