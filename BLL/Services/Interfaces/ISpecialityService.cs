using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces;

public interface ISpecialityService
{
    Task<IEnumerable<SpecialityDto>> GetAll();
    Task<IEnumerable<SpecialityDto>> GetActivos();
    Task<SpecialityDto> Add(SpecialityDto modelDto);
    Task Update(SpecialityDto modelDto);
    Task Remove(int id);
}
