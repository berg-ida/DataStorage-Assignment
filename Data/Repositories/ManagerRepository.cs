using Data.Contexts;
using Data.Entites;
using Data.Interfaces;

namespace Data.Repositories
{
    public class ManagerRepository(DataContext context) : BaseRepository<ManagerEntity>(context), IManagerRepository
    {
        private readonly DataContext _context = context;
    }
}
