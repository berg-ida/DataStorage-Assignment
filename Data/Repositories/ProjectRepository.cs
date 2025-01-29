using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : IProjectRepository
{
    private readonly DataContext _context = context;

    //Create
    public async Task<bool> CreateAsync(ProjectEntity entity)
    {
        if (entity == null)
        {
            return false;
        }

        try
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating project entity :: {ex.Message}");
            return false;
        }
    }

    //Read
    public async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        var entities = await _context.Projects.ToListAsync();
        return entities;
    }

    public async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        if (expression == null)
        {
            return null!;
        }

        var entity = await _context.Projects.FirstOrDefaultAsync(expression) ?? null!;
        return entity;
    }

    //Update
    public async Task<bool> UpdateAsync(ProjectEntity updatedEntity)
    {
        if (updatedEntity == null)
        {
            return false;
        }
        try
        {
            _context.Update(updatedEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating project entity :: {ex.Message}");
            return false;
        }
    }

    //Delete
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            _context.Projects.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}


