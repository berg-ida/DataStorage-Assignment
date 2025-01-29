using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class ManagerRepository(DataContext context) : IManagerRepository
    {
        private readonly DataContext _context = context;

        //Create
        public async Task<bool> CreateAsync(ManagerEntity entity)
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
                Debug.WriteLine($"Error creating manager entity :: {ex.Message}");
                return false;
            }
        }

        //Read
        public async Task<IEnumerable<ManagerEntity>> GetAllAsync()
        {
            var entities = await _context.ProjectManagers.ToListAsync();
            return entities;
        }

        public async Task<ManagerEntity?> GetAsync(Expression<Func<ManagerEntity, bool>> expression)
        {
            if (expression == null)
            {
                return null!;
            }

            var entity = await _context.ProjectManagers.FirstOrDefaultAsync(expression) ?? null!;
            return entity;
        }

        //Update
        public async Task<bool> UpdateAsync(ManagerEntity updatedEntity)
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
                Debug.WriteLine($"Error updating manager entity :: {ex.Message}");
                return false;
            }
        }

        //Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ProjectManagers.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.ProjectManagers.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
