using Models.DTOS;

namespace BLL.Services.Interfaces;

public interface IMedicoService
{
    Task<IEnumerable<MedicoDto>> GetAll();
    Task<MedicoDto> Add(MedicoDto modelDto);
    Task Update(MedicoDto modelDto);
    Task Remove(int id);
}
