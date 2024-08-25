using Models.Entities;


namespace Data.Interfaces.IRepository;

public interface IMedicoRepository : IRepositoryGeneric<Medico>
{
    void update(Medico medico);
}
