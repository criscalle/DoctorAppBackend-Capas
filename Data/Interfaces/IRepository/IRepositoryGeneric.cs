using System.Linq.Expressions;

namespace Data.Interfaces.IRepository;

public interface IRepositoryGeneric<T> where T : class  // tipo donde T sea cualquier clase
{
    Task<IEnumerable<T>> GetAllAsync(                        // tarea que obtiene una lista (IEnumerable) de tipo T (cualquier objeto)
        Expression<Func<T, bool>> filtro = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = null  
        );

    Task<T> GetFirst(                                      // tarea obtiene un objeto tipo t (cualquier objeto)
        Expression<Func<T, bool>> filter = null,
        string includeProperties = null
        );

    Task Add(T entity);
    void Remove(T entity);

}
