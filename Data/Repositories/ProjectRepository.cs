using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        var entities = await _context.Projects
            .Include(p => p.TimePeriod)
            .Include(p => p.ProjectManager)
            .Include(p => p.Client)
            .ToListAsync();
        return entities;
    }

    public override async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        if (expression == null)
        {
            return null!;
        }

        var entity = await _context.Projects
            .Include(p => p.TimePeriod)
            .Include(p => p.ProjectManager)
            .Include(p => p.Client)
            .FirstOrDefaultAsync(expression) ?? null!;
        return entity;
    }

    public override async Task<ProjectEntity> UpdateAsync(Expression<Func<ProjectEntity, bool>> expression, ProjectEntity updatedEntity)
    {
        if (updatedEntity == null)
        {
            return null!;
        }
        try
        {
            var existingEntity = await _context.Projects
                .Include(p => p.TimePeriod)
                .Include(p => p.ProjectManager)
                .Include(p => p.Client)
                .FirstOrDefaultAsync(p => p.Id == updatedEntity.Id);

            if (existingEntity == null)
            {
                return null!;
            }

            existingEntity.Name = updatedEntity.Name;
            existingEntity.ServiceName = updatedEntity.ServiceName;
            existingEntity.Price = updatedEntity.Price;
            existingEntity.Status = updatedEntity.Status;
            existingEntity.TimePeriod.StartDay = updatedEntity.TimePeriod.StartDay;
            existingEntity.TimePeriod.StartMonth = updatedEntity.TimePeriod.StartMonth;
            existingEntity.TimePeriod.StartYear = updatedEntity.TimePeriod.StartYear;
            existingEntity.TimePeriod.EndDay = updatedEntity.TimePeriod.EndDay;
            existingEntity.TimePeriod.EndMonth = updatedEntity.TimePeriod.EndMonth;
            existingEntity.TimePeriod.EndYear = updatedEntity.TimePeriod.EndYear;
            existingEntity.ProjectManager.FirstName = updatedEntity.ProjectManager.FirstName;
            existingEntity.ProjectManager.LastName = updatedEntity.ProjectManager.LastName;
            existingEntity.Client.CompanyName = updatedEntity.Client.CompanyName;

            return existingEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating project entity :: {ex.Message}");
            return null!;
        }
    }
}


