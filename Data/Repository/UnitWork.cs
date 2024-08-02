using Data.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository;

public class UnitWork : IUnitWork
{
    private readonly ApplicationDbContext _context;

    public ISpecialityRepository Speciality { get; private set; }

    public UnitWork(ApplicationDbContext context)
    {
        _context = context;
        Speciality = new SpecialityRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
