using Data.Interfaces.IRepository;
using Models.Entities;


namespace Data.Repository;

public class SpecialityRepository : Repository<Speciality>, ISpecialityRepository
{
    private readonly ApplicationDbContext _context;

    public SpecialityRepository(ApplicationDbContext context): base(context)
    {
        _context = context;
    }
    public void update(Speciality speciality)
    {
        var specialityDB = _context.Specialities.FirstOrDefault(e => e.Id == speciality.Id);

        if (specialityDB != null)
        {
            specialityDB.namespeciality = speciality.namespeciality;
            specialityDB.description = speciality.description;
            specialityDB.state = speciality.state;
            specialityDB.dateupdate = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
