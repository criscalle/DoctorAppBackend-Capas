using AutoMapper;
using BLL.Services.Interfaces;
using Data.Interfaces.IRepository;
using Models.DTOS;
using Models.Entities;

namespace BLL.Services;

public class SpecialityService : ISpecialityService
{
    private readonly IUnitWork _unitWork;
    private readonly IMapper _mapper;

    public SpecialityService(IUnitWork unitWork, IMapper mapper)
    {
        _unitWork = unitWork;
        _mapper = mapper;
    }

    public async Task<SpecialityDto> Add(SpecialityDto modelDto)
    {
        try
        {
            Speciality spec = new Speciality
            {
                namespeciality = modelDto.namespeciality,
                description = modelDto.description,
                state = modelDto.state == 1 ? true : false,
                datecreation = DateTime.Now,
                dateupdate = DateTime.Now,
            };
            await _unitWork.Speciality.Add(spec);
            await _unitWork.Save();
            if (spec.Id == 0)
                throw new TaskCanceledException("La especialidad no se pudo crear");
            return _mapper.Map<SpecialityDto>(spec);

        }catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<SpecialityDto>> GetAll()
    {
        try
        {
            var list = await _unitWork.Speciality.GetAllAsync(
                orderBy: e => e.OrderBy(e => e.namespeciality));
            return _mapper.Map<IEnumerable<SpecialityDto>>(list);

        }catch(Exception)
        {
            throw;
        }
    }

    public async Task Remove(int id)
    {
        try
        {
            var specialityDb = await _unitWork.Speciality.GetFirst(e => e.Id == id);
            if (specialityDb == null)
                throw new TaskCanceledException("La Especialidad no existe");
            _unitWork.Speciality.Remove(specialityDb);
            await _unitWork.Save();

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Update(SpecialityDto modelDto)
    {
        try
        {
            var specialityDb = await _unitWork.Speciality.GetFirst(e => e.Id == modelDto.Id);
            if (specialityDb == null)
                throw new TaskCanceledException("La Especialidad no existe");

            specialityDb.namespeciality = modelDto.namespeciality;
            specialityDb.description = modelDto.description;
            specialityDb.state = modelDto.state == 1 ? true : false;
            _unitWork.Speciality.update(specialityDb);
            await _unitWork.Save();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<SpecialityDto>> GetActivos()
    {
        try
        {
            var list = await _unitWork.Speciality.GetAllAsync(x => x.state == true,
                orderBy: e => e.OrderBy(e => e.namespeciality));
            return _mapper.Map<IEnumerable<SpecialityDto>>(list);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
