namespace Server.Services.Repositories.Interfaces;

using System.Linq.Expressions;

public interface IGenericRepository<T> where T : class
{
    // Retrieve all records
    Task<IEnumerable<T>> GetAllAsync();

    // Retrieve an entity by Id
    Task<T> GetByIdAsync(int id);

    // Retrieve entities based on a condition with optional includes
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, string[] includes = null);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string[] includes = null);




    public Task<IEnumerable<T>> GetAllAsyncNoTracking(
        Expression<Func<T, bool>> predicate,
        string[] includes = null,
        bool asNoTracking = false,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);




    Task<T> GetByNameAsync(Expression<Func<T, bool>> predicate);

    // Add a new entity
    Task<T> AddAsync(T entity);

    // Update an existing entity
    T Update(T entity);

    // Delete an entity by Id
    T Delete(int id);
}
