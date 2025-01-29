using Data.Contexts;
using Data.Entites;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class ClientRepository(DataContext context) : IClientRepository
    {
        private readonly DataContext _context = context;

        //Create
        public async Task<bool> CreateAsync(ClientEntity entity)
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
                Debug.WriteLine($"Error creating client entity :: {ex.Message}");
                return false;
            }
        }

        //Read
        public async Task<IEnumerable<ClientEntity>> GetAllAsync()
        {
            var entities = await _context.Clients.ToListAsync();
            return entities;
        }

        public async Task<ClientEntity?> GetAsync(Expression<Func<ClientEntity, bool>> expression)
        {
            if (expression == null)
            {
                return null!;
            }

            var entity = await _context.Clients.FirstOrDefaultAsync(expression) ?? null!;
            return entity;
        }

        //Update
        public async Task<bool> UpdateAsync(ClientEntity updatedEntity)
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
                Debug.WriteLine($"Error updating client entity :: {ex.Message}");
                return false;
            }
        }

        //Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.Clients.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
