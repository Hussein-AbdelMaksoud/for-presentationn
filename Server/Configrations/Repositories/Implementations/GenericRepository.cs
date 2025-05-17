namespace Server.Services.Repositories.Implementations;

using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Services.Repositories.Interfaces;
using System.Linq.Expressions;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDBContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDBContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    public async Task<T> GetByNameAsync(Expression<Func<T, bool>> predicate) => await _dbSet.FirstOrDefaultAsync(predicate);


    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, string[] includes = null)
    {
        IQueryable<T> query = _dbSet;

        if (includes != null)
        {
            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }
        }

        // Make predicate optional
        predicate ??= x => true;

        return await query.Where(predicate).ToListAsync();
    }



    public async Task<IEnumerable<T>> GetAllAsyncNoTracking(
        Expression<Func<T, bool>> predicate,
        string[] includes = null,
        bool asNoTracking = false,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
    {
        IQueryable<T> query = _dbSet;

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        if (includes != null)
        {
            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }
        }

        predicate ??= x => true;

        query = query.Where(predicate);

        if (orderBy != null)
        {
            query = orderBy(query); // Apply ordering if provided
        }

        return await query.ToListAsync();
    }









    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string[] includes = null)
    {
        IQueryable<T> query = _dbSet;

        if (includes != null)
        {
            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.FirstOrDefaultAsync(predicate);
    }



    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public T Update(T entity)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public T Delete(int id)
    {
        var entity = _dbSet.Find(id);
        _dbSet.Remove(entity);
        return entity;
    }
}
