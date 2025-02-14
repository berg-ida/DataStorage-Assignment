using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class TimeRepository(DataContext context) : ITimeRepository
    {
        private readonly DataContext _context = context;

        //Create
        public async Task<bool> CreateAsync(TimeEntity entity)
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
                Debug.WriteLine($"Error creating time entity :: {ex.Message}");
                return false;
            }
        }

        //Read
        public async Task<IEnumerable<TimeEntity>> GetAllAsync()
        {
            var entities = await _context.TimePeriods.ToListAsync();
            return entities;
        }

        public async Task<TimeEntity?> GetAsync(Expression<Func<TimeEntity, bool>> expression)
        {
            if (expression == null)
            {
                return null!;
            }

            var entity = await _context.TimePeriods.FirstOrDefaultAsync(expression) ?? null!;
            return entity;
        }

        //Update
        public async Task<bool> UpdateAsync(TimeEntity updatedEntity)
        {
            if (updatedEntity == null)
            {
                return false;
            }
            try
            {
                var existingEntity = await _context.TimePeriods.FindAsync(updatedEntity.Id);
                if (existingEntity == null)
                {
                    return false;
                }

                _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating time entity :: {ex.Message}");
                return false;
            }
        }

        //Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TimePeriods.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.TimePeriods.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
