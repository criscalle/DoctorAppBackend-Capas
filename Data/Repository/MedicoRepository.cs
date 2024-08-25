using Data.Interfaces.IRepository;
using Models.Entities;


namespace Data.Repository;

public class MedicoRepository : Repository<Medico>, IMedicoRepository
{
    private readonly ApplicationDbContext _context;

    public MedicoRepository(ApplicationDbContext context): base(context)
    {
        _context = context;
    }
    public void update(Medico medico)
    {
        var medicoDb = _context.Medicos.FirstOrDefault(e => e.Id == medico.Id);

        if (medicoDb != null)
        {
            medicoDb.Nombre = medico.Nombre;
            medicoDb.Apellido = medico.Apellido;
            medicoDb.Direccion = medico.Direccion;
            medicoDb.Telefono = medico.Telefono;
            medicoDb.Genero = medico.Genero;
            medicoDb.EspecialidadId = medico.EspecialidadId;
            medicoDb.Estado = medico.Estado;
            medicoDb.FechaActualizacion = DateTime.Now;
            _context.SaveChanges();
        }
    }

    
}
