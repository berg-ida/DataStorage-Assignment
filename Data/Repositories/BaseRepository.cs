using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    private readonly DataContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private IDbContextTransaction _transaction = null!;

    //Save
    public virtual async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    //Create
    public virtual async Task<bool> CreateAsync(TEntity entity)
    {
        if (entity == null)
        {
            return false;
        }
        try
        {
            await _dbSet.AddAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating {nameof(TEntity)} entity :: {ex.Message}");
            return false;
        }
    }

    //Read
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return entities;
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
        {
            return null!;
        }

        var entity = await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
        return entity;
    }

    //Update
    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity)
    {
        if (updatedEntity == null)
        {
            return null!;
        }
        try
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(expression) ?? null!;
            if (existingEntity == null)
            {
                return null!;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            return existingEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating {nameof(TEntity)} entity :: {ex.Message}");
            return null!;
        }
    }

    //Delete
    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
        {
            return false;
        }
        try
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(expression) ?? null;
            if (existingEntity == null)
            {
                return false;
            }

            _dbSet.Remove(existingEntity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting {nameof(TEntity)} entity :: {ex.Message}");
            return false;
        }
    }
}