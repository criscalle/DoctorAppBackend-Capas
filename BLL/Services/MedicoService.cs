using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Models.DTOS;
using Models.Entities;

namespace BLL.Services;

public class MedicoService : IMedicoService
{
    private readonly IUnitWork _unitWork;
    private readonly IMapper _mapper;

    public MedicoService(IUnitWork unitWork, IMapper mapper)
    {
        _unitWork = unitWork;
        _mapper = mapper;
    }

    public async Task<MedicoDto> Add(MedicoDto modelDto)
    {
        try
        {
            Medico med = new Medico
            {
                Apellido = modelDto.Apellido,
                Nombre = modelDto.Nombre,
                Direccion = modelDto.Direccion,
                Telefono = modelDto.Telefono,
                Genero = modelDto.Genero,
                EspecialidadId = modelDto.EspecialidadId,
                Estado = modelDto.Estado == 1 ? true : false,
                FechaActualizacion = DateTime.Now,
                FechaCreacion = DateTime.Now           
            };
            await _unitWork.Medico.Add(med);
            await _unitWork.Save();
            if (med.Id == 0)
                throw new TaskCanceledException("La medico no se pudo crear");
            return _mapper.Map<MedicoDto>(med);

        }catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<MedicoDto>> GetAll()
    {
        try
        {
            var list = await _unitWork.Medico.GetAllAsync(includeProperties: "Especialidad",
                orderBy: e => e.OrderBy(e => e.Apellido));
            return _mapper.Map<IEnumerable<MedicoDto>>(list);

        }catch(Exception)
        {
            throw;
        }
    }

    public async Task Remove(int id)
    {
        try
        {
            var MedicoDb = await _unitWork.Medico.GetFirst(e => e.Id == id);
            if (MedicoDb == null)
                throw new TaskCanceledException("El Medico no existe");
            _unitWork.Medico.Remove(MedicoDb);
            await _unitWork.Save();

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Update(MedicoDto modelDto)
    {
        try
        {
            var MedicoDb = await _unitWork.Medico.GetFirst(e => e.Id == modelDto.Id);
            if (MedicoDb == null)
                throw new TaskCanceledException("El Medico no existe");

            MedicoDb.Nombre = modelDto.Nombre;
            MedicoDb.Apellido = modelDto.Apellido;
            MedicoDb.Direccion = modelDto.Direccion;
            MedicoDb.Telefono = modelDto.Telefono;
            MedicoDb.Genero = modelDto.Genero;
            MedicoDb.EspecialidadId = modelDto.EspecialidadId;
            MedicoDb.Estado = modelDto.Estado == 1 ? true : false;
            _unitWork.Medico.update(MedicoDb);
            await _unitWork.Save();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
