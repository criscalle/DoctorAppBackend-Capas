using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces.IRepository;

public interface IUnitWork : IDisposable
{
    ISpecialityRepository Speciality { get; }
    IMedicoRepository Medico { get; }   
    Task Save();
}
